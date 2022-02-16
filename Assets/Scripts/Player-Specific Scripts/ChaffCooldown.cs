using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaffCooldown : MonoBehaviour
{
    //public vaiables----
    public ParticleSystem chaffSystem;
    public float sCooldown = 4;
    public AudioSource source;
    public AudioClip fireClip;
    public AudioClip fireFailedClip;
    public float volume = .7f;

    //Anti-tampering variables----
    SecretFloat cooldown;

    //private variables----
    private float timer;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        cooldown = new SecretFloat(sCooldown);
    }

    private void Start()
    {
        timer = cooldown.GetFloat();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.chaffOwned.VerifyBool(controller.pChaffOwned))
        {
            timer += Time.deltaTime;
            //If cooldown is over and the player uses the ability by hitting either shift key----
            if (timer >= cooldown.GetFloat() && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                //Reset timer and use ability----
                chaffSystem.Play();
                timer = 0;
                //Play sound from sound source----
                source.PlayOneShot(fireClip, volume);
            }
            //Playing sound if player tries to use chaff while it's on cooldown----
            else if (timer < cooldown.GetFloat() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
            {
                source.PlayOneShot(fireFailedClip, volume);
            }
        }
    }
}
