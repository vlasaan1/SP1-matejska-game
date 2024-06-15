using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strip : MonoBehaviour
{
    [SerializeField] Transform baseBallPosition;
    [SerializeField] List<LineRenderer> renderers;
    [SerializeField] List<Transform> baseStripPositions;

    Vector3 moveDirection;
    Vector3 finalPos;
    int deltaFrame=0;
    int currentMovingFrame=1;

    void Start(){
        ResetStrip();
    }

    public void ResetStrip(){
        for(int i=0;i<renderers.Count;i++){
            //Add (0,0,-1) to make sure the strip is in front of the sling
            renderers[i].SetPosition(0,baseStripPositions[i].position);// + new Vector3(0,0,-1));
            renderers[i].SetPosition(1,baseBallPosition.position);// + new Vector3(0,0,-1));
        }
    }

    public void SetStrip(Vector3 currentPos,Vector3 targetPos,int deltaFrame){
        //Just to make sure it is correctly with the ball
        for(int i=0;i<renderers.Count;i++){
            renderers[i].SetPosition(1,currentPos);
        }
        this.deltaFrame = deltaFrame;
        finalPos = targetPos;
        moveDirection = finalPos - currentPos;
        currentMovingFrame = 0;
    }

    void Update()
    {
        if(currentMovingFrame<deltaFrame){
            for(int i=0;i<renderers.Count;i++){
                renderers[i].SetPosition(1,renderers[i].GetPosition(1)+moveDirection/deltaFrame);
            }
            currentMovingFrame++;
        } else if(currentMovingFrame == deltaFrame){
            for(int i=0;i<renderers.Count;i++){
                renderers[i].SetPosition(1,finalPos);
            }
            currentMovingFrame++;
        }
    }
}
