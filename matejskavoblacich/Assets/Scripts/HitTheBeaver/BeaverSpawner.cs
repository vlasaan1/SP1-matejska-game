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

    void Start(){
        timeBetweenWaves = 2f;
        StartCoroutine(SpawnRandomBeaver());
    }

    IEnumerator SpawnRandomBeaver(){
        do{
            int index = Random.Range(0, holes.Count);
            if(holes[index].onShowBeaver!=0){
                yield return new WaitForSeconds(0.3f);
                continue;
            } 
            timeBetweenWaves = (0.72f / holes[index].moveSpeed) + 0.05f;
            //timeBetweenWaves = (0.36f / holes[index].moveSpeed) + 0.05f;
            holes[index].showBeaver();
            yield return new WaitForSeconds(timeBetweenWaves);
        } while(isLooping && (!minigame.isFinished));
    }

}
