using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject tutorialMenu;

    private void Start()
    {
        //Initialize Tutorial for new players----
        if (!GameControl.control.tutorialCompleted)
        {
            mainMenu.SetActive(false);
            tutorialMenu.SetActive(true);
        }
    }
}
