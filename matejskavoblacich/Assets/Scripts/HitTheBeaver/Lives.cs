using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
   int health;

    void Awake(){
        health = 30;
    }

    public int GetHealth(){
        return health;
    }
    public void DecreseHealth(int change){
        health -= change;
    }
    public void ResetHealth(){
        health = 30;
    }

}
