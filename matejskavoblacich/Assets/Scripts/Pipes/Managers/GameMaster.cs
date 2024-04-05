using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameState gameState;
    [SerializeField] Minigame minigame;
    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] GeneratingAlgo generatingAlgo;
    [SerializeField] UnitManager unitManager;
    [SerializeField] FillingLogic fillingLogic;
    [SerializeField] int numberOfBombs = 3;
    [SerializeField] int fieldSize = 6;
    [SerializeField] int scaler = 5;
    [SerializeField] bool useSeed = false;

    private Dictionary<Vector2, string> board;
    private List<PathTile> path;
    private BaseUnit start;
    private BaseUnit end;
    private int seed;

    void Start()
    {
        ChangeState(GameState.Instantiate);
    }

    private void StartUp(){
        if(useSeed){
            seed = minigame.seed;
        }
        else{
            seed = Random.Range(0, int.MaxValue);
        }
    }

    private void FailedFunction(){
        minigame.isFinished = true;
    }

    private void SuccessedFunction(){
        minigame.score = (int) ((minigame.endTime - Time.time) * 100);
        minigame.isFinished = true;
    }

    public void ChangeState(GameState newState){
        gameState = newState;
        switch(newState){
            case GameState.Instantiate:
                StartUp();
                ChangeState(GameState.GenerateGrid);
                break;
            case GameState.GenerateGrid:
                playerGrid.GenerateGrid(fieldSize, scaler, seed);
                ChangeState(GameState.Algorithm);
                break;
            case GameState.Algorithm:
                (board, path) = generatingAlgo.GenerateMap(fieldSize, numberOfBombs, seed);
                ChangeState(GameState.SpawnTiles);
                break;
            case GameState.SpawnTiles:
                (start, end) = unitManager.spawnUnits(board, path, playerGrid, fieldSize, seed);
                ChangeState(GameState.Gameplay);
                break;
            case GameState.Gameplay:
                fillingLogic.startFilling(start, end);
                break;
            case GameState.FailEnd:
                FailedFunction();
                break;
            case GameState.GoodEnd:
                SuccessedFunction();
                break;
        }
        
    }

    public enum GameState{
        Instantiate = 0,
        GenerateGrid = 1,
        Algorithm = 2,
        SpawnTiles = 3,
        Gameplay = 4,
        FailEnd = 5,
        GoodEnd = 6
    }

}
