using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParticleSystem : MonoBehaviour
{
    public ParticleSystem system;
    public AudioSource thisSound;
    private void OnEnable()
    {
        system.Play();
        if (thisSound)
        {
            Invoke("PlaySound", .05f);
        }
    }
    private void PlaySound()
    {
        if (thisSound.isActiveAndEnabled)
        {
            thisSound.Play();
        }
    }
}
