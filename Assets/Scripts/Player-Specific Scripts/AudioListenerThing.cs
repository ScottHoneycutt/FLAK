using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerThing : MonoBehaviour
{
    public GameObject audioListenerObject;
    public GameObject playerFighter;

    //private variables----
    private float timer = 0;
    private float zVal = -100;

    // Start is called before the first frame update
    void Awake()
    {
        audioListenerObject.transform.position = new Vector3 (playerFighter.transform.position.x, playerFighter.transform.position.y, -300);
        //Setting volume from gameControl----
        AudioListener.volume = GameControl.control.volume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerFighter)
        {
            timer += Time.deltaTime;
            //Mission intro----
            if (timer < .3f)
            {
                zVal += 333*Time.deltaTime;
                audioListenerObject.transform.position = new Vector3(playerFighter.transform.position.x, playerFighter.transform.position.y, -200+zVal);
            }
            else
            {
                audioListenerObject.transform.position = new Vector3(playerFighter.transform.position.x, playerFighter.transform.position.y, -200);
            }
        }
    }
} 
