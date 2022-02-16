using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DCMRangeControl : MonoBehaviour
{
    //Slider that controls sensitivity----
    public Slider slider;

    //Audiosource for sample sound----
    public AudioSource soundSource;
    public AudioClip clip;

    //Old value to detect change----
    private float oldRange;

    //Getting old values from Gamecontrol on start----
    private void Start()
    {
        slider.value = GameControl.control.dcmRange;
        oldRange = GameControl.control.dcmRange;
    }

    private void Update()
    {
        //Playing sample sound----
        if (slider.value != oldRange && Input.GetMouseButtonUp(0))
        {
            soundSource.PlayOneShot(clip);
            oldRange = slider.value;
            GameControl.control.dcmRange = slider.value;
            GameControl.control.Save();
        }
    }
}
