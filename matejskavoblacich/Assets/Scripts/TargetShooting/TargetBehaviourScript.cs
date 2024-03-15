using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using initi.prefabScripts;

public class TargetBehaviourScript : BaseHittable
{
    public override void Hit(Vector2 hitPosition)
    {
        Destroy(gameObject);
    }
}
