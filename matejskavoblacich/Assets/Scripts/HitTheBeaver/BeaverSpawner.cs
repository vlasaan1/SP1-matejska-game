using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;


public class BeaverSpawner : BaseHittable
{
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    [SerializeField] List<HoleActivation> holes;

    void Start(){
        StartCoroutine(SpawnRandomBeaver());
    }

    IEnumerator SpawnRandomBeaver(){
        do{
            int index = Random.Range(0, holes.Count);
            holes[index].showBeaver();
            yield return new WaitForSeconds(timeBetweenWaves);
        } while(isLooping);
    }

}
