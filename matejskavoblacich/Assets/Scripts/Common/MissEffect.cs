using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissEffect : MonoBehaviour
{
    [SerializeField] float timeToDestroy = .5f;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
