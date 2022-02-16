using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftCrash : MonoBehaviour {

    void OnTriggerEnter(Collider objectCollision)
    {
        //If the aircraft collides with terrain...----
        if (objectCollision.gameObject.layer == 18)
        {
            //Die----
            this.GetComponent<UnitHP>().health = 0;
        }
    }
}
