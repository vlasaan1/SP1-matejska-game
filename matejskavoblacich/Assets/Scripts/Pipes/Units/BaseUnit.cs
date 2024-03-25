using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Vector2 inDir;
    public Vector2 outDir;
    public Tile occupiedTile;

    public bool isMoveable;

    public virtual void CalculateRotation(){}

}
