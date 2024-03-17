using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoundPipe : BasePipe
{
    public override void CalculateRotation()
    {
        base.CalculateRotation();
        Vector2 vec = new Vector2(direction1.x + direction2.x, direction1.y + direction2.y);
    }
}
