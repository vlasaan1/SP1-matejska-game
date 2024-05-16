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
        //add score -600 points
        GetComponentInParent<BoothBehaviourScript>().UpdateScore(-600);

        Destroy(gameObject);
    }

}
