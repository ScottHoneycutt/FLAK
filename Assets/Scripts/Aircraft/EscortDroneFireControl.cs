using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortDroneFireControl : MonoBehaviour
{
    //Public variables----
    public float sMissileReloadTime = 4;
    public float maxCannonBurstDuration = 2;
    public float minCannonBurstDuration = 0.5f;
    public float delayBetweenCannonBursts = 0.4f;

    public Transform escortDrone;
    public ParticleSystem cannonFiring;
    public EscortDroneMovement escort;

    //Secret and protected values (anti-tampering measures)----
    private SecretFloat missileReloadTime;

    //Sound Variables----
    public AudioSource soundSource;

    //Private Variables----
    private float missileReloadTimer = 0;
    private float cannonDelayTimer = 0;
    private bool firing = false;
    private bool cannonFiringBool = false;
    private float fireTimer = 0;
    private float burstDuration;

    private ParticleSystem.EmissionModule cannonEmission;

    private bool engaged = false;
    private bool launching = false;

    //Dump variables----
    private float dumpReloadTime;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        //Anti-tampering measures (SecretFloats)----
        missileReloadTime = new SecretFloat(sMissileReloadTime);
    }

    private void Start()
    {
        //Tech upgrades----
        if (controller.markIIIAutoloadersOwned.VerifyBool(controller.pMarkIIIAutoloadersOwned))
        {
            missileReloadTime.Reset(missileReloadTime.GetFloat() * .83333f);
        }

        //Dumping variables----
        dumpReloadTime = missileReloadTime.GetFloat();
        //Desperation Module (Tech upgrades)----
        if (controller.desperationModuleOwned.VerifyBool(controller.pDesperationModuleOwned))
        {
            StartCoroutine("RefreshDesperationModule");
        }
        if (controller.markIVFeederOwned.VerifyBool(controller.pMarkIVFeederOwned))
        {
            //Fire rate----
            cannonEmission = cannonFiring.emission;
            cannonEmission.rateOverTime = cannonEmission.rateOverTime.constant * 1.2f;
            //Audio play rate----
            soundSource.pitch = soundSource.pitch * 1.2f;
        }
    }

    //Restting 
    private void OnEnable()
    {
        
    }

    IEnumerable RefreshDesperationModule()
    {
        while (true)
        {
            if (controller.criticalHealth)
            {
                missileReloadTime.Reset(dumpReloadTime * 8.3333f);
            }
            else
            {
                missileReloadTime.Reset(dumpReloadTime);
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    //Telling the rest of the script to start firing weapons when a target is within the fire control hitbox----
    private void OnTriggerEnter(Collider objectCollider)
    {
        firing = true;
    }

    // Update is called once per frame
    void Update()
    {
        engaged = escort.engaged;

        //Seperating autonomous and player-incited actions----
        //Player-incited engage----
        if (!engaged)
        {
            //Cannons----
            if (controller.cannonShootingBool && !firing)
            {
                firing = true;
                //Start firing----
                cannonFiring.Play();
                //Start sound----
                soundSource.loop = true;
                soundSource.Play();
            }
            else if (!controller.cannonShootingBool)
            {
                firing = false;
                cannonFiring.Stop();
                //Stop firing----
                firing = false;
                //Stop sound----
                soundSource.loop = false;
            }

            //Missiles----
            if (controller.missileShootBool && !launching)
            {
                launching = true;
                //Launch missile----
                ObjectPoolManager.manager.SpawnFromPool("Player Rockets", escortDrone.position, escortDrone.rotation);
                ObjectPoolManager.manager.SpawnFromPool("Player Rockets", transform.position, escortDrone.rotation);
                //Launch sound----
                soundSource.Play();
            }
            else if (!controller.missileShootBool && launching)
            {
                launching = false;
            }
        }

        //If self-engaged----
        else
        {
            //Incrementing timers----
            missileReloadTimer += Time.deltaTime;

            if (!cannonFiringBool)
            {
                cannonDelayTimer += Time.deltaTime;
            }
            if (cannonFiringBool)
            {
                fireTimer += Time.deltaTime;
            }

            //If a target is detected by the fire control hitbox----
            if (firing)
            {
                //Missile firing----
                if (missileReloadTimer >= missileReloadTime.GetFloat())
                {
                    missileReloadTimer = 0;
                    ObjectPoolManager.manager.SpawnFromPool("Player Rockets", transform.position, escortDrone.rotation);
                    ObjectPoolManager.manager.SpawnFromPool("Player Rockets", transform.position, escortDrone.rotation);
                    //Missile firing sound----
                    soundSource.Play();
                }
                //Start cannon burst----
                if (!cannonFiringBool && cannonDelayTimer >= delayBetweenCannonBursts && Time.timeScale == 1)
                {
                    cannonDelayTimer = 0;
                    cannonFiringBool = true;
                    cannonFiring.Play();

                    burstDuration = Random.Range(minCannonBurstDuration, maxCannonBurstDuration);

                    //Cannonfire sound----
                    soundSource.loop = true;
                    soundSource.Play();
                }
            }
            //Overriding cannon bursts to stop when timescale is changed----
            if (Time.timeScale != 1)
            {
                fireTimer = burstDuration;
            }

            //Stop cannon burst when burst ends----
            if (fireTimer >= burstDuration && cannonFiringBool)
            {
                fireTimer = 0;
                cannonFiringBool = false;
                cannonFiring.Stop();

                //Stop cannonfire sound----
                soundSource.loop = false;
            }
        }

        //Resetting firing to false unless the player is firing----
        if (!controller.cannonShootingBool)
        {
            firing = false;
        }
    }
}
