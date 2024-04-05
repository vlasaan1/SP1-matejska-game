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
        gameMaster.LoadGame();
    }

    public void GoToMenu(){
        gameMaster.QuitToMenu();
    }
}
