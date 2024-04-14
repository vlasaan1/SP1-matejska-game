using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
   int health;
   bool isAlive = true;

    void Awake(){
        health = 3;
    }

    public int GetHealth(){
        return health;
    }
    public bool GetState(){
        return isAlive;
    }

    public void DecreseHealth(int change){
        health -= change;
        if(health<=0){
            isAlive=false;
        }
    }
    public void ResetHealth(){
        health = 3;
    }

}
