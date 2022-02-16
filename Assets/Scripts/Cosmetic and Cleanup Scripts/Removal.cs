using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removal : MonoBehaviour {

    public GameObject destroyTarget;
    public float delay = 1;

    // Use this for initialization
    void OnEnable()
    {
        Invoke("Disable", delay);
    }
    private void Disable()
    {
        destroyTarget.SetActive(false);
    }
}

