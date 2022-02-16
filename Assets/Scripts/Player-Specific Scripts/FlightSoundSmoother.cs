using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSoundSmoother : MonoBehaviour
{
    public AudioSource soundSource;
    public bool missilesNear = false;
    public float fadeTime = 1.5f;
    private float volume = .2f;

    // Update is called once per frame
    void Update()
    {
        //Reducing volume when enemy missiles are near----
        if (missilesNear && volume > 0)
        {
            volume -= Time.deltaTime/fadeTime*.2f;
        }
        //Increasing volume once missiles are gone----
        else if(!missilesNear && volume < 1)
        {
            volume += Time.deltaTime / fadeTime*.2f;
        }

        //Making sure volume stays between 0 and 1----
        if (volume > .2)
        {
            volume = .2f;
        }
        else if (volume < 0)
        {
            volume = 0;
        }

        //Changing actual volume on the audiosource----
        soundSource.volume = volume;
    }
}
