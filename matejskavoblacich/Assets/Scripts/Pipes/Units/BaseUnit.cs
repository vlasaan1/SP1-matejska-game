using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Vector2 inDir;
    public Vector2 outDir;
    public Tile occupiedTile;
    protected SpriteRenderer spriteRenderer;
    private bool isMoveable;
    public bool IsMoveable {get; set;}
    private bool reversedFilling = false;
    public bool ReversedFilling { get; }

    public virtual void CalculateRotation(){}

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void changeColor(Color color){
        spriteRenderer.color = color;
    }

    public void swapDirections(){
        Vector2 temp = inDir;
        inDir = outDir;
        outDir = temp;
        reversedFilling = true;
    }

}
