using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Timer in every minigame
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField] Slider timer;
    bool updateTimer = false;
    float timeLen;
    float endTime;

    public void SetTime(float duration){
        timeLen = duration;
        endTime = Time.time + timeLen;
        updateTimer = true;
    }

    void Update(){
        if(updateTimer){
            float currentTime = Time.time;
            if(currentTime>endTime){
                timer.value = 0;
                updateTimer = false;
                return;
            }
            timer.value = (endTime-currentTime) / timeLen;
        }
    }
}
