using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Calculates score and show medals between minigames
/// </summary>
public class BetweenMinigamesResults : MonoBehaviour
{
    [SerializeField,Tooltip("Renderers of medals")] protected List<SpriteRenderer> resultRend;
    [SerializeField,Tooltip("Text for score number")] protected List<TMP_Text> resultText;
    [SerializeField,Tooltip("Medal sprites")] protected List<Sprite> sprites;

    /// <summary>
    /// Show medals according to results
    /// </summary>
    public void ShowResults(int[] results){
        int[] orderRes = GetOrder(results);
        for(int i=0;i<results.Length;i++){
            resultRend[i].sprite = sprites[orderRes[i]];
            resultText[i].text = results[i].ToString();
        }
    }

    /// <summary>
    /// Get order of players from their results
    /// </summary>
    /// <returns> Array, where number means oder (0=winner, 1=second place, etc.) </returns>
    protected int[] GetOrder(int[] results){
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
