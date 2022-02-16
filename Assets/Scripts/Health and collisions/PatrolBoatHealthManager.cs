using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBoatHealthManager : MonoBehaviour
{
    public float refreshdelay = .1f;

    public UnitHP boat;

    public GameObject turret1;
    public GameObject targeter;

    private bool registered = false;

    //calling identiferis on disable----
    private void OnDisable()
    {
        if (registered)
        {
            registered = false;
            //If it is an allied ship----
            if (targeter.layer == 15)
            {
                Identifiers.identifier.RemoveAlliedShipTargeter(targeter);
                Identifiers.identifier.RemoveAlliedFlakMissileTargeter(targeter);
                Identifiers.identifier.ReportAlliedAATargeter(targeter);
            }
            //Enemy ship----
            else
            {
                Identifiers.identifier.RemoveEnemyShipTargeter(targeter);
                Identifiers.identifier.RemoveEnemyFlakMissileTargeter(targeter);
                Identifiers.identifier.RemoveEnemyAATargeter(targeter);
            }
        }
    }

    //Private variables----
    private float timer;

    // Update is called once per frame
    void Update()
    {
        //Calling identifiers to register targeter----
        if (!registered)
        {
            registered = true;

            //If it is an allied ship----
            if (targeter.layer == 15)
            {
                Identifiers.identifier.ReportAlliedShipTargeter(targeter);
                Identifiers.identifier.ReportAlliedFlakMissileTargeter(targeter);
                Identifiers.identifier.ReportAlliedAATargeter(targeter);
            }
            //Enemy ship----
            else
            {
                Identifiers.identifier.ReportEnemyShipTargeter(targeter);
                Identifiers.identifier.ReportEnemyFlakMissileTargeter(targeter);
                Identifiers.identifier.ReportEnemyAATargeter(targeter);
            }
        }

        //Incrementing timer----
        timer += Time.deltaTime;

        //Refreshing----
        if (timer > refreshdelay)
        {
            //Testing turrets----
            if (!turret1.activeSelf)
            {
                boat.health = 0;
            }
        }
    }
}
