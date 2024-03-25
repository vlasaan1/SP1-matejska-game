using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    float nextHitTime = 0;
    float delayBetweenPoints = 0.3f;
    [SerializeField] ThrowingScore score;
    void OnTriggerExit2D(Collider2D other){
        if(Time.timeSinceLevelLoad<nextHitTime) return;
        if(other.gameObject.CompareTag("ball")){
            if(other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.velocity.y<0){
                Debug.Log("SCORE");
                score.points+=1;
                nextHitTime = Time.timeSinceLevelLoad + delayBetweenPoints;
            }
        }
    }
}
