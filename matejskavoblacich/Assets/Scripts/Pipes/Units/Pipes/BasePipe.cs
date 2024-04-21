using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePipe : BaseUnit
{
    public BasePipe(){
        IsMoveable = true;
    }

    public override void SetBackground(PlayerGrid playerGrid)
    {
        backgroundObj = new GameObject("BackgroundObj");
        backgroundRenderer = backgroundObj.AddComponent<SpriteRenderer>();
        backgroundObj.transform.SetParent(playerGrid.transform);
        backgroundRenderer.sprite = background;
        backgroundRenderer.color = Color.white;
        backgroundRenderer.sortingOrder = 2;
        backgroundObj.transform.position = transform.position;
        backgroundObj.transform.rotation = transform.rotation;
        backgroundObj.transform.localScale = transform.localScale;
    }
}
