using UnityEngine;

/// <summary>
/// Main menu
/// </summary>
public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseNumberOfPlayers;
    [SerializeField] GameObject chooseOneGame;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject clouds;
    MainGameMaster gameMaster;
    int playOnlyOneGameId = -1; //Used to load a single minigame

    void Start(){
        gameMaster = FindObjectOfType<MainGameMaster>();
        ShowMenu();
    }

    /// <summary>
    /// Called from Play button, Shows number of players selection, hides main menu
    /// </summary>
    public void PlayGame(){
        mainMenu.SetActive(false);
        chooseNumberOfPlayers.SetActive(true);
    }

    /// <summary>
    /// Called from choose number of players, starts minigames with given number of players
    /// </summary>
    public void SetNumberOfPlayers(int numberOfPlayers){
        gameMaster.SetNumberOfPlayers(numberOfPlayers);
        if(playOnlyOneGameId==-1){
            gameMaster.ChangeState(MainGameMaster.GameState.LoadMinigame);
        } else {
            gameMaster.PlaySingleMinigame(playOnlyOneGameId);
        }
    }

    /// <summary>
    /// Transitions to Leaderboard scene
    /// </summary>
    public void ShowLeaderboard(){
        gameMaster.ChangeState(MainGameMaster.GameState.ShowLeaderboard);
    }

    /// <summary>
    /// Transitions to credits scene
    /// </summary>
    public void ShowCredits(){
        gameMaster.ChangeState(MainGameMaster.GameState.ShowCredits);
    }

    /// <summary>
    /// Shows options, hides main menu and cloud edges
    /// </summary>
    public void ShowOptions(){
        mainMenu.SetActive(false);
        clouds.SetActive(false);
        options.SetActive(true);
    }

    /// <summary>
    /// Shows main menu, hides everything else
    /// </summary>
    public void ShowMenu(){
        mainMenu.SetActive(true);
        clouds.SetActive(true);
        chooseNumberOfPlayers.SetActive(false);
        chooseOneGame.SetActive(gameMaster.showSingleMinigameButtons);
        options.SetActive(false);
    }

    /// <summary>
    /// Quits whole application
    /// </summary>
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
