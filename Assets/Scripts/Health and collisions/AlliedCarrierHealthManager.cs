using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedCarrierHealthManager : MonoBehaviour
{
    //Aircraft deployment variables----
    public int sMaxFighterActive = 8;
    public float sDeployPeriod = 3;
    public bool level2Aircraft = false;
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    public Transform spawnPoint;

    public GameObject shipTargeter1;
    public GameObject shipTargeter2;
    public GameObject shipTargeter3;
    private bool isRegistered = false;

    //Anti-Tampering measures----
    SecretInt maxFighterActive;
    SecretFloat deployPeriod;

    private float deployTimer = 0;
    private int activeCount = 0;
    private List<GameObject> fighters;

    private AlliedFighterMovement fighterMovement;

    //HP variables----
    public float turretHealthEquivalent = 200;
    public float refreshdelay = .1f;

    public UnitHP carrier;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;

    //Private variables----
    private int startTurretCount = 4;
    private int turretCount;

    private float timer;

    //Anti-Tampering measures----
    private void Awake()
    {
        maxFighterActive = new SecretInt(sMaxFighterActive);
        deployPeriod = new SecretFloat(sDeployPeriod);
    }

    private void OnEnable()
    {
        startTurretCount = 4;
        deployTimer = deployPeriod.GetFloat();
        fighters = new List<GameObject>();
    }
    //Removing targeters from identifier list on disable----
    private void OnDisable()
    {
        if (isRegistered)
        {
            isRegistered = false;

            Identifiers.identifier.RemoveAlliedShipTargeter(shipTargeter1);
            Identifiers.identifier.RemoveAlliedShipTargeter(shipTargeter2);
            Identifiers.identifier.RemoveAlliedShipTargeter(shipTargeter3);

            Identifiers.identifier.RemoveAlliedFlakMissileTargeter(shipTargeter1);
            Identifiers.identifier.RemoveAlliedFlakMissileTargeter(shipTargeter2);
            Identifiers.identifier.RemoveAlliedFlakMissileTargeter(shipTargeter3);

            Identifiers.identifier.RemoveAlliedAATargeter(shipTargeter1);
            Identifiers.identifier.RemoveAlliedAATargeter(shipTargeter2);
            Identifiers.identifier.RemoveAlliedAATargeter(shipTargeter3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRegistered)
        {
            isRegistered = true;
            //Sending targeters to identifiers----
            Identifiers.identifier.ReportAlliedShipTargeter(shipTargeter1);
            Identifiers.identifier.ReportAlliedShipTargeter(shipTargeter2);
            Identifiers.identifier.ReportAlliedShipTargeter(shipTargeter3);

            Identifiers.identifier.ReportAlliedFlakMissileTargeter(shipTargeter1);
            Identifiers.identifier.ReportAlliedFlakMissileTargeter(shipTargeter2);
            Identifiers.identifier.ReportAlliedFlakMissileTargeter(shipTargeter3);

            Identifiers.identifier.ReportAlliedAATargeter(shipTargeter1);
            Identifiers.identifier.ReportAlliedAATargeter(shipTargeter2);
            Identifiers.identifier.ReportAlliedAATargeter(shipTargeter3);
        }

        //Incrementing timers----
        timer += Time.deltaTime;
        deployTimer += Time.deltaTime;

        //Updating the list of active fighters----
        for (int i = 0; i < fighters.Count; i++)
        {
            //Removing inactive (destroyed) fighters from list----
            if (!fighters[i].activeInHierarchy)
            {
                fighters.Remove(fighters[i]);
            }
        }

        activeCount = fighters.Count;
        //Deploying a new aircraft if max capacity has not been reached every deployPeriod----
        if (deployTimer >= deployPeriod.GetFloat() && activeCount < maxFighterActive.GetInt())
        {
            GameObject launchedAircraft;
            if (!level2Aircraft)
            {
                launchedAircraft = ObjectPoolManager.manager.SpawnFromPool("Allied Fighters", spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                launchedAircraft = ObjectPoolManager.manager.SpawnFromPool("Allied Fighters Level 2", spawnPoint.position, spawnPoint.rotation);
            }
            fighters.Add(launchedAircraft);
            //Setting fighter to launch and setting patrolpoints----
            fighterMovement = launchedAircraft.GetComponent<AlliedFighterMovement>();
            fighterMovement.takeoff = true;
            fighterMovement.patrolPoint1 = patrolPoint2;
            fighterMovement.patrolPoint2 = patrolPoint1;
            //Reset timer----
            deployTimer = 0;
        }

        //Refreshing Health stuff----
        if (timer > refreshdelay)
        {
            turretCount = 0;
            timer = 0;

            //Testing turrets----
            if (turret1.activeSelf)
            {
                turretCount++;
            }
            if (turret2.activeSelf)
            {
                turretCount++;
            }
            if (turret3.activeSelf)
            {
                turretCount++;
            }
            if (turret4.activeSelf)
            {
                turretCount++;
            }

            if (turretCount < startTurretCount)
            {
                //Subtracting health----
                carrier.health -= (startTurretCount - turretCount) * turretHealthEquivalent;
                startTurretCount = turretCount;
            }
        }
    }
}
