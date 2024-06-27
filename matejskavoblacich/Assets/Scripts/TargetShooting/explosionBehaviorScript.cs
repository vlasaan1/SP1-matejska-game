using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBehaviorScript : MonoBehaviour
{
    // destroy self after 1 seconds
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
