using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : BaseUnit
{
    public BaseObstacle(){
        IsMoveable = false;
        inDir = new Vector2(0,0);
        outDir = new Vector2(0,0);
    }
}
