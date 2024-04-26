using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScreenKeyboardController : MonoBehaviour {
    [SerializeField]
    private TextMeshPro inputField;
    public UnityEvent<string> returnName;

    private string teamName = "";
    private readonly int maxTeamNameLength = 18;

    /// <summary>
    ///     Adds character to team name
    /// </summary>
    public void KeyPress(char c) {
        if (teamName.Length >= maxTeamNameLength) {
            return;
        }
        teamName += c;
        UpdateName();
    }

    /// <summary>
    ///     Removes last character
    /// </summary>
    public void RemoveChar() {
        if(teamName.Length > 0)
            teamName = teamName.Remove(teamName.Length - 1);
        UpdateName();
    }

    /// <summary>
    ///     Sends team name
    /// </summary>
    public void SendName() {
        if(teamName.Length > 0){
            teamName = teamName.ToUpper();
            returnName.Invoke(teamName);
            teamName = "";
            UpdateName();
        }
    }

    private void UpdateName() {
        inputField.text = teamName;
    }
}
