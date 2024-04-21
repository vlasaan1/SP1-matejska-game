using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    public bool isInQueue = false;
    bool bonusBall = false;

    void Start (){
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnCollisionEnter2D(Collision2D coll){
        audioSource.volume = Mathf.Clamp(coll.relativeVelocity.magnitude/15,0,1);
        audioSource.Play();
    }

    public void TurnOnBonus(){
        bonusBall = true;
        spriteRenderer.material.SetInt("_EnableBonus",1);
    }

    public void TurnOffBonus(){
        if(bonusBall){
            bonusBall = false;
            spriteRenderer.material.SetInt("_EnableBonus",0);
        }
    }

    public bool IsBonus(){
        return bonusBall;
    }
}
