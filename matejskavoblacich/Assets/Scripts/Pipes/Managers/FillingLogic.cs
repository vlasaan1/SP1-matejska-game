using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class FillingLogic : MonoBehaviour
{
    [SerializeField] float waitingTime = 2f;
    public static FillingLogic instance;
    [SerializeField] PlayerGrid playerGrid;

    void Awake(){
        instance = this;
    }

    public void startFilling(BaseUnit start, BaseUnit end){
        BaseUnit current = start;
        BaseUnit previous = start;
        bool finisedOut = false;
        StartCoroutine(fillingHelper(previous, current, finisedOut));
        if(finisedOut){
            Debug.Log("Good finish");
        }
        else{
            Debug.Log("Bad finish");
        }
    }

    private IEnumerator fillingHelper(BaseUnit previous, BaseUnit current, bool finisedOut){
        while(true){
            if(endCheck(current)){
                finisedOut = true;
                yield break;
            }
            if(!controlInput(previous, current)){
                finisedOut =  false;
                yield break;
            }
            current.IsMoveable = false;
            current.changeColor(Color.green);
            yield return new WaitForSeconds(waitingTime);
            current.changeColor(Color.red);

            previous = current;
            current = sendSignalToNextPipe(current);
        }
    }

    private bool controlInput(BaseUnit previous, BaseUnit current){
        if(current.occupiedTile.possitionOnGrid.x + current.inDir.x == previous.occupiedTile.possitionOnGrid.x && current.occupiedTile.possitionOnGrid.y + current.inDir.y == previous.occupiedTile.possitionOnGrid.y)
            return true;
        if(current.occupiedTile.possitionOnGrid.x + current.outDir.x == previous.occupiedTile.possitionOnGrid.x && current.occupiedTile.possitionOnGrid.y + current.outDir.y == previous.occupiedTile.possitionOnGrid.y){
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
