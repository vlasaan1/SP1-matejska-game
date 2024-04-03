using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingLogic : MonoBehaviour
{
    [SerializeField] float waitingTime = 3f;
    public static FillingLogic instance;
    private bool finished = false;

    void Awake(){
        instance = this;
    }

    public void startFilling(BaseUnit start, BaseUnit end){
        StartCoroutine(fillingCoroutine(start));
    }

    IEnumerator fillingCoroutine(BaseUnit unit){
        unit.changeColor(Color.green);
        yield return new WaitForSeconds(waitingTime);
        unit.changeColor(Color.red);
    }
}
