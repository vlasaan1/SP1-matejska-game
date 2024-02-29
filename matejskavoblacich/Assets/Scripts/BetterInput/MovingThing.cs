using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThing : BaseHoldable
{
    [SerializeField] Collider2D baseCollider;
    [SerializeField] Collider2D movementCollider;
    bool isHeld = false;

    public void Awake(){
        movementCollider.enabled = false;
    }
    protected override void OnHold(Vector2 hitPosition)
    {
        if(!isHeld){
            isHeld = true;
            baseCollider.enabled = false;
            movementCollider.enabled = true;
        }
        Vector3 moveDir = new Vector3(hitPosition.x,hitPosition.y,0) - transform.position;
        if(moveDir.sqrMagnitude > 0.1f){
            transform.Translate(moveDir);
        }
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        isHeld = false;
        movementCollider.enabled = false;
        baseCollider.enabled = true;
    }
}
