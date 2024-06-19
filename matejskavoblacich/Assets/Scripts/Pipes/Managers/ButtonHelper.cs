using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <sumary>
/// Class that just moves the button acording to the scaler of the game
/// <sumary>
public class ButtonHelper : MonoBehaviour
{
    [SerializeField] GameMaster gameMaster;
    [SerializeField] float offset = 0.5f;
    
    void Start(){
        transform.position = new Vector3(transform.position.x, transform.position.y - gameMaster.Scaler/2f - transform.localScale.y/2f - offset, transform.position.z);
    }
}
