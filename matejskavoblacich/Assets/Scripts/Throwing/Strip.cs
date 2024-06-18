using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Handles animation of sling strips in basketball minigame
///</summary>
public class Strip : MonoBehaviour
{
    [SerializeField] Transform baseBallPosition;
    [SerializeField] List<LineRenderer> renderers;
    [SerializeField] List<Transform> baseStripPositions;

    //Used for calculations
    Vector3 moveDirection;
    Vector3 finalPos;
    int deltaFrame=0;
    int currentMovingFrame=1;

    void Start(){
        ResetStrip();
    }

    /// <summary>
    /// Sets strips to base position
    /// </summary>
    public void ResetStrip(){
        for(int i=0;i<renderers.Count;i++){
            renderers[i].SetPosition(0,baseStripPositions[i].position);
            renderers[i].SetPosition(1,baseBallPosition.position);
        }
    }

    /// <summary>
    /// Sets strips to targetPos
    /// </summary>
    /// <param name="currentPos"> Used to make sure the strip sits perfectly on the ball </param>
    /// <param name="targetPos"> Target position </param>
    /// <param name="deltaFrame"> Movement is split into deltaFrames frames, to make it continuous </param>
    public void SetStrip(Vector3 currentPos,Vector3 targetPos,int deltaFrame){
        //make sure it is correctly with the ball
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
        //Move strips
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
