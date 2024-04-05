using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsHandler : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> players;

    [SerializeField] List<Sprite> sprites;

    void Start(){
        MainGameMaster gameMaster = FindObjectOfType<MainGameMaster>();
        ShowResults(gameMaster.GetLastResults().results);
    }

    public void ShowResults(int[] results){
        Debug.Log(results);
        for(int i=0;i<results.Length;i++){
            players[i].sprite = sprites[results[i]];
        }
    }
}
