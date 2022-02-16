using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunMunitionFlight : MonoBehaviour
{
    //public variables----
    public float speed = 1000;
    public float lifetime = .5f;
    public GameObject munition;

    //private variables----
    private float timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Forward flight----
        munition.transform.Translate(0, 0, speed * Time.deltaTime);

        //Incrementing timer----
        timer += Time.deltaTime;
        //Removing if past lifetime----
        if(timer >= lifetime)
        {
            munition.SetActive(false);
        }
    }

    //Restting lifetime timer for each use----
    private void OnEnable()
    {
        timer = 0;
    }
}
