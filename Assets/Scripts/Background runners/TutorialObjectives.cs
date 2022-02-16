using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjectives : MonoBehaviour {
    //public variables----
    public GameObject playerFighter;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;

    public GameObject directionsUI1;
    public GameObject directionsUI2;
    public GameObject directionsUI3;
    public GameObject MissionCompletedCanvas;
    public GameObject MissionFailedCanvas;

    private bool directions2Used = false;
    private bool directions3Used = false;
    private bool failed = false;
    private bool completed = false;

    private float endDelay = 2;

    private float timer = 0;

    // Use this for initialization
    void Start () {
        //Pausing game at start for first set of directions----
        Time.timeScale = 0;
        directionsUI1.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        //Open second directions----
        if (playerFighter.activeSelf)
        {
            if (playerFighter.transform.position.x > 400 && !directions2Used)
            {
                directions2Used = true;
                directionsUI2.SetActive(true);
                Time.timeScale = 0;
            }
        }
        //Open third directions----
        if (!target1.activeSelf && !target2.activeSelf && !directions3Used && directions2Used)
        {
            directions3Used = true;
            directionsUI3.SetActive(true);
            Time.timeScale = 0;
        }
        //Mission completed----
        if (!target1.activeSelf && !target2.activeSelf && !target3.activeSelf && !target4.activeSelf && !target5.activeSelf && !failed && !completed)
        {
            timer += Time.deltaTime;
            if (timer >= endDelay)
            {
                MissionCompletedCanvas.SetActive(true);
                Time.timeScale = 0;
                completed = true;
                GameControl.control.tutorialCompleted = true;
                GameControl.control.Save();
            }
        }
        //Mission failed----
        if (!playerFighter.activeSelf && !completed && !failed)
        {
            timer += Time.deltaTime;
            if (timer >= endDelay)
            {
                MissionFailedCanvas.SetActive(true);
                Time.timeScale = 0;
                failed = true;
            }
        }
    }
    public void RespawnReset()
    {
        failed = false;
        timer = 0;
    }
}
