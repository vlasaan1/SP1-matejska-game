using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;
using System.Globalization;


public class HoleActivation : BaseHittable
{


    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float moveSpeed = 0.5f;
    GameObject activeBeaver;
    Vector3 startPosition;
    Vector3 endPosition; 
    //0-not active , 1-up , 2-down , 3-destroy with points , 4-destroy with points
    int onShowBeaver = 0;
    //used for probability of enemy
    int number = 0;
    [SerializeField] int upperBound = 5;
    Lives playerhealth;
    ScoreKeeper score;
    int changed = 0;


    void Awake()
    {
        startPosition = transform.position + new Vector3(0f, 0.28f, 0f);
        endPosition = startPosition + new Vector3(0f, 0.72f, 0f);
        playerhealth = FindObjectOfType<Lives>();
        score = FindObjectOfType<ScoreKeeper>();

    }

    void Update()
    {
        if(onShowBeaver!=0){
            if(activeBeaver==null && changed==0){
                onShowBeaver = 3;
                changed = 1;
                if(number == upperBound){
                    //hit - decrease health
                    playerhealth.DecreseHealth(10);
                    int num = playerhealth.GetHealth();
                    Debug.Log("Num");
                    Debug.Log(num);    
                }
                else{
                    //hit - add points
                    score.ModifyScore(50);
                    int s = score.GetScore();
                    Debug.Log("Score");
                    Debug.Log(s);
                }
            }
            else if(onShowBeaver==1){
                FollowPathUp();
            }
            else if(onShowBeaver==2){
                FollowPathDown();
            }
        }
    }

    public void showBeaver(){
        onShowBeaver = 1;
        changed = 0;
        number = Random.Range(1,upperBound+1);
        if(number<upperBound){
            activeBeaver = Instantiate(
                playerPrefab,
                startPosition,
                Quaternion.identity,
                transform
            );
        }
        else{
            activeBeaver = Instantiate(
                enemyPrefab,
                startPosition,
                Quaternion.identity,
                transform
            );
        }
    }

    void  FollowPathUp()
    {
        if (Vector3.Distance(activeBeaver.transform.position, endPosition) > 0.01f)
        {
            Vector3 targetPosition = endPosition;
            float delta = moveSpeed * Time.deltaTime;
            activeBeaver.transform.position = Vector2.MoveTowards(activeBeaver.transform.position, targetPosition, delta);
        }
        else{
            onShowBeaver = 2;
            FollowPathDown();
        }
    }
    
    void FollowPathDown()
    {
        if (Vector3.Distance(activeBeaver.transform.position, startPosition) > 0.01f)
        {
            Vector3 targetPosition = startPosition;
            float delta = moveSpeed * Time.deltaTime;
            activeBeaver.transform.position = Vector2.MoveTowards(activeBeaver.transform.position, targetPosition, delta);
        }
        else{
            onShowBeaver = 0;
            Destroy(activeBeaver);
        }
    }

}
