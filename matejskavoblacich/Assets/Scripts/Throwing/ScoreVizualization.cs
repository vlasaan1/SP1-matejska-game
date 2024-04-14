using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVizualization : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] Minigame minigame;

    void Update()
    {
        score.text = minigame.score.ToString();
    }
}
