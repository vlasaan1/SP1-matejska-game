using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] Minigame minigame;
    [SerializeField] PlayerGrid playerGrid;
    private List<ScriptableUnit> units;
    private int fieldSize;
    private List<Vector2> directions = new List<Vector2>{
            new Vector2(1,0),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(0,-1)
        };
    private BaseUnit start;
    private BaseUnit end;
    private System.Random random;

    void Awake(){
        if(minigame.playerId == 0){ //blue
            units = Resources.LoadAll<ScriptableUnit>("Pipes/Blue/Units").ToList();
        }
        else if(minigame.playerId == 1){ //pink
            units = Resources.LoadAll<ScriptableUnit>("Pipes/Pink/Units").ToList();
        }
        else if(minigame.playerId == 2){ //yellow
            units = Resources.LoadAll<ScriptableUnit>("Pipes/Yellow/Units").ToList();
        }
    }

    /// <summary>
    /// returns BaseUnit specialization based on parametre given
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n">name of the BaseUnit i want specificaly</param>
    /// <returns></returns>
    private T getUnit<T> (Name n) where T : BaseUnit{
        return (T) units.FirstOrDefault(u=>u.uName == n).unitPrefab;
    }

    /// <summary>
    /// function that is called from GameMaster to spawn Units on a Tiles
    /// </summary>
    /// <param name="board"></param>
    /// <param name="path"></param>
    /// <param name="playerHolder"></param>
    /// <param name="fs"></param>
    public (BaseUnit, BaseUnit) spawnUnits(Dictionary<Vector2, string> board, List<PathTile> path, PlayerGrid playerHolder, int fs, int seed)
    {
        random = new System.Random(seed);
        fieldSize = fs;
        SpawnMainPath(path, playerHolder);
        SpawnRestUnits(board, playerHolder);
        playerHolder.Shuffle();
        return (start, end);
    }

    /// <summary>
    /// Generating Unites based on board, if Tile is already occupied by some unit, ignore it
    /// </summary>
    /// <param name="board">to know what to spawn</param>
    /// <param name="playerHolder">player context</param>
    private void SpawnRestUnits(Dictionary<Vector2, string> board, PlayerGrid playerHolder){
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < fieldSize; x++){
                Vector2Int vec = new Vector2Int(x, y);
                if(!playerHolder.GetTileAtPosition(vec).isOccupied){
                    string tmp = board[vec];
                    if(tmp == "X"){
                        SpawnAndSetUnit(new PathTile(vec), playerHolder, Name.Wall);
                    }
                    else if(tmp == "B"){
                        SpawnAndSetUnit(new PathTile(vec), playerHolder, Name.Bomb);
                    }
                    else if(tmp == "V"){
                        PathTile temp = generateRandomPipePathTile(vec);
                        SpawnAndSetUnit(temp, playerHolder, getPipeType(temp.inDir, temp.outDir));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Generetas random PathTile, used for generating straight pipe or round pipe
    /// </summary>
    /// <param name="pos">position in grid</param>
    /// <returns></returns>
    private PathTile generateRandomPipePathTile(Vector2 pos){
        PathTile ret = new PathTile(pos);
        int firstIdx = random.Next(0, directions.Count);
        int secondIdx;
        while(true){
            secondIdx = random.Next(0, directions.Count);
            if(firstIdx != secondIdx)
            break;
        }
        ret.inDir = directions[firstIdx];
        ret.outDir = directions[secondIdx];
        return ret;
    }

    /// <summary>
    /// Spawns a main path Units
    /// </summary>
    /// <param name="path"></param>
    /// <param name="playerHolder"></param>
    private void SpawnMainPath(List<PathTile> path, PlayerGrid playerHolder){
        for(int i = 1; i < path.Count - 1; i++){
            SpawnAndSetUnit(path[i], playerHolder, getPipeType(path[i].inDir, path[i].outDir));
        }
        start = SpawnAndSetUnit(path[0], playerHolder, Name.StartPipe);
        end = SpawnAndSetUnit(path[path.Count-1], playerHolder, Name.EndPipe);
    }

    /// <summary>
    /// Spawns and sets unit on a Tile based on info what is in PathTile info
    /// </summary>
    /// <param name="info">Path tile to be spawned</param>
    /// <param name="playerHolder">current Player</param>
    /// <param name="name">name of the spawning Unit</param>
    private BaseUnit SpawnAndSetUnit(PathTile info, PlayerGrid playerHolder, Name name){
        var unit = getUnit<BaseUnit>(name);
        unit.inDir = info.inDir;
        unit.outDir = info.outDir;
        var spawnedUnit = Instantiate(unit, playerHolder.transform);
        var spawnUnitOnTile = playerHolder.grid[info.position];
        spawnUnitOnTile.setUnit(spawnedUnit);
        spawnedUnit.CalculateRotation();
        spawnedUnit.SetBackground(playerGrid);
        return spawnedUnit;
    }

    /// <summary>
    /// Returns name of the Pipe that will be spawned based on their out directions
    /// </summary>
    /// <param name="first">first direction of a Unit</param>
    /// <param name="second">second direction of a Unit</param>
    /// <returns></returns>
    private Name getPipeType(Vector2 first, Vector2 second){
        Vector2 vec = new Vector2(first.x + second.x, first.y + second.y);
        if(vec.x == 0 && vec.y == 0)
            return Name.StraightPipe;
        return Name.RoundPipe;
    }

}
