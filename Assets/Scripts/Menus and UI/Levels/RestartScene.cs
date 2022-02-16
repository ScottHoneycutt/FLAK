using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public float delay = .1f;
    public void Restart()
    {
        Invoke("RestartWithDelay", delay);
    }
    private void RestartWithDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
