using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StraightPipe : BasePipe
{
    public override void CalculateRotation()
    {
        if(Math.Abs(inDir.x) == 1)
            rotation = 90f;
        if(Math.Abs(inDir.y) == -1)
            rotation = 180f;
        if(Math.Abs(inDir.x) == -1)
            rotation = 270f;
        transform.Rotate(Vector3.forward, rotation);
        inOutDir();
    }

    /// <summary>
    /// sets correct outDir and inDir depanding on the rotation
    /// </summary>
    private void inOutDir(){
        switch(rotation){
            case 0f:
                inDir = new Vector2(0, 1);
                outDir = new Vector2(0,-1);
                break;
            case 90f:
                inDir = new Vector2(1,0);
                outDir = new Vector2(-1,0);
                break;
            case 180f:
                inDir = new Vector2(0,-1);
                outDir = new Vector2(0,1);
                break;
            case 270f:
                inDir = new Vector2(-1,0);
                outDir = new Vector2(1,0);
                break;
        }
    }

}
