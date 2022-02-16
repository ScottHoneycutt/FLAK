using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileReload : MonoBehaviour {

    //Public variables----
    public float reloadSpeed = 60;
    public GameObject uiReload;
    public GameObject playerFighter;

    //Sound variables----
    public AudioClip missileLaunch;
    public AudioClip missileClick;
    public AudioSource missileSoundRegulator;

    public float clickVolume = 1;

    //Camera shake variables----
    public float launchCameraShakeDuration = .3f;

    private float timer = 0;

    //private variables----
    private bool loaded = true;
    private Quaternion rotation;

    //Dump variables for tech upgrades----
    private float dumpReloadSpeed;

    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    //Factoring in missile upgrades----
    void Start () {
        //Tech upgrades----
        if (controller.markIIIAutoloadersOwned.VerifyBool(controller.pMarkIIIAutoloadersOwned))
        {
            reloadSpeed = reloadSpeed * 1.25f;
        }
        //Utilities----
        if (controller.rocketPodsEquipped.VerifyBool(controller.pRocketPodsEquipped))
        {
            reloadSpeed = reloadSpeed * 0.75f;
        }

        //Dumping variables----
        dumpReloadSpeed = reloadSpeed;
        //Desperation Module (Tech upgrades)----
        if (controller.desperationModuleOwned.VerifyBool(controller.pDesperationModuleOwned))
        {
            StartCoroutine("RefreshDesperationModule");
        }
    }

    IEnumerable RefreshDesperationModule()
    {
        while (true)
        {
            if (controller.criticalHealth)
            {
                reloadSpeed = dumpReloadSpeed* 1.2f;
            }
            else
            {
                reloadSpeed = dumpReloadSpeed;
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    // Update is called once per frame
    void Update () {
        
        //RMB down?----
        bool isRightMouseDown = Input.GetMouseButton(1);

        //UI reload position----
        RectTransform reloadRectTransform = uiReload.GetComponent<RectTransform>();
        Vector2 reloadPosition = reloadRectTransform.anchoredPosition;
        float reloadX = reloadPosition.x;

        //Loaded----
        if (reloadX >= 0)
        {
            loaded = true;
        }

        //Reloading----
        if (!loaded)
        {
            reloadRectTransform.anchoredPosition = new Vector2(reloadX + (reloadSpeed*Time.deltaTime), 0);
        }

        //Unloading (firing)----
        if (loaded && isRightMouseDown && Time.timeScale == 1)
        {
            loaded = false;
            reloadRectTransform.anchoredPosition = new Vector2 (-135, 0);
            //Creating missile that will be fired----
            rotation = playerFighter.transform.rotation;

            if (!controller.rocketPodsEquipped.VerifyBool(controller.pRocketPodsEquipped))
            {
                ObjectPoolManager.manager.SpawnFromPool("Player Missiles", playerFighter.transform.position, rotation);
                if (controller.missileBaysEquipped.VerifyBool(controller.pMissileBaysEquipped))
                {
                    Invoke("FireSecondMissile", .3f);
                }
            }
            else if (controller.rocketPodsEquipped.VerifyBool(controller.pRocketPodsEquipped))
            {
                for (int i = 0; i < 6; i++)
                {
                    ObjectPoolManager.manager.SpawnFromPool("Player Rockets", playerFighter.transform.position, rotation);
                }
                if (controller.missileBaysEquipped.VerifyBool(controller.pMissileBaysEquipped))
                {
                    Invoke("FireSecondRocketPod",.3f);
                }
            }

            //Firing Sound----
            missileSoundRegulator.Play();

            //Starting camera shake----
            controller.missileShootBool = true;
        }
        //Sound for when the missile is not loaded but trigger is pulled----
        if (Input.GetMouseButtonDown(1) && !loaded && Time.timeScale == 1)
        {
            missileSoundRegulator.PlayOneShot(missileClick, clickVolume);
        }

        //Ending camera shake after timer expires----
        if(controller.missileShootBool == true)
        {
            timer += Time.deltaTime;
            if(timer >= launchCameraShakeDuration)
            {
                timer = 0;
                controller.missileShootBool = false;
            }
        }
	}
    

    //Other Methods----
    void FireSecondMissile()
    {
        ObjectPoolManager.manager.SpawnFromPool("Player Missiles", playerFighter.transform.position, rotation);
        //Firing Sound----
        missileSoundRegulator.PlayOneShot(missileLaunch, 1);

        //Starting camera shake----
        controller.missileShootBool = true;
    }
    void FireSecondRocketPod()
    {
        for (int i = 0; i < 6; i++)
        {
            ObjectPoolManager.manager.SpawnFromPool("Player Rockets", playerFighter.transform.position, rotation);
            //Firing Sound----
            missileSoundRegulator.PlayOneShot(missileLaunch, .2f);

            //Starting camera shake----
            controller.missileShootBool = true;
        }
    }
}
