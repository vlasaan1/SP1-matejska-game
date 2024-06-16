using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesAudioManager : MonoBehaviour
{
    [Header("------------- Audio Source -------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------- Audio Clip ---------------")]
    public AudioClip move;
    public AudioClip flow;
    public AudioClip success;
    public AudioClip fail;

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

}
