using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using initi.prefabScripts;
using UnityEngine.UIElements;

public class AudioPlayer : BaseHittable
{

    [SerializeField] AudioClip popSound;
    [SerializeField] AudioClip enemyPopSound;
    [SerializeField] [Range(0f,1f)] float popSoundVolume = 1f;


    public void PlayPopSound()
    {
        PlayClip(popSound, popSoundVolume); 
    }

    public void PlayEnemyPopSound()
    {
        PlayClip(enemyPopSound, popSoundVolume); 
    }

    void PlayClip(AudioClip sound, float volume){
        if(sound!=null) {
            AudioSource.PlayClipAtPoint(enemyPopSound, new Vector3(0f,0f,0f), volume); 
        }
    }
}
