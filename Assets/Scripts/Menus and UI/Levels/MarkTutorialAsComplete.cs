using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTutorialAsComplete : MonoBehaviour {
    public void TutorialCompleted()
    {
        GameControl.control.tutorialCompleted = true;
    }
}
