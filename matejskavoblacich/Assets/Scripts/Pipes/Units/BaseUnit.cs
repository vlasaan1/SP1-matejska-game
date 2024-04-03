using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Vector2 inDir;
    public Vector2 outDir;
    public Tile occupiedTile;
    private SpriteRenderer spriteRenderer;
    public bool isMoveable;

    public virtual void CalculateRotation(){}

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void changeColor(Color color){
        spriteRenderer.color = color;
    }

}
