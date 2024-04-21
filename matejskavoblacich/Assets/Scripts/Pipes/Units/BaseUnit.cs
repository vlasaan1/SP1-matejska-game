using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseUnit : MonoBehaviour
{
    public Vector2 inDir;
    public Vector2 outDir;
    public Tile occupiedTile;
    protected float rotation = 0;
    protected SpriteRenderer spriteRenderer;
    private bool isMoveable;
    public bool IsMoveable {get; set;}
    private bool reversedFilling = false;
    public bool ReversedFilling { get; }
    public bool IsReversedFilling { get;}

    // background things
    public Sprite background;
    protected GameObject backgroundObj;
    protected SpriteRenderer backgroundRenderer;
    

    public virtual void CalculateRotation(){}

    public virtual void SetBackground(PlayerGrid playerGrid){}

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(backgroundObj){
            backgroundObj.transform.position = transform.position;
        }
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
