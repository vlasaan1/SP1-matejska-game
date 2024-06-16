using UnityEngine;

/// <summary>
/// Main menu
/// </summary>
public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseNumberOfPlayers;

    MainGameMaster gameMaster;
    int playOnlyOneGameId = -1; //Used to load a single minigame
    void Start(){
        gameMaster = FindObjectOfType<MainGameMaster>();
        mainMenu.SetActive(true);
        chooseNumberOfPlayers.SetActive(false);
    }

    /// <summary>
    /// Called from Play button
    /// </summary>
    public void PlayGame(){
        mainMenu.SetActive(false);
        chooseNumberOfPlayers.SetActive(true);
    }

    /// <summary>
    /// Called from choose number of players, starts minigames with that number of players
    /// </summary>
    public void SetNumberOfPlayers(int numberOfPlayers){
        gameMaster.SetNumberOfPlayers(numberOfPlayers);
        if(playOnlyOneGameId==-1){
            gameMaster.ChangeState(MainGameMaster.GameState.LoadMinigame);
        } else {
            gameMaster.PlaySingleMinigame(playOnlyOneGameId);
        }
    }


    public void ShowLeaderboard(){
        gameMaster.ChangeState(MainGameMaster.GameState.ShowLeaderboard);
    }

    public void QuitGame(){
        Application.Quit();
    }

    /// <summary>
    /// Load singe minigame
    /// </summary>
    /// <param name="id"> Index of minigame in MainGameMaster minigames array </param>
    public void LoadOneGame(int id){
        playOnlyOneGameId = id;
        PlayGame();
    }
}
