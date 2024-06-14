using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// abstract base class for every unit in the game
/// </summary>
public class BaseUnit : MonoBehaviour
{
    public Vector2 inDir;
    public Vector2 outDir;
    public Tile occupiedTile;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private bool isMoveable;
    public bool IsMoveable {get; set;}
    public bool reversedFilling = false;
    protected float rotation = 0f;

    /// <summary>
    /// calculates rotation depending on class variables that it needs
    /// </summary>
    public virtual void CalculateRotation(){}

    public virtual void SetBackground(PlayerGrid playerGrid){}

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void changeColor(Color color){
        spriteRenderer.color = color;
    }

    /// <summary>
    /// swaps inDir with outDir if needed
    /// </summary>
    public void swapDirections(){
        Vector2 temp = inDir;
        inDir = outDir;
        outDir = temp;
        reversedFilling = true;
    }

}
