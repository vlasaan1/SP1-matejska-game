using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipMinigame : MonoBehaviour
{
    public void Skip(){
        FindObjectOfType<MainGameMaster>().SkipMinigame();
    }
}
