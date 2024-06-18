using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;


public class BeaverSpawner : BaseHittable
{
    [SerializeField] Minigame minigame;
    [SerializeField] float timeBetweenWaves = 2f;
    [SerializeField] bool isLooping;
    [SerializeField] List<HoleActivation> holes;

    int previousEnemies = 0;
    int previousBeavers = 0;

/// <summary>
/// Starts a coroutine, that spawns an object periodically.
/// </summary>
    void Start(){
        StartCoroutine(SpawnRandomBeaver());
    }

/// <summary>
/// Spawns a beaver(baloon) at a given time in a randomly generated hole. Only while the game is not finished.
/// </summary>
    IEnumerator SpawnRandomBeaver(){
        do{
            int index = Random.Range(0, holes.Count);
            if(holes[index].onShowBeaver!=0){
                yield return new WaitForSeconds(0.4f);
                continue;
            } 
                                 //0.72 is the ditance object is gonna move 
            timeBetweenWaves = (0.72f / holes[index].moveSpeed) + 0.07f; //timeBetweenWaves = (0.36f / holes[index].moveSpeed) + 0.05f;
            holes[index].showBeaver( ref previousEnemies, ref previousBeavers);
            yield return new WaitForSeconds(timeBetweenWaves);
        } while(isLooping && (!minigame.isFinished));
    }
}