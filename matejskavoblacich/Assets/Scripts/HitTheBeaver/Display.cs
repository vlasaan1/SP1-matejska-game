using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("Health")]
     [SerializeField] GameObject lifePrefab;
     [SerializeField] Lives Playerhealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Minigame minigame;

    List<GameObject> activeHearts;

    int displayedHearts;
    Vector3 startPosition;
    //x=-2 y=1 z=0


    void Start()
    {
        activeHearts = new List<GameObject>(); // Initialize activeHearts before using it
        Vector3 startPosition = transform.position + new Vector3(-2f,2.35f,0f);
        scoreText.text = minigame.score.ToString("000000");;
        displayedHearts = Playerhealth.GetHealth();
        
        for(int i = 0; i<Playerhealth.GetHealth(); i++){
            GameObject heart = Instantiate(
                lifePrefab,
                startPosition + new Vector3(i * 0.8f, 0f, 0f),
                Quaternion.identity,
                transform
            );
            activeHearts.Add(heart);
        }
    }
    void Update()
    {
        scoreText.text = minigame.score.ToString("000000");;
        if(displayedHearts > Playerhealth.GetHealth() ){
            if (activeHearts.Count > 0){
                    Destroy(activeHearts.ElementAt(displayedHearts-1));
                    activeHearts.RemoveAt(displayedHearts-1);
            }
            displayedHearts--;

        }
    }
}
