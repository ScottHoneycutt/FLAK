using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DCMSensControl : MonoBehaviour
{
    //Slider that controls sensitivity----
    public Slider slider;

    //Audiosource for sample sound----
    public AudioSource soundSource;
    public AudioClip clip;

    //Old value to detect change----
    private float oldSens;

    //Getting old values from Gamecontrol on start----
    private void Start()
    {
        slider.value = GameControl.control.dcmSens;
        oldSens = GameControl.control.dcmSens;
    }

    private void Update()
    {
        //Playing sample sound----
        if (slider.value != oldSens && Input.GetMouseButtonUp(0))
        {
            soundSource.PlayOneShot(clip);
            oldSens = slider.value;
            GameControl.control.dcmSens = slider.value;
            GameControl.control.Save();
        }
    }
}

