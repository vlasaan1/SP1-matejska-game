using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public BaseUnit occupiedUnit;
    public bool isOccupied = false;
    public void setUnit(BaseUnit unit){
        if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        unit.transform.localScale = transform.localScale;
        occupiedUnit = unit;
        unit.occupiedTile = this;
        isOccupied = true;
    }

    public void SwapUnits(Tile other){
        BaseUnit temp = occupiedUnit;
        occupiedUnit = other.occupiedUnit;
        other.occupiedUnit = temp;
    }
}
