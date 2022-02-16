using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerHealthManager : MonoBehaviour {

    public float turretHealthEquivalent = 100;
    public float cannonHealthEquivalent = 200;
    public float refreshdelay = .1f;

    public UnitHP destroyer;

    public GameObject frontCannon;
    public GameObject midCannon;
    public GameObject rearCannon;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;
    public GameObject turret5;

    public GameObject shipTargeter1;
    public GameObject shipTargeter2;
    public GameObject shipTargeter3;
    private bool isRegistered = false;

    //Private variables----
    private int startTurretCount = 5;
    private int startCannonCount = 3;
    private int turretCount;
    private int cannonCount;

    private float timer;

    private void OnEnable()
    {
        startTurretCount = 5;
        startCannonCount = 3;
    }
    //Removing targeters from identifier list on disable----
    private void OnDisable()
    {
        if (isRegistered)
        {
            isRegistered = false;
            //If it is an allied ship----
            if (shipTargeter1.layer == 15)
            {
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
            //Enemy ship----
            else
            {
                Identifiers.identifier.RemoveEnemyShipTargeter(shipTargeter1);
                Identifiers.identifier.RemoveEnemyShipTargeter(shipTargeter2);
                Identifiers.identifier.RemoveEnemyShipTargeter(shipTargeter3);

                Identifiers.identifier.RemoveEnemyFlakMissileTargeter(shipTargeter1);
                Identifiers.identifier.RemoveEnemyFlakMissileTargeter(shipTargeter2);
                Identifiers.identifier.RemoveEnemyFlakMissileTargeter(shipTargeter3);

                Identifiers.identifier.RemoveEnemyAATargeter(shipTargeter1);
                Identifiers.identifier.RemoveEnemyAATargeter(shipTargeter2);
                Identifiers.identifier.RemoveEnemyAATargeter(shipTargeter3);
            }
        }
    }


    // Update is called once per frame
    void Update () {

        //Sending targeters to identifiers----
        if (!isRegistered && shipTargeter1)
        {
            isRegistered = true;
            //If it is an allied ship----
            if (shipTargeter1.layer == 15)
            {
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
            //Enemy ship----
            else
            {
                Identifiers.identifier.ReportEnemyShipTargeter(shipTargeter1);
                Identifiers.identifier.ReportEnemyShipTargeter(shipTargeter2);
                Identifiers.identifier.ReportEnemyShipTargeter(shipTargeter3);

                Identifiers.identifier.ReportEnemyFlakMissileTargeter(shipTargeter1);
                Identifiers.identifier.ReportEnemyFlakMissileTargeter(shipTargeter2);
                Identifiers.identifier.ReportEnemyFlakMissileTargeter(shipTargeter3);

                Identifiers.identifier.ReportEnemyAATargeter(shipTargeter1);
                Identifiers.identifier.ReportEnemyAATargeter(shipTargeter2);
                Identifiers.identifier.ReportEnemyAATargeter(shipTargeter3);
            }
        }


        //Incrementing timer----
        timer += Time.deltaTime;

        //Refreshing----
        if (timer > refreshdelay)
        {
            turretCount = 0;
            cannonCount = 0;
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
            if (turret5.activeSelf)
            {
                turretCount++;
            }

            if (turretCount < startTurretCount)
            {
                //Subtracting health----
                destroyer.health -= (startTurretCount - turretCount) * turretHealthEquivalent;
                startTurretCount = turretCount;
            }

            //Testing cannons----
            if (frontCannon.activeSelf)
            {
                cannonCount++;
            }
            if (midCannon.activeSelf)
            {
                cannonCount++;
            }
            if (rearCannon.activeSelf)
            {
                cannonCount++;
            }

            if (cannonCount < startCannonCount)
            {
                //Subtracting health----
                destroyer.health -= (startCannonCount - cannonCount) * cannonHealthEquivalent;
                startCannonCount = cannonCount;
            }
        }
    }
}
