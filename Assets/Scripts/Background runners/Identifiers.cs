using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifiers : MonoBehaviour {

    //Public variables----
    public float refreshDelay = .1f;

    //Class Instance-----
    public static Identifiers identifier;

    //Private lists of identifier Targeters---
    private List<GameObject> possibleEnemyFlakMissileTargets = new List<GameObject>(); //Layer 15
    private List<GameObject> possibleAlliedFlakMissileTargets = new List<GameObject>(); //Layer 17
    private List<GameObject> possibleEnemyAATargets = new List<GameObject>(); //Layer 14
    private List<GameObject> possibleAlliedAATargets = new List<GameObject>(); //Layer 16
    private List<GameObject> alliedMissiles = new List<GameObject>(); //Layer 21
    private List<GameObject> enemyMissiles = new List<GameObject>(); //Layer 22

    //Ships----
    private List<GameObject> enemyShipTargeters = new List<GameObject>();
    private List<GameObject> alliedShipTargeters = new List<GameObject>();

    //Neutral units----
    private List<GameObject> neutralTargeters = new List<GameObject>();

    //Private velocity vector of player fighter for use by bombs----
    private Vector3 playerVelocityVector = new Vector3(0,0,0);

    //Drone----
    private GameObject escortDrone = null;

    // Use this for initialization
    void Awake()
    {
        //Instant initialization;
        identifier = this;
    }

    //Send method for player velocity for bomb use----
    public void SendPlayerVelocity(Vector3 vector)
    {
        playerVelocityVector = vector;
    }
    //Get method for player velocity for bomb use----
    public Vector3 GetPlayerVelocity()
    {
        return playerVelocityVector;
    }
    
    //Get method for escort drone----
    public void GetEscortDrone(GameObject drone)
    {
        escortDrone = drone;
    }
    //Send method for escort drone----
    public GameObject SendEscortDrone()
    {
        return escortDrone;
    }

    //Report methods for constructing lists. Lists are filled and emptied by the objects themselves using the UnitHP or various boat regulation script------------------------------------------------------------------------------------------
    public void ReportAlliedAATargeter(GameObject targeter)
    {
        possibleEnemyAATargets.Add(targeter);
    }
    public void ReportEnemyAATargeter(GameObject targeter)
    {
        possibleAlliedAATargets.Add(targeter);
    }
    public void ReportAlliedFlakMissileTargeter(GameObject targeter)
    {
        possibleEnemyFlakMissileTargets.Add(targeter);
    }
    public void ReportEnemyFlakMissileTargeter(GameObject targeter)
    {
        possibleAlliedFlakMissileTargets.Add(targeter);
    }
    public void ReportEnemyMissile(GameObject targeter)
    {
        enemyMissiles.Add(targeter);
    }
    public void ReportAlliedMissile(GameObject targeter)
    {
        alliedMissiles.Add(targeter);
    }
    public void ReportAlliedShipTargeter(GameObject targeter)
    {
        alliedShipTargeters.Add(targeter);
    }
    public void ReportEnemyShipTargeter(GameObject targeter)
    {
         enemyShipTargeters.Add(targeter);
    }
    public void ReportNeutralTargeter(GameObject targeter)
    {
        neutralTargeters.Add(targeter);
    }


    //Remove methods for taking disabled objects off the lists------------------------------------------------------------------------------------------
    public void RemoveAlliedAATargeter(GameObject targeter)
    {
        possibleEnemyAATargets.Remove(targeter);
    }
    public void RemoveEnemyAATargeter(GameObject targeter)
    {
        possibleAlliedAATargets.Remove(targeter);
    }
    public void RemoveAlliedFlakMissileTargeter(GameObject targeter)
    {
        possibleEnemyFlakMissileTargets.Remove(targeter);
    }
    public void RemoveEnemyFlakMissileTargeter(GameObject targeter)
    {
        possibleAlliedFlakMissileTargets.Remove(targeter);
    }
    public void RemoveEnemyMissile(GameObject targeter)
    {
        enemyMissiles.Remove(targeter);
    }
    public void RemoveAlliedMissile(GameObject targeter)
    {
        alliedMissiles.Remove(targeter);
    }
    public void RemoveAlliedShipTargeter(GameObject targeter)
    {
        alliedShipTargeters.Remove(targeter);
    }
    public void RemoveEnemyShipTargeter(GameObject targeter)
    {
        enemyShipTargeters.Remove(targeter);
    }
    public void RemoveNeutralTargeter(GameObject targeter)
    {
        neutralTargeters.Remove(targeter);
    }


    //Return methods for sending lists------------------------------------------------------------------------------------------
    public List<GameObject> ReturnEnemyFlakMissileTargets()
    {
        return possibleEnemyFlakMissileTargets;
    }
    public List<GameObject> ReturnAlliedFlakMissileTargets()
    {
        return possibleAlliedFlakMissileTargets;
    }
    public List<GameObject> ReturnEnemyAATargets()
    {
        return possibleEnemyAATargets;
    }
    public List<GameObject> ReturnAlliedAATargets()
    {
        return possibleAlliedAATargets;
    }
    public List<GameObject> ReturnAlliedMissiles()
    {
        return alliedMissiles;
    }
    public List<GameObject> ReturnEnemyMissiles()
    {
        return enemyMissiles;
    }
    public List<GameObject> ReturnEnemyShipTargeters()
    {
        return enemyShipTargeters;
    }
    public List<GameObject> ReturnAlliedShipTargeters()
    {
        return alliedShipTargeters;
    }
    public List<GameObject> ReturnNeutralTargeters()
    {
        return neutralTargeters;
    }
}
