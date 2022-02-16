using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSizeRegulator : MonoBehaviour
{
    public GameObject FullscreenActiveButton;
    public GameObject FullscreenInactiveButton;

    int pixelWidth;
    int pixelHeight;

    private GameControl controller;

    //Set up the screen size upon game startup----
    void Start()
    {

        controller = GameControl.control;

        //Determining what the largest supported resolution is----
        Resolution[] rez = Screen.resolutions;
        int tempRezWidth = 0;
        int tempRezHeight = 0;

        for (int i = 0; i< rez.Length; i++)
        {
            //Largest is stored----
            if (tempRezWidth < rez[i].width)
            {
                tempRezWidth = rez[i].width;
            }
            if (tempRezHeight < rez[i].height)
            {
                tempRezHeight = rez[i].height;
            }
        }

        pixelWidth = tempRezWidth;
        pixelHeight = tempRezHeight;

        //Checking GameControl for which FullScreenMode----
        if (controller.fullScreen)
        {
            //Active button is on by default. No change necessary----

            //Checking to see if 16:9 adjusted sizes fit the screen----
            //Pixel height is the limiter;
            if (pixelWidth * 9 / 16 > pixelHeight)
            {
                Screen.SetResolution(pixelHeight * 16 / 9, pixelHeight, FullScreenMode.FullScreenWindow);
            }
            //Pixel width is the limiter----
            else if (pixelWidth * 9 / 16 < pixelHeight)
            {
                Screen.SetResolution(pixelWidth, pixelWidth * 9/16, FullScreenMode.FullScreenWindow);
            }
        }
        //Windowed----
        else
        {
            //Switching which buttons are active----
            FullscreenActiveButton.SetActive(false);
            FullscreenInactiveButton.SetActive(true);

            //Checking to see if 16:9 adjusted sizes fit the screen----
            //Pixel height is the limiter;
            if (pixelWidth * 9 / 16 > pixelHeight)
            {
                Screen.SetResolution(pixelHeight * 16 / 9/2, pixelHeight/2, FullScreenMode.Windowed);
            }
            //Pixel width is the limiter----
            else if (pixelWidth * 9 / 16 < pixelHeight)
            {
                Screen.SetResolution(pixelWidth/2, pixelWidth * 9 / 16/2, FullScreenMode.Windowed);
            }
        }
    }

    //Method to be called when the sceen mode settings are changed----
    public void SetFullScreen(bool fullScreen)
    {
        controller.fullScreen = fullScreen;
        controller.Save();

        //Updating screen immediately----

        //Checking GameControl for which FullScreenMode----
        if (controller.fullScreen)
        {
            //Checking to see if 16:9 adjusted sizes fit the screen----
            //Pixel height is the limiter;
            if (pixelWidth * 9 / 16 > pixelHeight)
            {
                Screen.SetResolution(pixelHeight * 16 / 9, pixelHeight, FullScreenMode.FullScreenWindow);
            }
            //Pixel width is the limiter----
            else if (pixelWidth * 9 / 16 < pixelHeight)
            {
                Screen.SetResolution(pixelWidth, pixelWidth * 9 / 16, FullScreenMode.FullScreenWindow);
            }

            //Confining mouse----
            Cursor.lockState = CursorLockMode.Confined;
        }
        //Windowed----
        else
        {
            //Checking to see if 16:9 adjusted sizes fit the screen----
            //Pixel height is the limiter;
            if (pixelWidth * 9 / 16 > pixelHeight)
            {
                Screen.SetResolution(pixelHeight * 16 / 9/2, pixelHeight/2, FullScreenMode.Windowed);
            }
            //Pixel width is the limiter----
            else if (pixelWidth * 9 / 16 < pixelHeight)
            {
                Screen.SetResolution(pixelWidth/2, pixelWidth * 9 / 16/2, FullScreenMode.Windowed);
            }

            //Unlocking mouse----
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
