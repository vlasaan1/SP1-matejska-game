using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;
using System.Globalization;

using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor;


public class HoleActivation : MonoBehaviour
{

    [SerializeField] Minigame minigame;
    [SerializeField] List<GameObject> playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public float moveSpeed = 0.5f;
    float moveSpeedIncrease = 0.5f;
    float moveSpeedMax = 2.5f;
    [SerializeField] Lives playerhealth;
    float timeTotal;
    float percentageIncrease = 0.2f;
    float percentage;

    GameObject activeBeaver;
    Vector3 startPosition;
    Vector3 endPosition; 

    //0-not active , 1-up , 2-down , 3-destroy with points , 4-destroy with points
    public int onShowBeaver = 0;
    //variables used for probability of enemy and beaver spawned
    int beaverMinNum = 2;
    //int enemyMaxNum = 2;
    //int beaverMaxNum = 10;
    int number = 0;
    //used for generating objects(deciding factor of type)
    [SerializeField] int upperBound = 5; 
    private System.Random random;
    int changed = 0; //track hit

    void Awake()
    {
        percentage = percentageIncrease;
        timeTotal = minigame.endTime - minigame.startTime;
        startPosition = transform.position + new Vector3(0f, -0.1f, 0f);
        endPosition = startPosition + new Vector3(0f, 1f, 0f);
        random = new System.Random(minigame.seed);
    }

    void Update()
    {
        //increase speed of object based on percentage time passed
        if(((Time.time-minigame.startTime) > percentage*timeTotal)&&(moveSpeed<moveSpeedMax)){
            percentage += percentageIncrease;
            moveSpeed += moveSpeedIncrease;
        }
        //if given time passed, end the game
        if(Time.time > minigame.endTime){
            minigame.isFinished = true;
        }
        //if player lost all lives, end the game
        if(!playerhealth.GetState()){
            minigame.isFinished = true;
        }
        //if a beaver was generated
        if(onShowBeaver!=0){
            //based on hit, adjust score
            if(activeBeaver==null && changed==0){
                onShowBeaver = 0;
                changed = 1;
                if(number == upperBound){
                    //hit - decrease health
                    playerhealth.DecreseHealth(1);
                }
                else{
                    //hit - add points
                    minigame.score += 10;
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

/// <summary>
/// Instantiates an object in the game in the current hole. Based on generated variable number it is going to be a player or enemy.
/// </summary>
/// <param name="previousEnemies"> keeps track of number of enemies generated </param>
/// <param name="previousBeavers"> keeps track of number of beavers(players balloons) generated </param>
    public void showBeaver(ref int previousEnemies, ref int previousBeavers){
        onShowBeaver = 1;
        changed = 0;
        number = random.Next(1,upperBound+1);
        if( (number<upperBound)  || (previousBeavers<beaverMinNum)) {
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

/// <summary>
/// Moves object in the hole upwards in a given trajectory.
/// </summary>
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
    
/// <summary>
/// Moves object in the hole downwards in a given trajectory.
/// </summary>
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
