using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollision : MonoBehaviour
{
    //Private variables---- 
    private bool expended = false;
    private UnitHP hpScript;

    //Caching unithp----
    private void Awake()
    {
        hpScript = this.GetComponent<UnitHP>();
    }

    private void OnEnable()
    {
        expended = false;
    }

    private void OnTriggerEnter(Collider objectCollision)
    {
        //If the bomb collides with an object on the "Enemy"/"Terrain" layer and it hasn't already collided...----
        if ((objectCollision.gameObject.layer == 8 || objectCollision.gameObject.layer == 18 || objectCollision.gameObject.layer == 19) && !expended)
        {
            //Die----
            hpScript.health = 0;
            expended = true;
        }
    }
}
