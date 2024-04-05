using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class FillingLogic : MonoBehaviour
{
    [SerializeField] float firstWaitingTime = 8f;
    [SerializeField] float waitingTime = 4f;
    [SerializeField] float speedWaitingTime = 0.25f;
    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] GameMaster gameMaster;
    private bool finishState = false;

    /// <summary>
    /// outer method that is called from game master to manage gameplay
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void startFilling(BaseUnit start, BaseUnit end){
        BaseUnit current = start;
        BaseUnit previous = start;
        StartCoroutine(fillingHelper(previous, current));
    }

    /// <summary>
    /// coroutine that manages filling pipe by pipe
    /// </summary>
    /// <param name="previous"></param>
    /// <param name="current"></param>
    /// <returns></returns>
    private IEnumerator fillingHelper(BaseUnit previous, BaseUnit current){
        int i = 0;
        while(true){
            if(endCheck(current)){
                finishState = true;
                break;
            }
            if(!controlInput(previous, current)){
                finishState =  false;
                break;
            }
            current.IsMoveable = false;
            
            current.changeColor(Color.green);
            if(i == 0){
                yield return new WaitForSeconds(firstWaitingTime);
            }
            yield return new WaitForSeconds(waitingTime);
            current.changeColor(Color.red);

            previous = current;
            current = sendSignalToNextPipe(current);
            i++;
        }
        
        if(finishState){
            Debug.Log("Good finish");
            gameMaster.ChangeState(GameMaster.GameState.GoodEnd);
        }
        else{
            Debug.Log("Bad finish");
            gameMaster.ChangeState(GameMaster.GameState.FailEnd);
        }
    }

    /// <summary>
    /// method that controls if pipes were conected correctly to either end or another pipe
    /// </summary>
    /// <param name="previous"></param>
    /// <param name="current"></param>
    /// <returns></returns>
    private bool controlInput(BaseUnit previous, BaseUnit current){
        if(current.occupiedTile.possitionOnGrid.x + current.inDir.x == previous.occupiedTile.possitionOnGrid.x && current.occupiedTile.possitionOnGrid.y + current.inDir.y == previous.occupiedTile.possitionOnGrid.y)
            return true;
        if(current.occupiedTile.possitionOnGrid.x + current.outDir.x == previous.occupiedTile.possitionOnGrid.x && current.occupiedTile.possitionOnGrid.y + current.outDir.y == previous.occupiedTile.possitionOnGrid.y){
            current.swapDirections();
            return true;
        }
        return false;
    }

    /// <summary>
    /// method that sends signal to other pipe that should be filled next
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    private BaseUnit sendSignalToNextPipe(BaseUnit current){
        if(current.ReversedFilling)
            current = playerGrid.GetTileAtPosition(new Vector2Int((int) current.occupiedTile.possitionOnGrid.x + (int) current.inDir.x, (int) current.occupiedTile.possitionOnGrid.y + (int) current.inDir.y)).occupiedUnit;
        else{
            current = playerGrid.GetTileAtPosition(new Vector2Int((int) current.occupiedTile.possitionOnGrid.x + (int) current.outDir.x, (int) current.occupiedTile.possitionOnGrid.y + (int) current.outDir.y)).occupiedUnit;
        }
        return current;
    }

    /// <summary>
    /// checking if pipe that is currently procesed is ending pipe
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    private bool endCheck(BaseUnit current){
        return current is EndPipe;
    }

    public void setSpeedToFilling(){
        waitingTime = speedWaitingTime;
    }

}
