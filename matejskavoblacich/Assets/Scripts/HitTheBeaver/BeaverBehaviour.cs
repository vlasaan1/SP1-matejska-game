using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;

public class BeaverBehaviour : BaseHittable
{
    //Override metody Hit - zavolá se pokud se na tento objekt klikne na stěně
    public override void Hit(Vector2 hitPosition){
        //hitPosition jsou globální souřadnice doteku
        Destroy(gameObject);
    }
}
