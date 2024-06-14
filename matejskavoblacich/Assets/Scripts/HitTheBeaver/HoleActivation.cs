using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;
using System.Globalization;

using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor;


public class HoleActivation : BaseHittable
{

    [SerializeField] Minigame minigame;
    [SerializeField] List<GameObject> playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public float moveSpeed = 0.5f;
    [SerializeField] Lives playerhealth;
    float timeTotal;
    float percentage;

    GameObject activeBeaver;
    Vector3 startPosition;
    Vector3 endPosition; 

    //0-not active , 1-up , 2-down , 3-destroy with points , 4-destroy with points
    public int onShowBeaver = 0;
    //variables used for probability of enemy and beaver spawned
    int enemyMaxNum = 2;
    int beaverMaxNum = 10;
    //int beaverMinNum = 2;
    int number = 0;
    [SerializeField] int upperBound = 5;
    private System.Random random;
    int changed = 0;

    void Awake()
    {
        moveSpeed = 0.5f;
        percentage = 0.2f;
        //timeTotal = 60f;
        timeTotal = minigame.endTime - minigame.startTime;
        startPosition = transform.position + new Vector3(0f, -0.1f, 0f);
        endPosition = startPosition + new Vector3(0f, 1f, 0f);
        random = new System.Random(minigame.seed);
    }

    void Update()
    {
        if(((Time.time-minigame.startTime) > percentage*timeTotal)&&(moveSpeed<2.5f)&&(percentage<100f)){
            percentage += 0.2f;
            moveSpeed += 0.3f;
        }
        if(Time.time > minigame.endTime){
            minigame.isFinished = true;
        }
        if(!playerhealth.GetState()){
            minigame.isFinished = true;
        }
        if(onShowBeaver!=0){
            if(activeBeaver==null && changed==0){
                onShowBeaver = 0;
                changed = 1;
                if(number == upperBound){
                    //hit - decrease health
                    playerhealth.DecreseHealth(1);
                }
                else{
                    //hit - add points
                    minigame.score += 25;
                }
                ///
            }
            else if(onShowBeaver==1){
                FollowPathUp();
            }
            else if(onShowBeaver==2){
                FollowPathDown();
            }
        }
    }

    public void showBeaver(ref int previousEnemies, ref int previousBeavers){
        onShowBeaver = 1;
        changed = 0;
        number = random.Next(1,upperBound+1);
        if( ((number<upperBound) || (previousEnemies==enemyMaxNum)) && (previousBeavers!=beaverMaxNum)) {
            previousEnemies = 0;
            number = 1; //when rng needs to be altered
            previousBeavers++;
            activeBeaver = Instantiate(
                playerPrefab[minigame.playerId],
                startPosition,
                Quaternion.identity,
                transform
            );
        }
        else{
            previousBeavers = 0;
            previousEnemies++;
            number = upperBound; //when rng needs to be altered especially when altering outcome
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
