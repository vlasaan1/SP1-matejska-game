using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsHandler : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> resultRend;
    [SerializeField] List<TMP_Text> resultText;

    [SerializeField] List<Sprite> sprites;
    [Header("Final Scene only")]
    [SerializeField] GameObject leaderboard;
    [SerializeField] GameObject keyboardMenu;
    [SerializeField] GameObject text;
    [SerializeField] GameObject backToMenuButton;
    [SerializeField] float waitTimeBeforeLeaderboardPopup = 4;

    int maxPoints;
    int numberOfPlayers;

    public void ShowResults(int[] results){
        int[] orderRes = GetOrder(results);
        numberOfPlayers = results.Length;
        for(int i=0;i<results.Length;i++){
            resultRend[i].sprite = sprites[orderRes[i]];
            resultText[i].text = results[i].ToString();
        }
    }

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

    int[] GetOrder(int[] results){
        int[] res = new int[results.Length];
        for(int i=0;i<results.Length;i++){
            for(int j=0;j<results.Length;j++){
                if(i==j) continue;
                if(results[i]>results[j]){
                    res[j]++;
                }
            }
        }
        return res;
    }

}
