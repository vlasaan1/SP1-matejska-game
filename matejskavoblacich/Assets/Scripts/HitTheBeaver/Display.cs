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
    List<SpriteRenderer> spriteRenderer;
    [SerializeField] List<Sprite> spriteList;

    int displayedHearts;
    Vector3 startPosition;

    void Start()
    {
        spriteRenderer = new List<SpriteRenderer>();
        activeHearts = new List<GameObject>(); // Initialize activeHearts before using it
        Vector3 startPosition = transform.position + new Vector3(-1.38f,2.35f,0f);
        scoreText.text = minigame.score.ToString("000000");;
        displayedHearts = Playerhealth.GetHealth();

        //based on max health level generate heart object representing health
        for(int i = 0; i<Playerhealth.GetHealth(); i++){
            GameObject heart = Instantiate(
                lifePrefab,
                startPosition + new Vector3(i * 0.55f, 0f, 0f),
                Quaternion.identity,
                transform
            );
            spriteRenderer.Add( heart.GetComponentInChildren<SpriteRenderer>() );
            spriteRenderer[spriteRenderer.Count()-1].sprite = spriteList[3-displayedHearts];
            activeHearts.Add(heart);
        }
    }
    /// <summary>
    /// Displays current score of a player and number of hearts representing the health.
    /// </summary>
    void Update()
    {
        scoreText.text = minigame.score.ToString("000000");;
        if(displayedHearts > Playerhealth.GetHealth() ){
            if (activeHearts.Count > 0){
                MoveHeart(activeHearts.ElementAt(displayedHearts-1), displayedHearts);
                displayedHearts--;
            }
            for(int i = 0; i<displayedHearts; i++){
                spriteRenderer[i].sprite = spriteList[3-displayedHearts];
            }
        }
    }

/// <summary>
/// Animates the destruction and disappearance of a heart, when the health is decreased.
/// </summary>
/// <param name="heart">Heart object to be destroyed.</param>
/// <param name="displayedNum">Number of displayed hearts, used for position of to be destroyed heart.</param>
    void MoveHeart(GameObject heart, int displayedNum ){
        heart.GetComponentInChildren<Animator>().SetTrigger("Fall");
        Destroy(heart,1f);
        activeHearts.RemoveAt(displayedNum-1);
    }
}
