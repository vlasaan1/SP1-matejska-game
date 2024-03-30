using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    MainGameMaster gameMaster;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseNumberOfPlayers;

    void Start(){
        gameMaster = FindObjectOfType<MainGameMaster>();
        mainMenu.SetActive(true);
        chooseNumberOfPlayers.SetActive(false);
    }

    public void PlayGame(){
        mainMenu.SetActive(false);
        chooseNumberOfPlayers.SetActive(true);
    }

    public void SetNumberOfPlayers(int numberOfPlayers){
        gameMaster.SetNumberOfPlayers(numberOfPlayers);
        gameMaster.LoadGame();
    }

    public void QuitGame(){
        Application.Quit();
    }
}
