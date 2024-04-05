using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    float nextHitTime = 0;
    float delayBetweenPoints = 0.3f;
    float shotFromUnderdelay = 1f;
    [SerializeField] Minigame minigame;
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
}
