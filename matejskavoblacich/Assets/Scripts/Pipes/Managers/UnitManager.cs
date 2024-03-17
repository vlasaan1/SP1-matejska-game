using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    private List<ScriptableUnit> units;

    void Awake(){
        instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Pipes/Units").ToList();
    }

    private T getUnit<T> (Name n) where T : BaseUnit{
        return (T) units.FirstOrDefault(u=>u.uName == n).unitPrefab;
    }

    private T getRandomPipe<T>(Type t) where T : BaseUnit{
        return (T) units.Where( u=> u.type == t).OrderBy( o => Random.value).First().unitPrefab;
    }

    public void spawnUnits(Dictionary<Vector2, string> board, List<PathTile> path, PlayerGrid playerHolder){
        for(int i = 1; i < path.Count - 1; i++){
            var unit = getUnit<BasePipe>(getPipeType(path[i].inDir, path[i].outDir));
            var spawnedUnit = Instantiate(unit, playerHolder.transform);
            var spawnUnitOnTile = playerHolder.grid[path[i].position];
            spawnUnitOnTile.setUnit(spawnedUnit);
        }
        var start = getUnit<BaseCore>(Name.StartPipe);
        var spawnedStart = Instantiate(start, playerHolder.transform);
        var startTile = playerHolder.grid[path[0].position];
        startTile.setUnit(spawnedStart);
        var end = getUnit<BaseCore>(Name.StartPipe);
        var spawnedEnd = Instantiate(end, playerHolder.transform);
        var endTile = playerHolder.grid[path[path.Count-1].position];
        endTile.setUnit(spawnedEnd);
    }

    private Name getPipeType(Vector2 first, Vector2 second){
        Vector2 vec = new Vector2(first.x + second.x, first.y + second.y);
        if(vec.x == 0 && vec.y == 0)
            return Name.StraightPipe;
        return Name.RoundPipe;
    }

}
