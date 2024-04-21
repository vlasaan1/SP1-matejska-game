using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePipe : BaseUnit
{
    public BasePipe(){
        IsMoveable = true;
    }

    public override void SetBackground()
    {
        backgroundObj = new GameObject("BackgroundObj");
        backgroundRenderer = backgroundObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = background;
        backgroundObj.transform.position = transform.position;
        backgroundObj.transform.rotation = Quaternion.Euler(0f,0f,rotation);
        base.SetBackground();
    }
}
