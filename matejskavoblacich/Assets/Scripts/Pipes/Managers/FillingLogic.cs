using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class FillingLogic : MonoBehaviour
{
    [SerializeField] float waitingTime = 3f;
    public static FillingLogic instance;
    [SerializeField] PlayerGrid playerGrid;

    void Awake(){
        instance = this;
    }

    public void startFilling(BaseUnit start, BaseUnit end){
        BaseUnit current = start;
        if(fillingHelper(start.occupiedTile.possitionOnGrid, current)){
            Debug.Log("Good ending");
        }
        else{
            Debug.Log("Bad Ending");
        }
    }

    IEnumerator fillingCoroutine(BaseUnit unit){
        unit.changeColor(Color.green);
        yield return new WaitForSeconds(waitingTime);
        unit.changeColor(Color.red);
    }

    private bool fillingHelper(Vector2 previousPosition, BaseUnit current){
        while(true){
            current.IsMoveable = false;
            if(endCheck(current))
                return true;
            if(!controlInput(previousPosition, current)){
                return false;
            }
            StartCoroutine(fillingCoroutine(current));
            current = sendSignalToNextPipe(current);
        }
    }

    private bool controlInput(Vector2 previousPosition, BaseUnit current){
        if(current.occupiedTile.possitionOnGrid.x + current.inDir.x == previousPosition.x && current.occupiedTile.possitionOnGrid.y + current.inDir.y == previousPosition.y)
            return true;
        if(current.occupiedTile.possitionOnGrid.x + current.outDir.x == previousPosition.x && current.occupiedTile.possitionOnGrid.y + current.outDir.y == previousPosition.y){
            current.swapDirections();
            return true;
        }
        return false;
    }

    private BaseUnit sendSignalToNextPipe(BaseUnit current){
        if(current.ReversedFilling)
            current = playerGrid.GetTileAtPosition(new Vector2Int((int) current.occupiedTile.possitionOnGrid.x + (int) current.inDir.x, (int) current.occupiedTile.possitionOnGrid.y + (int) current.inDir.y)).occupiedUnit;
        else{
            current = playerGrid.GetTileAtPosition(new Vector2Int((int) current.occupiedTile.possitionOnGrid.x + (int) current.outDir.x, (int) current.occupiedTile.possitionOnGrid.y + (int) current.outDir.y)).occupiedUnit;
        }
        return current;
    }

    private bool endCheck(BaseUnit current){
        return current is EndPipe;
    }
}
