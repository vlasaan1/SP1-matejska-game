using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;

/// <summary>
/// Class that handles "liquid flow in the game and the game flow proccess itself"
/// </summary>
public class FillingLogic : MonoBehaviour
{
    [SerializeField] float firstWaitingTime = 8f;
    [SerializeField] float waitingTime = 4f;
    [SerializeField] float speedWaitingTime = 0.25f;
    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] GameMaster gameMaster;
    [SerializeField] Minigame minigame;
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

            float startTime = Time.time;
            float currTime = i == 0 ? firstWaitingTime : waitingTime;
            current.spriteRenderer.material.SetInt("_isReversed", current.reversedFilling ? 1 : 0);
            while(startTime + currTime >= Time.time){
                current.spriteRenderer.material.SetFloat("_HoldPercent", (Time.time - startTime) / currTime);
                yield return new WaitForSeconds(0);
            }
            current.spriteRenderer.material.SetFloat("_HoldPercent", 1);

            previous = current;
            current = sendSignalToNextPipe(current);
            i++;
        }
        
        if(finishState){
            playerGrid.setSpriteRendererColor(new Color(137/255f, 209/255f, 116/255f));
            gameMaster.ChangeState(GameMaster.GameState.GoodEnd);
        }
        else{
            playerGrid.setSpriteRendererColor(new Color(236/255f, 10/255f, 25/255f));
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
        current = playerGrid.GetTileAtPosition(new Vector2Int((int) current.occupiedTile.possitionOnGrid.x + (int) current.outDir.x, (int) current.occupiedTile.possitionOnGrid.y + (int) current.outDir.y)).occupiedUnit;
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
        if(!minigame.isFinished)
            waitingTime = speedWaitingTime;
    }

}
