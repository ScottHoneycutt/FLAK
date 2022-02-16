using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
    public void SaveGame()
    {
        GameControl.control.Save();
    }
}
