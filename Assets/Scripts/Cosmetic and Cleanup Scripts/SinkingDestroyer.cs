using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingDestroyer : MonoBehaviour {
    //Public variables----
    public float tiltAngle = 15;
    public float sinkspeed = 3;
    public float tiltTime = 4;
    public float destroyDelay = 30;

    public GameObject destroyer;

    //Private variables----
    private float timer = 0;
    private float tiltSpeed;
	// Use this for initialization
	void OnEnable () {
        timer = 0;
        tiltAngle = Random.Range(-tiltAngle, tiltAngle);
        tiltSpeed = tiltAngle / tiltTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Incremeting timer----
        timer += Time.deltaTime;
        //Sinking----
        this.transform.Translate(new Vector3(0, -sinkspeed * Time.deltaTime, 0));
        //Tilting----
        if (timer < tiltTime)
        {
            this.transform.Rotate(new Vector3(0, 0, tiltSpeed * Time.deltaTime));
        }
        //Destroying----
        if (timer >= 30)
        {
            destroyer.SetActive(false);
        }
	}
}
