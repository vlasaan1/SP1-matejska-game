using System.Collections.Generic;
using initi.prefabScripts;
using UnityEngine;

/// <summary>
/// Ball logic from basketball minigame
/// </summary>
public class Ball : MonoBehaviour
{
    [SerializeField] List<AudioClip> ballSoundClips;
    [SerializeField] List<Sprite> ballSprites;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    public bool isInQueue = false;
    bool bonusBall = false;

    void Start (){
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ballSprites[0];
    }
    public void OnCollisionEnter2D(Collision2D coll){
        audioSource.volume = Mathf.Clamp(coll.relativeVelocity.magnitude/15,0,1);
        audioSource.clip = ballSoundClips[Random.Range(0,ballSoundClips.Count)];
        audioSource.Play();
    }

    public void TurnOnBonus(){
        bonusBall = true;
        spriteRenderer.sprite = ballSprites[1];
    }

    public void TurnOffBonus(){
        if(bonusBall){
            bonusBall = false;
            spriteRenderer.sprite = ballSprites[0];
        }
    }

    public bool IsBonus(){
        return bonusBall;
    }
}
