using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using initi.prefabScripts;
using Unity.Mathematics;

public class BeaverBehaviour : BaseHittable
{
    [SerializeField] ParticleSystem popEffect;
    //Override metody Hit - zavolá se pokud se na tento objekt klikne na stěně
    public override void Hit(Vector2 hitPosition){
        //hitPosition jsou globální souřadnice doteku
        //popEffect.transform.position = transform.position;// - new Vector3(0f, 0f, 0f);
        ParticleSystem pop = Instantiate(popEffect, transform.position  - new Vector3(0f,0f,2f), Quaternion.identity);
        popEffect.Play();
        Destroy(pop,0.5f);
        Debug.Log("played" + popEffect.transform.position);
        Destroy(gameObject);
    }
}
