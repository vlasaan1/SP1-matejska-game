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
            transform.Rotate(Vector3.forward, 90f);
        else if(vec.x == -1 && vec.y == -1)
            transform.Rotate(Vector3.forward, 180f);
        else if(vec.x == -1 && vec.y == 1)
            transform.Rotate(Vector3.forward, 270f);
        rotation = transform.rotation.z;
    }

}
