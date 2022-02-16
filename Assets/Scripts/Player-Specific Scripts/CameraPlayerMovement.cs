using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerMovement : MonoBehaviour {

    //Public camera variables----
    public GameObject playerFighter;
    public float cameraZoom;

    //Camera shake extremity variables----
    public float afterburnShake = .5f;
    public float cannonShootingShake = 1;
    public float missileShootShake = 1;
    public float lightDamageShake = 2;
    public float mediumDamageShake = 2;
    public float heavyDamageShake = 2;
    public float railgunShootingShake = .6f;

    //Camera shake boolean variables----
    public bool afterburnBool = false;
    public bool cannonShootingBool = false;
    public bool missileShootBool = false;
    public bool lightDamageBool = false;
    public bool mediumDamageBool = false;
    public bool heavyDamageBool = false;
    public bool railgunShootingBool = false;

    //Private camera variables----
    private float shake = 0;
    private float mouseX = 0;
    private float mouseY = 0;


    //Public radar/UI variables----
    public float listRefreshDelay = 0.1f;
    public float radarRange = 1000;
    public float radarMinRange = 225;

    public float uiRange = 225;

    public GameObject radarSphere;
    public GameObject uiPanel;

    //Private radar variables----
    private float listTimer = 0;
    private float updateTimer = 0;
    private Camera mainCamera;
    private Transform cameraTransform;
    private GameObject drone;

    private List<GameObject> enemyMissiles;
    private List<GameObject> alliedMissiles;
    private List<GameObject> enemies;
    private List<GameObject> allies;
    private List<GameObject> neutrals;

    Vector2 disappear;
    private List<RectTransform> allRectTransforms;

    //Flight sound smoother----
    private FlightSoundSmoother soundSmoother;

    // Use this for initialization
    void Start () {
        //RADAR AND UI LISTS----
        //Initializing lists----
        enemyMissiles = Identifiers.identifier.ReturnEnemyMissiles();
        alliedMissiles = Identifiers.identifier.ReturnAlliedMissiles();
        enemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
        allies = Identifiers.identifier.ReturnEnemyFlakMissileTargets();
        neutrals = Identifiers.identifier.ReturnNeutralTargeters();

        allRectTransforms = new List<RectTransform>();

        //Camera----
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;

        //Resetting camera shake variables----
        GameControl.control.lightDamageBool = false;
        GameControl.control.mediumDamageBool = false;
        GameControl.control.heavyDamageBool = false;
        GameControl.control.cannonShootingBool = false;
        GameControl.control.railgunShootingBool = false;
        GameControl.control.missileShootBool = false;

        //Vector2 for "removal"----
        disappear = new Vector2(10000, 10000);

        //Initializing soundSmoother----
        soundSmoother = playerFighter.GetComponent<FlightSoundSmoother>();
    }
	
	// LateUpdate is called once per frame
	void LateUpdate () {

        //CAMERA MOVEMENT------------------------------------------------------------------------------------------------------------------------

        //Dynamic camera movement----
        float sens = GameControl.control.dcmSens;
        float range = GameControl.control.dcmRange;
        //Mouse positions----
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        mouseX = mouseX - (Screen.width) * (float)(.5);
        mouseY = mouseY - (Screen.height) * (float)(.5);

        //Modifying mouseX and mouseY----
        mouseX = mouseX / Screen.width * 100 * sens;
        mouseY = mouseY / Screen.height * 100 * sens;

        //Maximum range for DCM----
        if (Mathf.Sqrt(Mathf.Pow(mouseX, 2) + Mathf.Pow(mouseY, 2)) > 25 * range)
        {
            //Finding proportions----
            float xContribution = mouseX / Mathf.Sqrt(Mathf.Pow(mouseX, 2) + Mathf.Pow(mouseY, 2));
            float yContribution = mouseY / Mathf.Sqrt(Mathf.Pow(mouseX, 2) + Mathf.Pow(mouseY, 2));
            //Capping DCM movement----
            mouseX = 25 * range * xContribution;
            mouseY = 25 * range * yContribution;
        }


        //Reset camera shake----
        shake = 0;

        //Grab damage status/camera shake variables from the GameControl----
        lightDamageBool = GameControl.control.lightDamageBool;
        mediumDamageBool = GameControl.control.mediumDamageBool;
        heavyDamageBool = GameControl.control.heavyDamageBool;
        cannonShootingBool = GameControl.control.cannonShootingBool;
        railgunShootingBool = GameControl.control.railgunShootingBool;
        missileShootBool = GameControl.control.missileShootBool;


        if (playerFighter.activeSelf)
        {
            if (Time.timeScale == 1)
            {
                // Adding up camera shake----
                if (afterburnBool)
                {
                    shake += afterburnShake;
                }
                if (cannonShootingBool)
                {
                    shake += cannonShootingShake;
                }
                if (missileShootBool)
                {
                    shake += missileShootShake;
                }
                if (lightDamageBool)
                {
                    shake += lightDamageShake;
                }
                if (mediumDamageBool)
                {
                    shake += mediumDamageShake;
                }
                if (heavyDamageBool)
                {
                    shake += heavyDamageShake;
                }
                if (railgunShootingBool)
                {
                    shake += railgunShootingShake;
                }
            }

            Vector3 a = playerFighter.transform.position;
            //Changing camera position with dynamic movement and random variables to simulate shake----
            cameraTransform.position = new Vector3(a.x + Random.Range(-shake, shake) + mouseX, a.y + Random.Range(-shake, shake) + mouseY, cameraZoom);
            //Making sure the camera does not dip below the water----
            if(cameraTransform.position.y < .5f)
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x, .5f, cameraZoom);
            }
        }

        //END OF CAMERA MOVEMENT------------------------------------------------------------------------------------------------------------------------



        //RADAR AND UI INDICATORS FOR HUD------------------------------------------------------------------------------------------------------------------------


        //Incrementing timers----
        listTimer += Time.deltaTime;
        updateTimer += Time.deltaTime;

        //Refreshing lists----
        if (listTimer >= listRefreshDelay)
        {
            listTimer = 0;
            //Get lists from identifier----
            enemyMissiles = Identifiers.identifier.ReturnEnemyMissiles();
            alliedMissiles = Identifiers.identifier.ReturnAlliedMissiles();
            enemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
            allies = Identifiers.identifier.ReturnEnemyFlakMissileTargets();
            neutrals = Identifiers.identifier.ReturnNeutralTargeters();

            //Adding drone manually (it does not have a targetter because it is not supposed to be attacked by enemies)----
            drone = Identifiers.identifier.SendEscortDrone();
        }

        //Purge old UI and Radar indicators----
        foreach (RectTransform ping in allRectTransforms)
        {
            ping.anchoredPosition = disappear;
        }
        allRectTransforms.Clear();

        //Resetting soundSmoother boolean----
        soundSmoother.missilesNear = false;

        //Radar stuff----
        if (playerFighter && Time.timeScale == 1)
        {
            Vector3 cameraCenter = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, playerFighter.transform.position.z);
            //enemyMissiles list----
            for (int i = 0; i < enemyMissiles.Count; i++)
            {
                if (enemyMissiles[i] != null)
                {
                    //Min/max radar range tests----
                    if (Vector3.Distance(enemyMissiles[i].transform.position, cameraCenter) <= radarRange && Vector3.Distance(enemyMissiles[i].transform.position, cameraCenter) >= radarMinRange)
                    {
                        Vector3 directionVector = enemyMissiles[i].transform.position - cameraCenter;
                        //Setting correct scale for positioning----
                        directionVector = directionVector / radarRange * 300;
                        //Instantiating indicator with radarSphere as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Enemy Missile Radars");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                        //Notifying soundSmoother----
                        soundSmoother.missilesNear = true;
                    }

                    //UI range tests----
                    else if (Vector3.Distance(enemyMissiles[i].transform.position, cameraCenter) <= uiRange)
                    {
                        Vector3 directionVector = enemyMissiles[i].transform.position;
                        //Setting correct scale for positioning----
                        directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                        //Instantiating indicator with uiCanvas as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Enemy Missile UIs");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                        //Notifying soundSmoother----
                        soundSmoother.missilesNear = true;
                    }
                }
            }

            //alliedMissiles list---- 
            for (int i = 0; i < alliedMissiles.Count; i++)
            {
                if (alliedMissiles[i] != null)
                {
                    //Min/max radar range tests----
                    if (Vector3.Distance(alliedMissiles[i].transform.position, cameraCenter) <= radarRange && Vector3.Distance(alliedMissiles[i].transform.position, cameraCenter) >= radarMinRange)
                    {
                        Vector3 directionVector = alliedMissiles[i].transform.position - cameraCenter;
                        //Setting correct scale for positioning----
                        directionVector = directionVector / radarRange * 300;
                        //Instantiating indicator with radarSphere as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Allied Missile Radars");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                        soundSmoother.missilesNear = true;
                    }

                    //UI range tests----
                    else if (Vector3.Distance(alliedMissiles[i].transform.position, cameraCenter) <= uiRange)
                    {
                        Vector3 directionVector = alliedMissiles[i].transform.position;
                        //Setting correct scale for positioning----
                        directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                        //Instantiating indicator with uiCanvas as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Allied Missile UIs");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                        soundSmoother.missilesNear = true;
                    }
                }
            }

            //enemies list----
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    //Min/max radar range tests----
                    if (Vector3.Distance(enemies[i].transform.position, cameraCenter) <= radarRange && Vector3.Distance(enemies[i].transform.position, cameraCenter) >= radarMinRange)
                    {
                        Vector3 directionVector = enemies[i].transform.position - cameraCenter;
                        //Setting correct scale for positioning----
                        directionVector = directionVector / radarRange * 300;
                        //Instantiating indicator with radarSphere as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Enemy Unit Radars");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                    }

                    //UI range tests----
                    else if (Vector3.Distance(enemies[i].transform.position, cameraCenter) <= uiRange)
                    {
                        Vector3 directionVector = enemies[i].transform.position;
                        //Setting correct scale for positioning----
                        directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                        //Instantiating indicator with uiCanvas as parent----
                        RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Enemy Unit UIs");
                        indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                        //Adding to list of allRectTransforms for reference for deactivation----
                        allRectTransforms.Add(indicator);
                    }
                }
            }

            //Neutrals list----
            for (int i = 0; i < neutrals.Count; i++)
            {
                //Min/max radar range tests----
                if (Vector3.Distance(neutrals[i].transform.position, cameraCenter) <= radarRange && Vector3.Distance(neutrals[i].transform.position, cameraCenter) >= radarMinRange)
                {
                    Vector3 directionVector = neutrals[i].transform.position - cameraCenter;
                    //Setting correct scale for positioning----
                    directionVector = directionVector / radarRange * 300;
                    //Instantiating indicator with radarSphere as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Neutral Unit Radars");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }

                //UI range tests----
                else if (Vector3.Distance(neutrals[i].transform.position, cameraCenter) <= uiRange)
                {
                    Vector3 directionVector = neutrals[i].transform.position;
                    //Setting correct scale for positioning----
                    directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                    //Instantiating indicator with uiCanvas as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Neutral Unit UIs");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }
            }

            //Allies list----
            for (int i = 0; i < allies.Count; i++)
            {
                //Min/max radar range tests----
                if (Vector3.Distance(allies[i].transform.position, cameraCenter) <= radarRange && Vector3.Distance(allies[i].transform.position, cameraCenter) >= radarMinRange)
                {
                    Vector3 directionVector = allies[i].transform.position - cameraCenter;
                    //Setting correct scale for positioning----
                    directionVector = directionVector / radarRange * 300;
                    //Instantiating indicator with radarSphere as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Allied Unit Radars");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }

                //UI range tests----
                else if (Vector3.Distance(allies[i].transform.position, cameraCenter) <= uiRange)
                {
                    Vector3 directionVector = allies[i].transform.position;
                    //Setting correct scale for positioning----
                    directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                    //Instantiating indicator with uiCanvas as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Allied Unit UIs");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding text indicators for NPCs----
                    if (allies[i].transform.parent.name == "Alpine")
                    {
                        RectTransform alpineText = ObjectPoolManager.manager.SpawnForUI("Alpine Text");
                        alpineText.anchoredPosition = new Vector2(directionVector.x, directionVector.y + Screen.height / 25);
                        allRectTransforms.Add(alpineText);
                    }
                    else if (allies[i].transform.parent.name == "Javelin")
                    {
                        RectTransform javelinText = ObjectPoolManager.manager.SpawnForUI("Javelin Text");
                        javelinText.anchoredPosition = new Vector2(directionVector.x, directionVector.y + Screen.height / 25);
                        allRectTransforms.Add(javelinText);
                    }
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }
            }

            //Adding the drone----
            //Min/max radar range tests----
            if (drone && drone.activeInHierarchy)
            {
                if (Vector3.Distance(drone.transform.position, cameraCenter) <= radarRange && Vector3.Distance(drone.transform.position, cameraCenter) >= radarMinRange)
                {
                    Vector3 directionVector = drone.transform.position - cameraCenter;
                    //Setting correct scale for positioning----
                    directionVector = directionVector / radarRange * 300;
                    //Instantiating indicator with radarSphere as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForRadar("Allied Unit Radars");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }

                //UI range tests----
                else if (Vector3.Distance(drone.transform.position, cameraCenter) <= uiRange)
                {
                    Vector3 directionVector = drone.transform.position;
                    //Setting correct scale for positioning----
                    directionVector = new Vector3(mainCamera.WorldToScreenPoint(directionVector).x - Screen.width / 2, mainCamera.WorldToScreenPoint(directionVector).y - Screen.height / 2, 0);
                    //Instantiating indicator with uiCanvas as parent----
                    RectTransform indicator = ObjectPoolManager.manager.SpawnForUI("Allied Unit UIs");
                    indicator.anchoredPosition = new Vector2(directionVector.x, directionVector.y);
                    //Adding to list of allRectTransforms for reference for deactivation----
                    allRectTransforms.Add(indicator);
                }
            }
        }

        //END OF RADAR AND UI INDICATORS FOR HUD------------------------------------------------------------------------------------------------------------------------
    }
}
