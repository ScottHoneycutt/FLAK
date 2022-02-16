using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    public AudioSource source;
    public AudioClip sound;
    public float volume;
    // Start is called before the first frame update
    void OnEnable()
    {
        source.PlayOneShot(sound, volume);
    }
}
