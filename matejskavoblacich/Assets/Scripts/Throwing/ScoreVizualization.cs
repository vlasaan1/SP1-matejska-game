using TMPro;
using UnityEngine;

/// <summary>
/// Updates score of minigame in basketball minigame
/// </summary>
public class ScoreVizualization : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] Minigame minigame;

    void Update()
    {
        score.text = minigame.score.ToString();
    }
}
