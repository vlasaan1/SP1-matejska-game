using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    MainGameMaster gameMaster;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseNumberOfPlayers;
    [SerializeField] GameObject chooseHeight;

    void Start(){
        gameMaster = FindObjectOfType<MainGameMaster>();
        mainMenu.SetActive(true);
        chooseNumberOfPlayers.SetActive(false);
        chooseHeight.SetActive(false);
    }

    public void PlayGame(){
        mainMenu.SetActive(false);
        //chooseHeight.SetActive(true);
        chooseNumberOfPlayers.SetActive(true);
    }

    public void SetHeight(float height){
        // ???
        chooseHeight.SetActive(false);
        gameMaster.SetYMove(height);
        chooseNumberOfPlayers.SetActive(true);
    }

    public void SetNumberOfPlayers(int numberOfPlayers){
        gameMaster.SetNumberOfPlayers(numberOfPlayers);
        gameMaster.LoadGame();
    }

    public void ShowLeaderboard(){
        mainMenu.SetActive(false);
        //TODO
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadOneGame(int id){
        gameMaster.LoadGameById(id);
    }
}
