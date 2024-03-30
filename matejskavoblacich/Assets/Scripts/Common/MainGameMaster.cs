using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMaster : MonoBehaviour
{
    [SerializeField] GameObject initiInput;
    [SerializeField] List<GameObject> minigamesPrefabs;
    [SerializeField] List<Vector3> playerPositions;
    [SerializeField,Tooltip("Time between loading new scene and starting minigame")] float waitTimeBeforeMinigameStart = 3;
    [SerializeField] float maxMinigamePlayTimeSeconds = 90;

    //List<Game1(pl1,pl2,pl3),Game2(...),...>
    List<int[]> resultsHistory;
    int numberOfPlayers;

    List<GameObject> originalMinigamesPrefabs;

    void Start(){
        //Dont destroy on load, destroy if exists
        if(FindObjectsByType<MainGameMaster>(FindObjectsSortMode.None).Length > 1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        //Instantiate initiInput only once
        Instantiate(initiInput);

        //Save all minigames, to be able to restore it after changes
        originalMinigamesPrefabs = new List<GameObject>(minigamesPrefabs);
    }

    public void SetNumberOfPlayers(int num){
        numberOfPlayers = num;
    }

    IEnumerator PlayGame(List<Minigame> minigames){
        //Wait before playing
        yield return new WaitForSeconds(waitTimeBeforeMinigameStart);
        float minigameStartTime = Time.time;

        //Start minigames
        foreach(Minigame game in minigames){
            game.StartMinigame();
        }

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
            if(end) break;
            //Check again later
            yield return new WaitForSeconds(0.5f);
        }

        //End minigames
        foreach(Minigame game in minigames){
            game.EndMinigame();
        }
        EndMinigame(minigames);
    }

    IEnumerator PrepareGame(){
        //Wait until next update, until new scene is prepared
        yield return new WaitUntil(() => true);
        List<Minigame> currentMinigames = new();

        //Choose randomly next minigame
        int nextGameId = Random.Range(0,minigamesPrefabs.Count); 
        for(int i=0;i<playerPositions.Count;i++){
            if(numberOfPlayers<=i) break;
            currentMinigames.Add(Instantiate(minigamesPrefabs[nextGameId],playerPositions[i],Quaternion.identity).GetComponent<Minigame>());
        }

        //Remove chosen minigame from the pool
        minigamesPrefabs.RemoveAt(nextGameId);
        StartCoroutine(PlayGame(currentMinigames));
    }

    public void LoadGame(){
        SceneManager.LoadScene("Minigame");
        StartCoroutine(PrepareGame());
    }

   

    void EndMinigame(List<Minigame> minigames){
        //Count score
        int[] gameResults = new int[numberOfPlayers];
        for(int i=0;i<minigames.Count;i++){
            int max = int.MinValue;
            int argMax=0;
            for(int j=0;j<minigames.Count;j++){
                if(minigames[j].score>max){
                    max = minigames[j].score;
                    argMax = j;
                }
            }
            gameResults[argMax] = i+1;
            minigames[argMax].score = int.MinValue;
        }
        resultsHistory.Add(gameResults);
        
        //Show score
        if(minigamesPrefabs.Count>0){
            SceneManager.LoadScene("BetweenMinigames");
        } else {
            SceneManager.LoadScene("FinalScene");
        }
    }

    void QuitToMenu(){
        //Restore list to show all minigames for next play
        minigamesPrefabs = new List<GameObject>(originalMinigamesPrefabs);
        SceneManager.LoadScene("Menu");
    }
}
