using UnityEngine;

/// <summary>
/// Functions for buttons in BetweenMinigames and FinalScene scenes
/// </summary>
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

    public void SkipToFinalScene(){
        gameMaster.SkipToFinalScene();
    }
}
