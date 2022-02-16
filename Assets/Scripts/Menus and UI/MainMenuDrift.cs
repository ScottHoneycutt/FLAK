using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDrift : MonoBehaviour {
    public float driftSpeed;
    public float timeSpeed;
    private float realtime = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update () {
        realtime = realtime + Time.deltaTime;
        
        float timer = (timeSpeed * realtime);

        float xSpeed = Mathf.Sin(timer);
        float ySpeed = Mathf.Cos(timer);

        transform.Translate(xSpeed*driftSpeed*Time.deltaTime, 0, 0);
        transform.Translate(0, ySpeed * driftSpeed * Time.deltaTime, 0);
    }
}
