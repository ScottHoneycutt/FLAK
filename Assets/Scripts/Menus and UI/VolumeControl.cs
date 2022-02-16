using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    //Slider that controls volume----
    public Slider slider;

    //Audiosource for sample sound----
    public AudioSource soundSource;
    public AudioClip clip;

    //Old value to detect change----
    private float oldVolume;

    //Getting old values from Gamecontrol on start----
    private void Start()
    {
        slider.value = GameControl.control.volume;
        oldVolume = GameControl.control.volume;
    }

    //Changing volume----
    public void SetVolume()
    {
        AudioListener.volume = slider.value;
    }

    private void Update()
    {
        //Playing sample sound----
        if (slider.value != oldVolume && Input.GetMouseButtonUp(0))
        {
            soundSource.PlayOneShot(clip);
            oldVolume = slider.value;
            GameControl.control.volume = slider.value;
            GameControl.control.Save();
        }
    }
}
