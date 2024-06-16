using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Shows medals for final scene, handles adding results to the leaderboard
/// </summary>
public class FinalSceneResults : BetweenMinigamesResults
{
    [Header("Final scene variables")]
    [SerializeField,Tooltip("Instance in scene")] GameObject leaderboard;
    [SerializeField,Tooltip("Instance in scene")] GameObject keyboardMenu;
    [SerializeField] GameObject text;
    [SerializeField] GameObject backToMenuButton;
    [SerializeField] float waitTimeBeforeLeaderboardPopup = 4;

    int maxPoints;

    public void FinalResults(List<Results> results){
        StartCoroutine(FinalScene(results));
    }

    IEnumerator FinalScene(List<Results> results){
        int[] pointsSum = new int[results[0].results.Length];
        for(int i=0;i<results.Count;i++){
            for(int j=0;j<results[i].results.Length;j++){
                pointsSum[j] += results[i].results[j];
            }
        }
        ShowResults(pointsSum);
        yield return new WaitForSeconds(waitTimeBeforeLeaderboardPopup);
        maxPoints = int.MinValue;
        for(int i=0;i<pointsSum.Length;i++){
            if(pointsSum[i]>maxPoints) maxPoints = pointsSum[i];
        }
        ScoreboardController scoreboard = leaderboard.GetComponent<ScoreboardController>();
        if(scoreboard.FindScoreboardPos(maxPoints) > -1){
            resultText[0].transform.parent.gameObject.SetActive(false);

            text.SetActive(false);
            keyboardMenu.SetActive(true);
            keyboardMenu.GetComponentInChildren<ScreenKeyboardController>().returnName.AddListener(AddToLeaderboard);
        } else {
            backToMenuButton.SetActive(true);
        }
        
    }

    void AddToLeaderboard(string name){
        ScoreboardController scoreboard = leaderboard.GetComponent<ScoreboardController>();
        scoreboard.AddEntry(name,maxPoints);

        resultText[0].transform.parent.gameObject.SetActive(true);

        text.SetActive(true);
        keyboardMenu.SetActive(false);
        backToMenuButton.SetActive(true);
    }
}
