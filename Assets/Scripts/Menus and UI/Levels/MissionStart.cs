using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionStart : MonoBehaviour
{
    public GameObject startUi;
    public GameObject number1;
    public GameObject number2;
    public GameObject number3;
    public Graphic blackScreen;

    public AudioSource source;
    public AudioClip beat;
    public AudioClip begin;
    //Start----
    void Start()
    {
        //Locking cursor to center of screen----
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 0.1f;
        Invoke("StartFrame", .1f);
        Invoke("SecondFrame", .2f);
        Invoke("LastFrame", .3f);
        source.PlayOneShot(beat, .3f);
    }

    //3----
    void StartFrame()
    {
        source.PlayOneShot(beat, .3f);
        number1.SetActive(false);
        number2.SetActive(true);
        blackScreen.CrossFadeAlpha(0, 2, true);
    }
    //2----
    void SecondFrame()
    {
        source.PlayOneShot(beat, .3f);
        number2.SetActive(false);
        number3.SetActive(true);

        //Unlocking Cursor----
        Cursor.lockState = CursorLockMode.None;
    }
    //1----
    void LastFrame()
    {
        source.PlayOneShot(begin, .5f);
        startUi.SetActive(false);
        Time.timeScale = 1;
    }
}
