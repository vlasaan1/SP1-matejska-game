using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPipe : BaseCore
{
    public override void CalculateRotation()
    {
        if(inDir.x == -1)
            transform.Rotate(Vector3.forward, 90f);
        else if(inDir.x == 1)
            transform.Rotate(Vector3.forward, 270f);
        else if(inDir.y == 1)
            transform.Rotate(Vector3.forward, 180f);
        rotation = transform.rotation.z;
    }
}
