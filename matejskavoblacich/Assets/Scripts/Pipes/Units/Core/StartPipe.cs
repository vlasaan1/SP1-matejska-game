using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPipe : BaseCore
{
    public override void CalculateRotation()
    {
        if(outDir.x == -1)
            transform.Rotate(Vector3.forward, 90f);
        else if(outDir.x == 1)
            transform.Rotate(Vector3.forward, 270f);
        else if(outDir.y == 1)
            transform.Rotate(Vector3.forward, 180f);
        rotation = transform.rotation.z;
    }
}
