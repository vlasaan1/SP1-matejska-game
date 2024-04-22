using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoundPipe : BasePipe
{
    public override void CalculateRotation()
    {
        Vector2 vec = new Vector2(inDir.x + outDir.x, inDir.y + outDir.y);
        if(vec.x == 1 && vec.y == -1 )
            rotation = 90f;
        else if(vec.x == -1 && vec.y == -1)
            rotation = 180f;
        else if(vec.x == -1 && vec.y == 1)
            rotation = 270f;
        transform.Rotate(Vector3.forward, rotation);
        inOutDir();
    }

    private void inOutDir(){
        switch(rotation){
            case 0f:
                inDir = new Vector2(0, 1);
                outDir = new Vector2(1,0);
                break;
            case 90f:
                inDir = new Vector2(1,0);
                outDir = new Vector2(0,-1);
                break;
            case 180f:
                inDir = new Vector2(0,-1);
                outDir = new Vector2(-1,0);
                break;
            case 270f:
                inDir = new Vector2(-1,0);
                outDir = new Vector2(0,1);
                break;
        }
    }

}
