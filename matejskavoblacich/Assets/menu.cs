using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    [SerializeField] float numberToPrint;
    // Start is called before the first frame update
    public void Start(){
        numberToPrint = transform.position.z;
    }
    public void Print(){
        Debug.Log(Time.time + ": " + numberToPrint);
    }
}
