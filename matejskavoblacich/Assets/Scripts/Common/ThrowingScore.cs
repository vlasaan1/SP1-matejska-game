using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using System;

public class ThrowingScore : MonoBehaviour
{
    public int points = 0;
    [SerializeField] TMP_Text scoreText;

    void Update(){
        scoreText.text = points.ToString();
    }

}
