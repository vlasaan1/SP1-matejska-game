using UnityEngine;

public class Minigame : MonoBehaviour
{
    public float startTime;
    public float endTime;
    public int score;
    public bool isFinished = false;

    virtual public void StartMinigame(){}

    virtual public void EndMinigame(){}

}