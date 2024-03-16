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

    private BaseUnit getUnit (Name n){
        return units.FirstOrDefault(u=>u.uName == n).unitPrefab;
    }

    private BaseUnit getRandomPipe(Type t){
        return units.Where( u=> u.type == t).OrderBy( o => Random.value).First().unitPrefab;
    }

    public void spawnUnits(Dictionary<Vector2, string> board, List<PathTile> path){
        for(int i = 0; i < path.Count; i++){
            
        }
    }

}
