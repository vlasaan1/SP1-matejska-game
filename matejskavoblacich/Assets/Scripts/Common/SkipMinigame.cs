using UnityEngine;

/// <summary>
/// Function for skip minigame button
/// </summary>
public class SkipMinigame : MonoBehaviour
{
    public void Skip(){
        FindObjectOfType<MainGameMaster>().SkipMinigame();
    }
}
