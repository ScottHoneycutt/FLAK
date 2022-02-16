using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playerUi;
    //Esc hotkey variables----
    public bool paused;

    public void PauseGame(bool pauseBool)
    {
        pauseMenu.SetActive(pauseBool);
        playerUi.SetActive(!pauseBool);
        AudioListener.pause = pauseBool;

    }
    //Checking for Esc hotkey press----
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                PauseGame(!paused);
            }
            else
            {
                Time.timeScale = 0;
                PauseGame(!paused);
            }
        }
    }
}
