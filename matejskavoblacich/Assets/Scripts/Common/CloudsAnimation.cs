using System;
using System.Collections.Generic;
using UnityEngine;

using Random=UnityEngine.Random;

public class CloudsAnimation : MonoBehaviour
{
    [SerializeField] float minTimeBetweenClouds;
    [SerializeField] float maxTimeBetweenClouds;
    [SerializeField] float minCloudSpeed;
    [SerializeField] float maxCloudSpeed;
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    [SerializeField] List<GameObject> clouds;

    List<Tuple<GameObject,float>> currentClouds = new();
    float nextCloud = 0;

    void Update()
    {
        if(Time.time > nextCloud){
            GameObject newCloud = Instantiate(clouds[Random.Range(0,clouds.Count)], new Vector3(-maxX-10,Random.Range(-maxY,maxY),1),Quaternion.identity,transform);
            currentClouds.Add(Tuple.Create(newCloud,Random.Range(minCloudSpeed,maxCloudSpeed)));
            nextCloud = Time.time + Random.Range(minTimeBetweenClouds,maxTimeBetweenClouds);
        }
        int maxI = currentClouds.Count;
        for(int i=0;i<maxI;i++){
            var cloud = currentClouds[i];
            cloud.Item1.transform.Translate(cloud.Item2 * Time.deltaTime * Vector3.right);
            if(cloud.Item1.transform.position.x > maxX+10){
                Destroy(cloud.Item1);
                currentClouds.RemoveAt(i);
                i--;
                maxI--;
            }
        }
    }
}
