using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroy after 5 seconds
        Destroy(gameObject, 5f);

        
    }

    //onclick destroy
    public void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
