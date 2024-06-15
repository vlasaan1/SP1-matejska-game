using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalShine : MonoBehaviour
{
    [SerializeField] float minTimeBetweenShines;
    [SerializeField] float maxTimeBetweenShines;

    float nextShine;
    Animator animator;

    void Start()
    {
        nextShine = Time.time + Random.Range(minTimeBetweenShines,maxTimeBetweenShines);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time > nextShine){
            nextShine = Time.time + Random.Range(minTimeBetweenShines,maxTimeBetweenShines);
            animator.SetInteger("ShineVariant",Random.Range(0,4));
            animator.SetTrigger("Shine");
        }
    }
}
