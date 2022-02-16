using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionLaunchEffects : MonoBehaviour
{
    public Graphic image;
    public void LaunchMission(float fadeDuration)
    {
        image.canvasRenderer.SetAlpha(0);
        image.CrossFadeAlpha(1, fadeDuration, true);
    } 
}
