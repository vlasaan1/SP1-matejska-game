using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInput : BaseHoldable
{
    [SerializeField] int numberOfTiles = 8;

    bool isHeld = false;
    GameObject heldObject;
    Vector2Int heldObjectArrayPos;
    Vector3 originalPosition;
    Vector3 moveDirection;
    float minMovement = 0.2f;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;

    Vector2Int GetArrayPos(Vector2 hitPosition){
        hitPosition = transform.InverseTransformPoint(new Vector3(hitPosition.x, hitPosition.y));
        hitPosition += new Vector2(0.5f,0.5f);
        return new Vector2Int(Mathf.FloorToInt(hitPosition.x*numberOfTiles),numberOfTiles-1-Mathf.FloorToInt(hitPosition.y*numberOfTiles));
    }

    protected override void OnHold(Vector2 hitPosition)
    {
        if(!isHeld){
            //Start holding
            heldObjectArrayPos = GetArrayPos(hitPosition);
            //if(!CanHold(heldObjectArrayPos)) return;
            //heldObject = GetObject(heldObjectArrayPos);
            originalPosition = heldObject.transform.position;
            isHeld = true;
        } else {
            //Is already holding -> move 
            moveDirection = (Vector3)hitPosition - heldObject.transform.position;
            //Stop jittering due to input accuracy
            if(moveDirection.magnitude < minMovement){
                moveDirection = Vector3.zero;
            }
        }

        deltaFrame = Time.frameCount - lastFrameCount;
        lastFrameCount = Time.frameCount;
        currentMovingFrame = 0; 
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        isHeld = false;
        moveDirection = Vector3.zero;
        heldObject.transform.position = originalPosition;
        //swap(heldObjectArrayPos,GetArrayPos(hitPosition));
    }

    public void Update(){
        if(isHeld && currentMovingFrame<=deltaFrame){
            heldObject.transform.Translate(moveDirection/deltaFrame);
            currentMovingFrame++;
        }
    }
}
