using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    [SerializeField] Minigame minigame;
    [SerializeField] Transform movementZone;
    [SerializeField] float movementSpeed = 3;
    float nextHitTime = 0;
    float delayBetweenPoints = 0.3f;
    float shotFromUnderdelay = 1f;
    Vector3 moveDir;
    float minX;
    float maxX;

    Vector3 target;

    void Start(){
        target = transform.position;
        minX = movementZone.position.x - movementZone.localScale.x/2;
        maxX = movementZone.position.x + movementZone.localScale.x/2;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(Time.time<nextHitTime) return;
        if(other.gameObject.CompareTag("Ball")){
            if(other.TryGetComponent(out Rigidbody2D rb)){
                if(rb.velocity.y>=0){
                    nextHitTime = Time.time + shotFromUnderdelay;
                    return;
                }
                minigame.score+=1;
                nextHitTime = Time.time + delayBetweenPoints;
            }
        }
    }

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
