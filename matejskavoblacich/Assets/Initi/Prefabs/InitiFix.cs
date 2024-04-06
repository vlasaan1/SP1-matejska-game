using System.Collections;
using System.Collections.Generic;
using initi.input;
using UnityEngine;

public class InitiFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if(FindObjectsByType<InputController>(FindObjectsSortMode.None).Length > 1){
           Destroy(gameObject);
           return;
       } 
    }
}
