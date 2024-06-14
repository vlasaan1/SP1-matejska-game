using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PipeInput : BaseHoldable
{
    [SerializeField] PlayerGrid grid;
    [SerializeField, Range(1,2)] float heldPipeSizeMultiplier = 1.2f;
    [SerializeField] GameMaster gameMaster;
    int numberOfTiles = -1;
    bool isHeld = false;
    GameObject heldObject;
    Vector2Int heldObjectArrayPos;
    Vector3 originalPosition;
    Vector3 moveDirection;
    Vector3 originalScale;
    float minMovement = 0.1f;
    int deltaFrame = 1;
    int lastFrameCount = 0;
    int currentMovingFrame = 0;

    void Awake(){
        numberOfTiles = gameMaster.FieldSize;
    }

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
            if(!grid.CanGetTileAtPosition(heldObjectArrayPos)) return;
            heldObject = grid.GetTileAtPosition(heldObjectArrayPos).occupiedUnit.gameObject;
            originalPosition = heldObject.transform.position;
            originalScale  = heldObject.transform.localScale;
            heldObject.transform.localScale = originalScale*heldPipeSizeMultiplier;
            isHeld = true;
        } else {
            //Is already holding -> move 
            moveDirection = (Vector3)hitPosition - heldObject.transform.position;
            //Stop jittering due to input accuracy
            if(moveDirection.magnitude < minMovement){
                moveDirection = Vector3.zero;
            }
            //Swap instantly
            Vector2Int currentArrayPos = GetArrayPos(hitPosition);
            if(currentArrayPos != heldObjectArrayPos){
                if(grid.CanGetTileAtPosition(currentArrayPos)){
                    originalPosition = grid.GetTileAtPosition(currentArrayPos).occupiedUnit.gameObject.transform.position;
                    Vector3 currentPosition = heldObject.transform.position;
                    grid.SwapTiles(heldObjectArrayPos,currentArrayPos);
                    heldObjectArrayPos = currentArrayPos;
                    heldObject.transform.position = currentPosition;
                }
            }
        }

        deltaFrame = Time.frameCount - lastFrameCount;
        lastFrameCount = Time.frameCount;
        currentMovingFrame = 0; 
    }

    protected override void OnRelease(Vector2 hitPosition)
    {
        if(!isHeld) return;
        isHeld = false;
        moveDirection = Vector3.zero;
        heldObject.transform.position = originalPosition;
        heldObject.transform.localScale = originalScale;
    }

    public void Update(){
        if(isHeld && currentMovingFrame<=deltaFrame){
            heldObject.transform.position = heldObject.transform.position + (moveDirection/deltaFrame);
            currentMovingFrame++;
        }
    }
}
