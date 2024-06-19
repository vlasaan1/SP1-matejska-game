using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Net logic from basketball minigame
/// </summary>
public class Net : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Minigame minigame;
    [SerializeField] List<Sprite> sprites;
    [SerializeField,Tooltip("Net will randomly move in this zone")] Transform movementZone;

    [Header("Settings")]
    [SerializeField] float movementSpeed = 3;
    [SerializeField] int goalPoints = 50;
    [SerializeField] int goalPointsBonusMultiplier = 3;
    [SerializeField, Tooltip("Needed to make sure single ball isnt counted multiple times")] float delayBetweenPoints = 0.3f;
    [SerializeField,Tooltip("Points scored in this time after ball is thrown to the net from under doesnt count")] float shotFromUnderDelay = 1f;

    float nextHitTime = 0;
    Vector3 moveDir;
    float minX;
    float maxX;

    Vector3 target;

    void Start(){
        target = transform.position;
        minX = movementZone.position.x - movementZone.localScale.x/2;
        maxX = movementZone.position.x + movementZone.localScale.x/2;

        GetComponentInChildren<SpriteRenderer>().sprite = sprites[minigame.playerId];
    }

    void OnTriggerEnter2D(Collider2D other){
        if(Time.time<nextHitTime) return;
        if(other.gameObject.CompareTag("Ball")){
            if(other.TryGetComponent(out Rigidbody2D rb)){
                //Ball went from under, points dont count
                if(rb.velocity.y>=0){
                    nextHitTime = Time.time + shotFromUnderDelay;
                    return;
                }
                //Add points, play sound, add delay
                if(other.GetComponent<Ball>().IsBonus()){
                    minigame.score+=goalPoints*goalPointsBonusMultiplier;
                } else {
                    minigame.score+=goalPoints;
                }
                GetComponent<AudioSource>().Play();
                nextHitTime = Time.time + delayBetweenPoints;
            }
        }
    }

    /// <summary>
    /// Move net ranomly in movement zone
    /// </summary>
    void Update(){
        if((target - transform.position).magnitude < .1f){
            float x;
            if(target.x == minX) x = maxX;
            else x = minX;
            float y = Random.Range(movementZone.position.y-movementZone.localScale.y/2,movementZone.position.y+movementZone.localScale.y/2);
            target = new Vector3(x,y,0);
            moveDir = target-transform.position;
        }
        transform.Translate(movementSpeed * Time.deltaTime * moveDir);
    }
}
