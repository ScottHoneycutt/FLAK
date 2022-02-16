using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    public float volume = 1;
    public AudioSource uiAudioSource;

    public void playSoundOnClick(AudioClip sound)
    {
        uiAudioSource.PlayOneShot(sound, volume);
    }
}
