using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster instance;

    public GameState gameState;

    [SerializeField] PlayerGrid onePlayerGrid;

    [SerializeField] int numberOfBombs = 3;

    [SerializeField] int fieldSize = 6;

    [SerializeField] int scaler = 5;

    [SerializeField] int numberOfPlayers = 4;

    private List<PlayerGrid> playersHolder = new List<PlayerGrid>();

    private Dictionary<Vector2, string> board;

    private List<PathTile> path;

    void Awake(){
        instance = this;
    }

    void Start()
    {
        ChangeState(GameState.InstantiatePlayers);
    }

    void InstantiatePlayers(){
        for(int i = 0; i < numberOfPlayers; i++){
            var spawnedOnePlayerField = Instantiate(onePlayerGrid, new Vector3(i, 0, 0), Quaternion.identity, transform);
            spawnedOnePlayerField.name = "Player" + i;
            spawnedOnePlayerField.transform.localScale = new Vector3(scaler / transform.localScale.x, scaler/ transform.localScale.y, 1);
            playersHolder.Add(spawnedOnePlayerField);
        }
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState){
        gameState = newState;
        switch(newState){
            case GameState.InstantiatePlayers:
                instance.InstantiatePlayers();
                break;
            case GameState.GenerateGrid:
                PlayerGrid[] instances = FindObjectsOfType<PlayerGrid>();
                foreach(var inst in instances){
                    inst.GenerateGrid(fieldSize, scaler);
                }
                ChangeState(GameState.Algorithm);
                break;
            case GameState.Algorithm:
                (board, path) = GeneratingAlgo.instance.GenerateMap(fieldSize, numberOfBombs);
                ChangeState(GameState.SpawnTiles);
                break;
            case GameState.SpawnTiles:
                UnitManager.instance.spawnUnits(board, path);
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.CheckState:
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
        PlayerTurn = 4,
        CheckState = 5,
        FailEnd = 6,
        GoodEnd = 7
    }

}
