using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using initi.prefabScripts;

public class TargetBehaviourScript : BaseHittable
{

    public override void Hit(Vector2 hitPosition)
    {
        //destroy target
        Destroy(gameObject);
        //add score 50 points
        Minigame minigame = GetComponentInParent<Minigame>();
        minigame.score += 50;
    }

    public void Update()
    {
        //every 0.5 seconds independently of time scale
        if (Time.unscaledTime % 0.1f < Time.unscaledDeltaTime)
        {
            //shrinks the target
            transform.localScale = new Vector3(transform.localScale.x - 0.001f, transform.localScale.y - 0.001f, transform.localScale.z);   

            //if target is too small, destroy it
            if (transform.localScale.x < 0.001f)
            {
                Destroy(gameObject);
            }
        }
    }
}
