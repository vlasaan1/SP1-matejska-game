using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] TMP_Text toggleMinigameButtonsText;
    [SerializeField] GameObject stall;
    MainGameMaster gameMaster;
    // Start is called before the first frame update
    void Awake()
    {
        gameMaster = FindObjectOfType<MainGameMaster>();
    }

    void OnEnable(){
        if(gameMaster.showSingleMinigameButtons){
            toggleMinigameButtonsText.text = "Skrýt tlačítka \npro jednotlivé minihry";
        } else {
            toggleMinigameButtonsText.text = "Zobrazit tlačítka \npro jednotlivé minihry";
        }
        ChangeStallOffset(0);
    }

    public void ToggleSingleMinigameButtons(){
        bool enable = !gameMaster.showSingleMinigameButtons;
        gameMaster.showSingleMinigameButtons = enable;
        if(enable){
            toggleMinigameButtonsText.text = "Skrýt tlačítka \npro jednotlivé minihry";
        } else {
            toggleMinigameButtonsText.text = "Zobrazit tlačítka \npro jednotlivé minihry";
        }
    }

    public void ChangeStallOffset(float change){
        gameMaster.AddYMove(change);
        stall.transform.position = new Vector3(0,gameMaster.GetYMove(),0);
    }
}
