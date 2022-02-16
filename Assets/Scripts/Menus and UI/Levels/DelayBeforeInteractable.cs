using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DelayBeforeInteractable : MonoBehaviour
{
    //public variables----
    public float delay = .7f;
    public Button firstButton;
    //private variables----
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        //Timer in spite of timescale----
        timer += Time.unscaledDeltaTime;
        if (timer >= delay)
        {
            //Setting button interactable----
            firstButton.interactable = true;
        }
    }
}
