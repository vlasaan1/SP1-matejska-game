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
            transform.Rotate(Vector3.forward, 90f);
        rotation = transform.rotation.z;
    }

}
