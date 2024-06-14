using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField] GameMaster gameMaster;
    [SerializeField] float offset = 0.5f;
    
    void Start(){
        transform.position = new Vector3(transform.position.x, transform.position.y - gameMaster.Scaler/2f - transform.localScale.y/2f - offset, transform.position.z);
    }
}
