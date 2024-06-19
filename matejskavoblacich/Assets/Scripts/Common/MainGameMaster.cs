using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles flow of the whole game
/// </summary>
public class MainGameMaster : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] GameObject initiInput;
    [SerializeField] GameObject sceneTransition;
    [SerializeField] List<MinigamePrefabSO> originalMinigamesPrefabs;
    [SerializeField] List<Vector3> playerPositions;

    [Header ("Time settings")]
    [SerializeField,Tooltip("Time between loading new scene and starting minigame")] float waitTimeBeforeMinigameStart = 3;
    [SerializeField] float maxMinigamePlayTimeSeconds = 90;
    [SerializeField] float waitTimeAfterMinigameEnds = 3;
    [SerializeField] float sceneTransitionWaitTime = 1;

    [Header ("Stall movement variables")]
    [SerializeField] float minYMove;
    [SerializeField] float maxYMove;
    [SerializeField] float baseYMove = -1;

    GameObject initiInstance;
    List<Results> resultsHistory = new();
    List<MinigamePrefabSO> minigames;
    int numberOfPlayers;
    float currentYMove;
    bool skipMinigame = false; //Can be set with skip minigame button
    [HideInInspector] public bool showSingleMinigameButtons = false;

    public enum GameState{
        Menu,
        LoadMinigame,
        PlayMinigame,
        ShowResultsBetweenMinigames,
        ShowResultsFinalScene,
        ShowLeaderboard,
        DoNothing
    };

    void Awake(){
        //Dont destroy on load, destroy if exists
        if(FindObjectsByType<MainGameMaster>(FindObjectsSortMode.None).Length > 1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        //Instantiate initiInput only once
        initiInstance = Instantiate(initiInput);

        Instantiate(sceneTransition);

        //Save all minigames, to be able to restore it after changes
        minigames = new List<MinigamePrefabSO>(originalMinigamesPrefabs);

        currentYMove = baseYMove;
    }
    
    public void SetNumberOfPlayers(int num){
        numberOfPlayers = num;
    }

    /// <summary>
    /// Offset all minigames
    /// </summary>
    /// <param name="yMove">Offset, clamped between minYMove and maxYMove</param>
    public void AddYMove(float yMove){
        //currentYMove = baseYMove + Mathf.Clamp(yMove,minYMove,maxYMove);
        currentYMove = Mathf.Clamp(currentYMove+yMove,minYMove,maxYMove);
    }

    public float GetYMove(){
        return currentYMove;
    }

    /// <summary>
    /// Instantly ends current minigame
    /// </summary>
    public void SkipMinigame(){
        skipMinigame = true;
    }

    /// <summary>
    /// Skip all remaining minigames and show final results
    /// </summary>
    public void SkipToFinalScene(){
        StartCoroutine(TransitionToScene("FinalScene",GameState.ShowResultsFinalScene));
    }

    public void ChangeState(GameState newState){
        switch(newState){
            case GameState.LoadMinigame:
                StartCoroutine(TransitionToScene("Minigame",GameState.PlayMinigame));
                break;
            case GameState.PlayMinigame:
                StartCoroutine(PrepareGame());
                break;
            case GameState.ShowResultsBetweenMinigames:
                FindObjectOfType<BetweenMinigamesResults>().ShowResults(resultsHistory[^1].results);
                break;
            case GameState.ShowResultsFinalScene:
                FindObjectOfType<FinalSceneResults>().FinalResults(resultsHistory);
                break;
            case GameState.ShowLeaderboard:
                StartCoroutine(TransitionToScene("Leaderboard",GameState.DoNothing));
                break;
            case GameState.Menu:
                //Restore minigames for next playing
                minigames = new List<MinigamePrefabSO>(originalMinigamesPrefabs);
                resultsHistory = new();
                StartCoroutine(TransitionToScene("Menu",GameState.DoNothing));
                break;
        }
    }

    /// <summary>
    /// Transtions to new scene with clouds effect, then changes MainGameMaster to nextState
    /// </summary>
    /// <param name="sceneName"> Scene to transition to </param>
    /// <param name="nextState"> MainGameMaster state after transition </param>
    IEnumerator TransitionToScene(string sceneName, GameState nextState){
        FindObjectOfType<SceneTransition>().StartTransition();
        yield return new WaitForSeconds(sceneTransitionWaitTime);

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0);

        Instantiate(sceneTransition);

        ChangeState(nextState);
    }


    /// <summary>
    /// Used to play only one minigame
    /// </summary>
    /// <param name="id"> Desired minigame index in minigames array </param>
    public void PlaySingleMinigame(int id){
        MinigamePrefabSO chosenOne = minigames[id];
        minigames.Clear();
        minigames.Add(chosenOne);
        ChangeState(GameState.LoadMinigame);
    }

    /// <summary>
    /// Instantiates minigames instances, fill required variables, call PlayMinigame to start minigame
    /// </summary>
    IEnumerator PrepareGame(){
        //Move Stall up/down to match player height 
        GameObject.Find("Stall").transform.position += Vector3.up * currentYMove;

        List<Minigame> currentMinigames = new();

        //Choose randomly next minigame
        int nextGameId = Random.Range(0,minigames.Count); 

        GameObject game = minigames[nextGameId].minigamePrefab;

        //Set timer visualization
        Timer timer = FindObjectOfType<Timer>();
        timer.SetTime(waitTimeBeforeMinigameStart);

        //Show minigame intro text, wait
        TMP_Text introText = GameObject.FindGameObjectWithTag("IntroText").GetComponent<TMP_Text>();
        introText.text = minigames[nextGameId].introText.Replace("\\n","\n");
        introText.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitTimeBeforeMinigameStart);

        //Hide text
        introText.gameObject.SetActive(false);

        //Remove chosen minigame from the pool
        minigames.RemoveAt(nextGameId);

        //Prepare minigame variables, instantiate minigames
        float minigameStartTime = Time.time;
        int seed = Random.Range(0,int.MaxValue);
        Minigame minigame = game.GetComponent<Minigame>();
        minigame.seed = seed;
        minigame.startTime = minigameStartTime;
        minigame.endTime = minigameStartTime + maxMinigamePlayTimeSeconds;

        for(int i=0;i<playerPositions.Count;i++){
            if(numberOfPlayers<=i) break;
            minigame.playerId = i;
            currentMinigames.Add(Instantiate(game,playerPositions[i]+(Vector3.up*currentYMove),Quaternion.identity).GetComponent<Minigame>());
        }
        
        StartCoroutine(PlayGame(currentMinigames));
    }

    /// <summary>
    /// Used from Prepare minigame, should never be called from a different place, waits until time runs out, skip button is pressed or all minigame instances ends
    /// </summary>
    /// <param name="minigames"> List of current minigame instances </param>
    /// <returns></returns>
    IEnumerator PlayGame(List<Minigame> minigames){
        float minigameStartTime = Time.time;
        Timer timer = FindObjectOfType<Timer>();
        timer.SetTime(maxMinigamePlayTimeSeconds);
        skipMinigame = false;

        //Play for a set time
        while(Time.time < minigameStartTime+maxMinigamePlayTimeSeconds){

            //End sooner if all minigames already ended
            bool end = true;
            foreach(Minigame m in minigames){
                if(!m.isFinished){
                    end = false;
                    break;
                }
            }
            if(end || skipMinigame) break;
            //Check again later
            yield return new WaitForSeconds(0.5f);
        }

        //Disable input - end of game
        initiInstance.SetActive(false);
        timer.SetTime(waitTimeAfterMinigameEnds);
        yield return new WaitForSeconds(waitTimeAfterMinigameEnds);

        //Enable input again
        initiInstance.SetActive(true);
        EndMinigame(minigames);
    }   

    /// <summary>
    /// Saves score, transitions to BetweenMinigames or FinalScene
    /// </summary>
    /// <param name="minigames"> List of current minigame instances </param>
    void EndMinigame(List<Minigame> minigames){
        //Count score
        Results gameResults = new();
        gameResults.results = new int[numberOfPlayers];
        for(int i=0;i<numberOfPlayers;i++){
            gameResults.results[i]=minigames[i].score;
        }
        resultsHistory.Add(gameResults);
        
        //Show score
        if(this.minigames.Count>0){
            StartCoroutine(TransitionToScene("BetweenMinigames",GameState.ShowResultsBetweenMinigames));
        } else {
            StartCoroutine(TransitionToScene("FinalScene",GameState.ShowResultsFinalScene));
        }
    }
}
