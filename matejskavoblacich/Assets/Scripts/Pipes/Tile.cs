using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public BaseUnit occupiedUnit;
    public bool isOccupied = false;
    public Vector2 possitionOnGrid;
    public int sizeOfFiled;
    public void setUnit(BaseUnit unit){
        if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        unit.transform.localScale = transform.localScale;
        occupiedUnit = unit;
        unit.occupiedTile = this;
        isOccupied = true;
    }

    public void SwapUnits(Tile other){
        BaseUnit thisUnit = occupiedUnit;
        BaseUnit otherUnit = other.occupiedUnit;
        occupiedUnit = other.occupiedUnit;
        other.occupiedUnit = thisUnit;
        thisUnit.occupiedTile = other;
        otherUnit.occupiedTile = this;
    }

}
