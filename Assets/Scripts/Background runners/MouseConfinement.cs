using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseConfinement : MonoBehaviour
{

    void OnApplicationFocus(bool focus)
    {
        //Confine cursor only when game is in focus and not windowed----
        if (focus && GameControl.control.fullScreen)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Confine cursor on startup----
    private void Start()
    {
        if (GameControl.control.fullScreen)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
