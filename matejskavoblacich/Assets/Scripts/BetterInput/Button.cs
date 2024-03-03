using UnityEngine;
using UnityEngine.Events;

public class Button : BaseHoldable
{
    [Header("Button properties")]
    [SerializeField, Tooltip("If true, OnClick is called only once even when holding for long time")] bool clickOnce = false;
    [Header("OnClick event")]
    [SerializeField] UnityEvent onClick;

    bool clickedThisHold = false;
    public void Start(){
        minTimeBeforeHold = 0.3f;
        maxTimeBetweenClicks = 0.4f;
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
    }
}
