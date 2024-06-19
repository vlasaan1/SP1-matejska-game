using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
   //max health is assingned at the begining and restart
   int maxHealth = 3;
   int health;
   bool isAlive = true;

    void Awake(){
        health = maxHealth;
    }

    public int GetHealth(){
        return health;
    }
    public bool GetState(){
        return isAlive;
    }
/// <summary>
/// Lowers health score. If it went bellow 0, players' state is changed to not alive.
/// </summary>
/// <param name="change"></param>
    public void DecreseHealth(int change){
        health -= change;
        if(health<=0){
            isAlive=false;
        }
    }
    public void ResetHealth(){
        health = maxHealth;
    }

}
