using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingThing : BaseHoldable
{
    [SerializeField] Collider2D baseCollider;
    [SerializeField, Tooltip("Increase size of this if object is often left behind during moving")] Collider2D movementCollider;
    [HideInInspector] public bool isHeld = false;

    Vector3 moveDirection;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;


    [SerializeField] TextMeshPro colliderSizeText;

    public void Awake(){
        movementCollider.enabled = false;
    }
    protected override void OnHold(Vector2 hitPosition)
    {
        //Do not start holding new things when already holding something
        if(!isHeld){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,transform.localScale.x/2);
            foreach(Collider2D coll in colliders){
                if(coll.gameObject.GetInstanceID()==gameObject.GetInstanceID() || coll.gameObject.CompareTag("MissEffectCollider"))
                {
                    continue;
                }
                if(coll.gameObject.TryGetComponent<MovingThing>(out MovingThing other)){
                    if(other.isHeld){
                        return;
                    }
                }
            }
            //Start holding
            isHeld = true;
            //baseCollider.enabled = false;
            movementCollider.enabled = true;
            lastFrameCount = Time.frameCount;
        //Do not set movement variables twice in same frame for hits that hit both colliders
        } else if(lastFrameCount != Time.frameCount) {
            //Is already holding -> move 
            moveDirection = (Vector3)hitPosition - transform.position;
            deltaFrame = Time.frameCount - lastFrameCount;
            lastFrameCount = Time.frameCount;
            currentMovingFrame = 0;
        }
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        isHeld = false;
        moveDirection = Vector3.zero;
        movementCollider.enabled = false;
        baseCollider.enabled = true;
    }

    public void Update(){
        if(currentMovingFrame<=deltaFrame){
            transform.Translate(moveDirection/deltaFrame);
            currentMovingFrame++;
        }


        colliderSizeText.text = gameObject.GetComponent<BoxCollider2D>().size.x.ToString();
    }
}
