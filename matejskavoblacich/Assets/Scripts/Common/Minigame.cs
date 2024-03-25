using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public float startTime;
    public float endTime;
    //public Score score;

    virtual public void StartMinigame(){}

    virtual public void EndMinigame(){}

    virtual public int[] GetResults(){return null;}

    public bool isFinished = false;
}