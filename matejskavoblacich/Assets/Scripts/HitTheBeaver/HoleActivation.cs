using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;


public class HoleActivation : BaseHittable
{

    [SerializeField] GameObject beaverPrefab;
    GameObject activeBeaver;
    [SerializeField] float moveSpeed = 2f;
    Vector3 startPosition;
    Vector3 endPosition; 
    bool onShowBeaver = false;


    void Awake()
    {
        startPosition = transform.position + new Vector3(0f, 0.28f, 0f);
        endPosition = startPosition + new Vector3(0f, 0.72f, 0f);
    }

    void Update()
    {
        if(onShowBeaver){
            if(activeBeaver==null){
                onShowBeaver = false;
                //nekdo ho prastil - body
            }
            else{
                FollowPath();
            }
        }
    }

    public void showBeaver(){
        onShowBeaver = true;
        activeBeaver = Instantiate(
            beaverPrefab,
            startPosition,
            Quaternion.identity,
            transform
        );
    }

    void FollowPath()
    {
        if (Vector3.Distance(activeBeaver.transform.position, endPosition) > 0.01f)
        {
            Vector3 targetPosition = endPosition;
            float delta = moveSpeed * Time.deltaTime;
            activeBeaver.transform.position = Vector2.MoveTowards(activeBeaver.transform.position, targetPosition, delta);
        }
        else{
            onShowBeaver = false;
            Destroy(activeBeaver,2f);
        }
    }

}
