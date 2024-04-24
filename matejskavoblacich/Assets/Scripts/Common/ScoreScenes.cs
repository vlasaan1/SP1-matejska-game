using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScenes : MonoBehaviour
{
    MainGameMaster gameMaster;

    void Start(){
        gameMaster = FindObjectOfType<MainGameMaster>();
    }

    public void LoadNextMinigame(){
        gameMaster.ChangeState(MainGameMaster.GameState.LoadMinigame);
    }

    public void GoToMenu(){
        gameMaster.ChangeState(MainGameMaster.GameState.Menu);
    }
}
