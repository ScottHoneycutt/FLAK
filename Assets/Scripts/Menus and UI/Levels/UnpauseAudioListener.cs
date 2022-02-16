using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseAudioListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AudioListener.pause = false;
    }
}
