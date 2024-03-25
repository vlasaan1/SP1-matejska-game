using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEditor.EditorTools;

public class MainGameMaster : MonoBehaviour
{
    [SerializeField] GameObject initiInput;
    [SerializeField,Tooltip("Names of scenes with minigames")] string[] minigames;

    int numberOfPlayers;
    

    void Start(){
        //Dont destroy on load, destroy if exists
        if(FindObjectsByType<MainGameMaster>(FindObjectsSortMode.None).Length > 1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //Instantiate initiInput only once
        Instantiate(initiInput);
    }

    public void SetNumberOfPlayers(int num){
        numberOfPlayers = num;
    }

    public int GetNumberOfPlayers(){
        return numberOfPlayers;
    }

    public void StartGame(){
       
    }

   

    void EndMinigame(){

    }

    void QuitToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
