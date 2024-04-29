using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField]
    private bool showOnLoad = false;

    [SerializeField, HideInInspector]
    private SBWrapper sbWrapper;
    private TextMeshPro textMeshPro;
    public readonly string defaultScoreboardFileName = "/scoreboard.json";


    private void Awake() {
        textMeshPro = GetComponent<TextMeshPro>();
        Load();
        if (showOnLoad) {
            ShowScoreboard();
        }
    }

    /// <summary>
    ///     Loads the scoreboard from a file.
    /// </summary>
    private void Load() {
        string path = Application.persistentDataPath + defaultScoreboardFileName;
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            sbWrapper = JsonUtility.FromJson<SBWrapper>(json);
            if (sbWrapper.scoreboard.Count == 0) {
                Debug.Log($"Scoreboard corrupted! Length: {sbWrapper.scoreboard.Count}");
                SetDefaultScoreboard();
            }
        } else {
            SetDefaultScoreboard();
        }
    }

    /// <summary>
    ///     Finds position at which new score should be added
    /// </summary>
    /// <returns>Position if score is high enough, -1 otherwise..</returns>
    public int FindScoreboardPos(int score) {
        for (int i = 0; i < sbWrapper.scoreboard.Count; i++) {
            if (sbWrapper.scoreboard[i].score < score) {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    ///     Adds new entry to the scoreboard if the score is high enough
    /// </summary>
    public void AddEntry(string name, int score) {
        int pos = FindScoreboardPos(score);
        if (pos == -1) {
            Debug.LogError($"Trying to add score ({score}) that is not high enough!");
            return;
        }
        sbWrapper.scoreboard.Insert(pos, new ScoreboardEntry(name, score));
        sbWrapper.scoreboard.RemoveAt(10);
        Save();
    }

    public void ShowScoreboard() {
        string text = "";
        foreach (ScoreboardEntry se in sbWrapper.scoreboard) {
            text += $"<align=left>{se.name}<line-height=0>\n<align=right>{se.score}<line-height=1em>\n";
        }
        textMeshPro.text = text;
    }

    /// <summary>
    ///     Saves the scoreboard to a file
    /// </summary>
    private void Save() {
        string path = Application.persistentDataPath + defaultScoreboardFileName;
        string json = JsonUtility.ToJson(sbWrapper);
        File.WriteAllText(path, json);
    }

    private void SetDefaultScoreboard() {
        sbWrapper.scoreboard = new List<ScoreboardEntry> {
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0),
                new ScoreboardEntry(" ", 0)
            };
    }

    [Serializable]
    private class SBWrapper {
        public List<ScoreboardEntry> scoreboard;
    }

    [Serializable]
    private class ScoreboardEntry {
        public string name;
        public int score;

        public ScoreboardEntry(string name, int score) {
            this.name = name;
            this.score = score;
        }
    }

}

