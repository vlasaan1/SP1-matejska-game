using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles tiles that are placed on the playing grid, units are spawned on the tiles
/// </summary>
public class Tile : MonoBehaviour
{
    public BaseUnit occupiedUnit;
    public bool isOccupied = false;
    public Vector2 possitionOnGrid;
    public int sizeOfFiled;

    /// <summary>
    /// connects unit to the tile and tile to the unit
    /// </summary>
    /// <param name="unit"></param>
    public void setUnit(BaseUnit unit){
        if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        unit.transform.localScale = transform.localScale;
        occupiedUnit = unit;
        unit.occupiedTile = this;
        isOccupied = true;
    }

    /// <summary>
    /// swaps units from tile to other tile
    /// </summary>
    /// <param name="other"></param>
    public void SwapUnits(Tile other){
        BaseUnit thisUnit = occupiedUnit;
        BaseUnit otherUnit = other.occupiedUnit;
        occupiedUnit = other.occupiedUnit;
        other.occupiedUnit = thisUnit;
        thisUnit.occupiedTile = other;
        otherUnit.occupiedTile = this;
    }

}
