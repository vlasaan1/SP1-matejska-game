using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMaster : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] GameObject initiInput;
    [SerializeField] List<MinigamePrefabSO> originalMinigamesPrefabs;
    [SerializeField] List<Vector3> playerPositions;
    [Header ("Time settings")]
    [SerializeField,Tooltip("Time between loading new scene and starting minigame")] float waitTimeBeforeMinigameStart = 3;
    [SerializeField] float maxMinigamePlayTimeSeconds = 90;
    [SerializeField] float waitTimeAfterMinigameEnds = 3;
    [Header ("Stall movement variables")]
    [SerializeField] float minYMove;
    [SerializeField] float maxYMove;

    GameObject initiInstance;
    List<Results> resultsHistory = new();
    MinigamePrefabSO currentMinigamePrefab;
    List<MinigamePrefabSO> minigames;
    bool skipMinigame = false;
    int numberOfPlayers;
    float currentYMove = 0;



    void Awake(){
        //Dont destroy on load, destroy if exists
        if(FindObjectsByType<MainGameMaster>(FindObjectsSortMode.None).Length > 1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        //Instantiate initiInput only once
        initiInstance = Instantiate(initiInput);

        //Save all minigames, to be able to restore it after changes
        minigames = new List<MinigamePrefabSO>(originalMinigamesPrefabs);
    }
    
    public void SetNumberOfPlayers(int num){
        numberOfPlayers = num;
    }

    public void SetYMove(float yMove){
        //Vypocet realneho posunu ??

        if(yMove < minYMove) currentYMove = minYMove;
        else if(yMove > maxYMove) currentYMove = maxYMove;
        else currentYMove = yMove;
    }

    public Results GetLastResults(){
        return resultsHistory[^1];
    }

    public List<Results> GetAllResults(){
        return resultsHistory;
    }

    public void SkipMinigame(){
        skipMinigame = true;
    }

    //Temporary method
    public void LoadGameById(int id){
        MinigamePrefabSO chosenOne = minigames[id];
        minigames.Clear();
        minigames.Add(chosenOne);
        numberOfPlayers = 3;
        LoadGame();
    }

    public void LoadGame(){
        SceneManager.LoadScene("Minigame");
        StartCoroutine(PrepareGame());
    }

    IEnumerator PrepareGame(){
        //Wait until next update, until new scene is prepared
        yield return new WaitUntil(() => true);
        //Move Stall up/down to match player height 
        GameObject.Find("Stall").transform.position += Vector3.up * currentYMove;

        List<Minigame> currentMinigames = new();

        //Choose randomly next minigame
        int nextGameId = Random.Range(0,minigames.Count); 

        currentMinigamePrefab = minigames[nextGameId];
        GameObject game = minigames[nextGameId].minigamePrefab;

        //Set timer visualization
        Timer timer = FindObjectOfType<Timer>();
        timer.SetTime(waitTimeBeforeMinigameStart);

        //Show minigame intro text, wait
        TMP_Text introText = GameObject.FindGameObjectWithTag("IntroText").GetComponent<TMP_Text>();
        introText.text = minigames[nextGameId].introText;
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
        

        StartCoroutine(PlayGame(currentMinigames,minigameStartTime));
    }

    IEnumerator PlayGame(List<Minigame> minigames, float minigameStartTime){
        Timer timer = FindObjectOfType<Timer>();
        timer.SetTime(maxMinigamePlayTimeSeconds);
        skipMinigame = false;

        //Play for set time
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

    void EndMinigame(List<Minigame> minigames){
        //Count score
        Results gameResults = new();
        gameResults.results = new int[numberOfPlayers];
        gameResults.icon = currentMinigamePrefab.icon;
        for(int i=0;i<numberOfPlayers;i++){
            for(int j=0;j<numberOfPlayers;j++){
                if(i==j) continue;
                if(minigames[i].score>minigames[j].score){
                    gameResults.results[j]++;
                }
            }
        }
        resultsHistory.Add(gameResults);
        
        //Show score
        if(this.minigames.Count>0){
            SceneManager.LoadScene("BetweenMinigames");
        } else {
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void QuitToMenu(){
        //Restore list to show all minigames for next play
        minigames = new List<MinigamePrefabSO>(originalMinigamesPrefabs);
        resultsHistory = new();
        SceneManager.LoadScene("Menu");
    }
}
