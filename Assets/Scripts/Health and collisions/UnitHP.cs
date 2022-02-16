using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHP : MonoBehaviour {

    //Public variables----
    public float maximumHealth = 100;
    public float health;
    public float deathDelay = 1;
    public bool isNeutral = false;
    public GameObject unit;
    public bool deathRegister = false;

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

    //Ship and bunker variables----
    public UnitHP turret1;
    public UnitHP turret2;
    public UnitHP turret3;
    public UnitHP turret4;
    public UnitHP turret5;
    public UnitHP frontCannon;
    public UnitHP midCannon;
    public UnitHP rearCannon;

    //Destroyed bunker----
    public GameObject destroyedBunker;
    //Destroyed practice target----
    public GameObject destroyedTarget;

    //EMP munitions variables----
    public bool empStart = false;
    public bool isEmped = false;
    public float empDuration = 2;
    private float empTimer = 0;

    //Camera shake variables----
    public float lightDamageDuration = .3f;
    public float mediumDamageDuration = .5f;
    public float heavyDamageDuration = .7f;
    public float recentDamageDuration = 2;

    private int counter = 0;
    private float lightTimer = 0;
    private float mediumTimer = 0;
    private float heavyTimer = 0;
    private float recentDamageTimer = 0;

    //Private variables----
    private bool deathComplete = false;
    private float HP1;
    private float HP2;

    //GameControl----
    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    //Setting starting health to full----
    void Start () {
        //Applying upgrades to player fighter----
        if(unit.name == "Player Fighter")
        {
            //Incremental----
            maximumHealth = maximumHealth* Mathf.Pow(1.1f, controller.armorLevel.VerifyInt(controller.pArmorLevel));

            //Tech upgrades----
            if (controller.ballisticArmorOwned.VerifyBool(controller.pBallisticArmorOwned))
            {
                maximumHealth = maximumHealth * 1.15f;
            }
            if (controller.titaniumSkeletonOwned.VerifyBool(controller.pTitaniumSkeletonOwned))
            {
                maximumHealth = maximumHealth * 1.1f;
            }
            if (controller.heatTreatmentOwned.VerifyBool(controller.pHeatTreatmentOwned))
            {
                maximumHealth = maximumHealth * 1.15f;
            }
            if (controller.collisionAnalysisOwned.VerifyBool(controller.pCollisionAnalysisOwned))
            {
                maximumHealth = maximumHealth * 1.04f;
            }
            if (controller.ballisticArmorOwned.VerifyBool(controller.pBallisticArmorOwned))
            {
                maximumHealth = maximumHealth * 1.15f;
            }
            if (controller.nanoRepairsOwned.VerifyBool(controller.pNanoRepairsOwned))
            {
                StartCoroutine("NanoRepairs");
            }
        }
        //Upgrades to player missile----
        else if(unit.name == "Player Missile(Clone)" || unit.name == "Player Rocket(Clone)")
        {
            if (controller.titaniumSkeletonOwned.VerifyBool(controller.pTitaniumSkeletonOwned))
            {
                maximumHealth = maximumHealth * 3;
            }
        }

        //Setting starting health to full----
        //Getting everything else ready----
        health = maximumHealth;

        HP1 = maximumHealth;
        HP2 = maximumHealth;
    }

    //OnEnable for resetting and calling identifiers----
    private void OnEnable()
    {
        health = maximumHealth;
        deathComplete = false;
        deathRegister = false;
        empTimer = 0;
        isEmped = false;
    }

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
    void Update () {

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

        //No health = registered as dead----
        if (health <= 0 && !deathRegister)
        {
            deathRegister = true;
        }

        //Dead = explosion and unit removal/death animation----
        if (deathRegister)
        {
            //Unit position and angle----
            Vector3 unitPosition = unit.transform.position;
            Quaternion unitAngle = unit.transform.rotation;

            //Aircraft death----
            if (unit.CompareTag("Aircraft") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Fighter Explosions", unitPosition, unitAngle);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Turret death----
            else if (unit.CompareTag("Grounded") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Turret Explosions", unitPosition, unitAngle);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Normal Missile death----
            else if(unit.CompareTag("Missile") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Missile Explosions", unitPosition, unitAngle);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Small Missile death---
            else if(unit.CompareTag("Rocket") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Rocket Explosions", unitPosition, unitAngle);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Bomb death----
            else if (unit.CompareTag("Bomb") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Bomb Explosions", unitPosition, Quaternion.Euler(0, 0, 0));
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Practice Target death----
            else if (unit.CompareTag("PracticeTarget") && deathComplete == false)
            {
                destroyedTarget.SetActive(true);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Destroyer death----
            else if (unit.CompareTag("MediumShip") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Destroyed Destroyers", unitPosition, unitAngle);
                //Destroying individual turrets and cannons if they still exist----
                turret1.health = 0;
                turret2.health = 0;
                turret3.health = 0;
                turret4.health = 0;
                turret5.health = 0;
                frontCannon.health = 0;
                midCannon.health = 0;
                rearCannon.health = 0;

                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Frigate death----
            else if (unit.CompareTag("Frigate") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Destroyed Frigates", unitPosition, unitAngle);
                //Destroying individual turrets and cannons if they still exist----
                turret1.health = 0;
                turret2.health = 0;
                turret3.health = 0;
                frontCannon.health = 0;

                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Carrier death----
            else if (unit.CompareTag("Carrier") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Destroyed Carriers", unitPosition, unitAngle);
                //Destroying individual turrets and cannons if they still exist----
                turret1.health = 0;
                turret2.health = 0;
                turret3.health = 0;
                turret4.health = 0;

                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Patrol boat death----
            else if(unit.CompareTag("SmallShip") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Destroyed Patrol Boats", unitPosition, unitAngle);
                //Destroying individual turrets and cannons if they still exist----
                turret1.health = 0;
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Bunker death----
            else if(unit.CompareTag("Bunker") && deathComplete == false)
            {
                destroyedBunker.SetActive(true);
                //Destroying turret if it's still alive----
                turret1.health = 0;
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
            //Bomber death----
            else if(unit.CompareTag("Bomber") && deathComplete == false)
            {
                ObjectPoolManager.manager.SpawnFromPool("Bomber Explosions", unitPosition, unitAngle);
                Invoke("Deactivate", deathDelay);
                deathComplete = true;
            }
        }

        //Start of EMP effect (EMP's specific effects are regulated by the relevant scripts themselves, UnitHP just relays the variable status to them)----
        if (empStart)
        {
            empStart = false;
            //Reset EMP timer and turning on isEmped even if already EMPed----
            empTimer = 0;
            isEmped = true;
        }
        //Timer----
        if (isEmped)
        {
            empTimer += Time.deltaTime;
            //EMP effect wears off----
            if (empTimer >= empDuration)
            {
                isEmped = false;
                empTimer = 0;
            }
        }

        //If unit is a player (Camera shake)----
        if (unit.name == "Player Fighter")
        {
            //Alternating frames to detect a differece in health levels----
            counter++;
            if (counter % 2 == 1)
            {
                HP1 = health;

                //Determining light damage----
                if (HP1 < HP2)
                {
                    //Reset lightTimer upon taking damage----
                    lightTimer = 0;
                    controller.lightDamageBool = true;
                }
            }
            if (counter % 2 == 0)
            {
                counter = 0;
                HP2 = health;

                //Determining light damage----
                if (HP1 > HP2)
                {
                    //Reset lightTimer upon taking damage----
                    lightTimer = 0;
                    controller.lightDamageBool = true;
                }
            }

            //Starting lightTimer----
            if (controller.lightDamageBool == true)
            {
                lightTimer += Time.deltaTime;
                //Stopping camera shake if timer has exceeded its shake duration----
                if (lightTimer >= lightDamageDuration)
                {
                    controller.lightDamageBool = false;
                }
            }

            //Determining medium damage----
            if (Mathf.Abs(HP1 - HP2) >= 15)
            {
                //Reset mediumTimer upon taking medium damage----
                mediumTimer = 0;
                controller.mediumDamageBool = true;
            }
            //Starting mediumTimer----
            if (controller.mediumDamageBool == true)
            {
                mediumTimer += Time.deltaTime;
                //Stopping camera shake if timer has exceeded its shake duration----
                if (mediumTimer >= mediumDamageDuration)
                {
                    controller.mediumDamageBool = false;
                }
            }

            //Determining heavy damage----
            if (Mathf.Abs(HP1 - HP2) >= 25)
            {
                //Reset heavyTimer upon taking heavy damage----
                heavyTimer = 0;
                controller.heavyDamageBool = true;
            }
            //Starting heavyTimer----
            if (controller.heavyDamageBool == true)
            {
                heavyTimer += Time.deltaTime;
                //Stopping camera shake if timer has exceeded its shake duration----
                if (heavyTimer >= heavyDamageDuration)
                {
                    controller.heavyDamageBool = false;
                }
            }

            //UPGRADES----
            //Kinetic Recycling upgrade active detection----
            if (Mathf.Abs(HP1 - HP2) > 0)
            {
                //Reset recentDamageTimer----
                recentDamageTimer = 0;
                controller.recentlyDamaged = true;
            }
            if (controller.recentlyDamaged)
            {
                recentDamageTimer += Time.deltaTime;
                //Stopping Kinetic Recylcing upgrade from taking affect after 2 seconds----
                if (recentDamageTimer >= recentDamageDuration)
                {
                    controller.recentlyDamaged = false;
                }
            }

            //If the player fighter is at or below 25% HP----
            if (health <= maximumHealth * .25f)
            {
                controller.criticalHealth = true;
            }
            else
            {
                controller.criticalHealth = false;
            }
        }
	}

    IEnumerator NanoRepairs()
    {
        while (this.gameObject.activeSelf)
        {
            if(health < maximumHealth)
            {
                health++;
            }
            yield return new WaitForSeconds(1);
        }
    }



    void Deactivate()
    {
        unit.SetActive(false);
    }
}
