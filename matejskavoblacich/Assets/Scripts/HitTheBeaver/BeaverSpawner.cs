using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;


public class BeaverSpawner : BaseHittable
{
    [SerializeField] Minigame minigame;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] bool isLooping;
    [SerializeField] List<HoleActivation> holes;

    int previousEnemies = 0;
    int previousBeavers = 0;


    void Start(){
        timeBetweenWaves = 2f;
        StartCoroutine(SpawnRandomBeaver());
    }

    IEnumerator SpawnRandomBeaver(){
        do{
            int index = Random.Range(0, holes.Count);
            if(holes[index].onShowBeaver!=0){
                yield return new WaitForSeconds(0.4f);
                continue;
            } 
            timeBetweenWaves = (0.72f / holes[index].moveSpeed) + 0.07f; //timeBetweenWaves = (0.36f / holes[index].moveSpeed) + 0.05f;
            holes[index].showBeaver( ref previousEnemies, ref previousBeavers);
            yield return new WaitForSeconds(timeBetweenWaves);
        } while(isLooping && (!minigame.isFinished));
    }
}