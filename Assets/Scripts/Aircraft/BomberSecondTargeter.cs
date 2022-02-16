using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSecondTargeter : MonoBehaviour
{
    //Targeter variables----
    public GameObject alliedAATargeter;
    public GameObject enemyAATargeter;
    public GameObject alliedFlakMissileTargeter;
    public GameObject enemyFlakMissileTargeter;
    public GameObject enemyMissileTargeter;
    public GameObject alliedMissileTargeter;
    public GameObject alliedShipTargeter;
    public GameObject enemyShipTargeter;

    private bool isReported = false;

    //Removing targeters from identifiers upon disable----
    private void OnDisable()
    {
        if (isReported)
        {
            isReported = false;
            if (alliedAATargeter)
            {
                Identifiers.identifier.RemoveAlliedAATargeter(alliedAATargeter);
            }
            if (alliedFlakMissileTargeter)
            {
                Identifiers.identifier.RemoveAlliedFlakMissileTargeter(alliedFlakMissileTargeter);
            }
            if (enemyAATargeter)
            {
                Identifiers.identifier.RemoveEnemyAATargeter(enemyAATargeter);
            }
            if (enemyFlakMissileTargeter)
            {
                Identifiers.identifier.RemoveEnemyFlakMissileTargeter(enemyFlakMissileTargeter);
            }
            if (alliedMissileTargeter)
            {
                Identifiers.identifier.RemoveAlliedMissile(alliedMissileTargeter);
            }
            if (enemyMissileTargeter)
            {
                Identifiers.identifier.RemoveEnemyMissile(enemyMissileTargeter);
            }
            if (alliedShipTargeter)
            {
                Identifiers.identifier.RemoveAlliedShipTargeter(alliedShipTargeter);
            }
            if (enemyShipTargeter)
            {
                Identifiers.identifier.RemoveAlliedShipTargeter(enemyShipTargeter);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isReported)
        {
            isReported = true;
            //Reporting targeters to identifiers upon enable----
            if (alliedAATargeter)
            {
                Identifiers.identifier.ReportAlliedAATargeter(alliedAATargeter);
            }
            if (alliedFlakMissileTargeter)
            {
                Identifiers.identifier.ReportAlliedFlakMissileTargeter(alliedFlakMissileTargeter);
            }
            if (enemyAATargeter)
            {
                Identifiers.identifier.ReportEnemyAATargeter(enemyAATargeter);
            }
            if (enemyFlakMissileTargeter)
            {
                Identifiers.identifier.ReportEnemyFlakMissileTargeter(enemyFlakMissileTargeter);
            }
            if (alliedMissileTargeter)
            {
                Identifiers.identifier.ReportAlliedMissile(alliedMissileTargeter);
            }
            if (enemyMissileTargeter)
            {
                Identifiers.identifier.ReportEnemyMissile(enemyMissileTargeter);
            }
            if (alliedShipTargeter)
            {
                Identifiers.identifier.ReportAlliedShipTargeter(alliedShipTargeter);
            }
            if (enemyShipTargeter)
            {
                Identifiers.identifier.ReportAlliedShipTargeter(enemyShipTargeter);
            }
        }
    }
}
