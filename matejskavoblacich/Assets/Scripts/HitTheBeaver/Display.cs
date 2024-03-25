using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("Health")]
     [SerializeField] Slider healthSlider;
     [SerializeField] Lives Playerhealth;

    [Header("Health")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Playerhealth = FindObjectOfType<Lives>();
    }
    void Start()
    {
        healthSlider.maxValue = Playerhealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");;
    }
    void Update()
    {
        healthSlider.value = Playerhealth.GetHealth();   
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");
    }
}
