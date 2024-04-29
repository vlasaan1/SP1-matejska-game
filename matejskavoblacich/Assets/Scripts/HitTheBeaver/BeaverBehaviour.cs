using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;

public class BeaverBehaviour : BaseHittable
{
    [SerializeField] ParticleSystem popEffect;
    [SerializeField] AudioClip popSound;
    [SerializeField] [Range(0f,1f)] float popSoundVolume = 1f;
    //Override metody Hit - zavolá se pokud se na tento objekt klikne na stěně

    public override void Hit(Vector2 hitPosition){
        //hitPosition jsou globální souřadnice doteku
        ParticleSystem pop = Instantiate(popEffect, transform.position  - new Vector3(0f,0f,2f), Quaternion.identity);
        AudioSource.PlayClipAtPoint(popSound, new Vector3(0f,0f,0f), popSoundVolume); 
        Destroy(pop,0.5f);
        Debug.Log("played" + popEffect.transform.position);
        Destroy(gameObject);
    }
}