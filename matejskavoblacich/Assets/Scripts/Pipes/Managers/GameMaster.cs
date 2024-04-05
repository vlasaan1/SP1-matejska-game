using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameState gameState;

    [SerializeField] PlayerGrid playerGrid;
    [SerializeField] GeneratingAlgo generatingAlgo;
    [SerializeField] UnitManager unitManager;
    [SerializeField] FillingLogic fillingLogic;
    [SerializeField] int numberOfBombs = 3;

    [SerializeField] int fieldSize = 6;

    [SerializeField] int scaler = 5;

    private Dictionary<Vector2, string> board;

    private List<PathTile> path;

    private BaseUnit start;
    private BaseUnit end;

    void Start()
    {
        ChangeState(GameState.InstantiatePlayers);
    }

    public void ChangeState(GameState newState){
        gameState = newState;
        switch(newState){
            case GameState.InstantiatePlayers:
                ChangeState(GameState.GenerateGrid);
                break;
            case GameState.GenerateGrid:
                playerGrid.GenerateGrid(fieldSize, scaler);
                ChangeState(GameState.Algorithm);
                break;
            case GameState.Algorithm:
                (board, path) = generatingAlgo.GenerateMap(fieldSize, numberOfBombs);
                ChangeState(GameState.SpawnTiles);
                break;
            case GameState.SpawnTiles:
                (start, end) = unitManager.spawnUnits(board, path, playerGrid, fieldSize);
                ChangeState(GameState.Gameplay);
                break;
            case GameState.Gameplay:
                fillingLogic.startFilling(start, end);
                break;
            case GameState.FailEnd:
                break;
            case GameState.GoodEnd:
                break;
        }
        
    }

    public enum GameState{
        InstantiatePlayers = 0,
        GenerateGrid = 1,
        Algorithm = 2,
        SpawnTiles = 3,
        Gameplay = 4,
        FailEnd = 5,
        GoodEnd = 6
    }

}
