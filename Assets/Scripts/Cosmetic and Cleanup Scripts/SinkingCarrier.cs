using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingCarrier : MonoBehaviour
{
    //Public variables----
    public float tiltAngle = 15;
    public float sinkspeed = 3;
    public float tiltTime = 4;
    public float destroyDelay = 30;

    public GameObject carrier;
    public GameObject front;
    public GameObject back;

    //Private variables----
    private float timer = 0;
    private float tiltSpeed;
    private Quaternion startRotationFront;
    private Quaternion startRotationBack;
    // Use this for initialization
    private void Awake()
    {
        startRotationFront = front.transform.rotation;
        startRotationBack = back.transform.rotation;
    }

    void OnEnable()
    {
        timer = 0;
        tiltAngle = Random.Range(-tiltAngle, tiltAngle);
        tiltSpeed = tiltAngle / tiltTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Incremeting timer----
        timer += Time.deltaTime;
        //Sinking----
        carrier.transform.Translate(new Vector3(0, -sinkspeed * Time.deltaTime, 0));
        //Tilting----
        if (timer < tiltTime)
        {
            front.transform.Rotate(new Vector3(0, 0, tiltSpeed * Time.deltaTime));
            back.transform.Rotate(new Vector3(0, 0, -tiltSpeed * Time.deltaTime));
        }
        //Destroying----
        if (timer >= 30)
        {
            carrier.SetActive(false);
        }
    }
}
