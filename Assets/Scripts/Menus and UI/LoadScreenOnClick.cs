using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenOnClick : MonoBehaviour {

    public int sceneIndex = 0;
    public float delay = 0.3f;

    public void LoadSceneDelay()
    {
        Invoke("LoadScene", delay);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
