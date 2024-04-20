using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource audioSource;
    public bool isInQueue = false;

    void Start (){
        audioSource = GetComponent<AudioSource>();
    }
    public void OnCollisionEnter2D(Collision2D coll){
        //Used later for playing sound
        //Debug.Log(coll.relativeVelocity.magnitude);
        audioSource.volume = Mathf.Clamp(coll.relativeVelocity.magnitude/10,0,1);
        audioSource.Play();
    }
}
