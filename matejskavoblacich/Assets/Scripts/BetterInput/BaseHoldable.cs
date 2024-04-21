using System.Collections;
using UnityEngine;
using initi.prefabScripts;
public class BaseHoldable : BaseHittable
{
    [SerializeField] protected float minTimeBeforeHold = 0.3f;
    [SerializeField] protected float maxTimeBetweenClicks = 0.4f;
    protected float firstHitTime = 0;
    protected float lastHitTime = 0;

    public override void Hit(Vector2 hitPosition){
        float timeNow = Time.time;
        
        if(timeNow - lastHitTime < maxTimeBetweenClicks){
            //Holding
            if(timeNow - firstHitTime > minTimeBeforeHold){
                OnHold(hitPosition);
            }
        } else {
            //First press
            firstHitTime = timeNow;
            OnPress(hitPosition);
        }
        lastHitTime = timeNow;
        if(gameObject.activeInHierarchy){
            StartCoroutine(TestRelease(hitPosition));
        }
    }

    private IEnumerator TestRelease(Vector2 hitPosition)
    {
        yield return new WaitForSeconds(maxTimeBetweenClicks);
        if (Time.time - lastHitTime >= maxTimeBetweenClicks)
        {
            OnRelease(hitPosition);
        }
    }

    virtual protected void OnPress(Vector2 hitPosition){}
    virtual protected void OnHold(Vector2 hitPosition){}
    virtual protected void OnRelease(Vector2 hitPosition){}
}