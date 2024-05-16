using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoothBehaviourScript : MonoBehaviour
{
    public GameObject target;
    public GameObject targetClone;

    public GameObject targetBonus;

    public GameObject targetBonusClone;
    public GameObject bomb;
    public GameObject bombClone;
    [SerializeField] TMP_Text scoreText;
    public float Timer = 2f;

    public float timeToSpawn = 2f;

    public Vector3 targetPosition;

    //minigame script
    public Minigame minigame;
    



    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            targetPosition = new Vector3(Random.Range(-2.2f, 2.2f),Random.Range(-3f, 3f), -1f);

            // chance to spawn a bomb
            if (Random.Range(0, 100) < 10)
            {
                bombClone = Instantiate(bomb, transform.position + targetPosition, transform.rotation, transform) as GameObject;
                Timer = timeToSpawn;
                return;
            }
            // chance to spawn a bonus target
            if (Random.Range(0, 100) < 10)
            {
                targetBonusClone = Instantiate(targetBonus, transform.position + targetPosition, transform.rotation, transform) as GameObject;
                Timer = timeToSpawn;
                return;
            }
            // initiate as a child of the booth
            targetClone = Instantiate(target, transform.position + targetPosition, transform.rotation, transform) as GameObject;
            Timer = timeToSpawn;
        }
    }

    public void UpdateScore(int change){
        minigame.score += change;
        scoreText.text = minigame.score.ToString();
    }
}
