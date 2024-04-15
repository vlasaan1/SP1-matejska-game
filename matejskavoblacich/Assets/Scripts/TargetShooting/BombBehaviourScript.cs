using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using initi.prefabScripts;

public class BombBehaviourScript : BaseHittable
{
    // Update is called once per frame
    void Update()
    {
        //destroy after 5 seconds
        Destroy(gameObject, 5f);

        
    }

    //onclick destroy
    public override void Hit(Vector2 hitPosition)
    {
        Destroy(gameObject);

        //add score -100 points
        Minigame minigame = GetComponentInParent<Minigame>();
        minigame.score -= 100;
    }

}
