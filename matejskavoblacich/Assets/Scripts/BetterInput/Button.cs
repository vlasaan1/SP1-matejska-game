using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : BaseHoldable
{
    [SerializeField] SpriteRenderer sprite;

    [Header("Button properties")]
    [SerializeField, Tooltip("If true, OnClick is called only once even when holding for long time")] bool clickOnce = true;

    [Header("OnClick event")]
    [SerializeField] UnityEvent onClick;

    bool clickedThisHold = false;
    bool activeVisualization = false;

    protected override void OnPress(Vector2 hitPosition)
    {
        activeVisualization = true;
        StartCoroutine(HoldVisualization());
    }

    protected override void OnHold(Vector2 hitPosition)
    {
        if(clickOnce && clickedThisHold){
            return;
        }
        onClick.Invoke();
        clickedThisHold = true;
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        clickedThisHold = false;
        activeVisualization = false;
    }

    IEnumerator HoldVisualization(){
        float holdPercent;
        while(activeVisualization){
            holdPercent = Mathf.Clamp((Time.time-firstHitTime)/minTimeBeforeHold,0,1);
            sprite.material.SetFloat("_HoldPercent",holdPercent);
            yield return new WaitForSeconds(0);
        }
        sprite.material.SetFloat("_HoldPercent",0);
    }
}
