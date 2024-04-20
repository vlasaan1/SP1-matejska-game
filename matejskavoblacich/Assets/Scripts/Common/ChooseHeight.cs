using System.Collections;
using System.Collections.Generic;
using initi.prefabScripts;
using UnityEngine;

public class ChooseHeight : BaseHittable
{
    [SerializeField] Menu menu;

    public override void Hit(Vector2 hitPosition)
    {
        menu.SetHeight(hitPosition.y);
    }
}
