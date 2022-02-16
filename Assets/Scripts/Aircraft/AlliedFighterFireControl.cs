using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedFighterFireControl : MonoBehaviour {
    //Public variables----
    public float sMissileReloadTime = 4;
    public float maxCannonBurstDuration = 2;
    public float minCannonBurstDuration = 0.5f;
    public float delayBetweenCannonBursts = 0.4f;
    public bool level2 = false;

    public Transform fighter;
    public ParticleSystem cannonFiring;

    //Secret and protected values (anti-tampering measures)----
    private SecretFloat missileReloadTime;

    //Sound variables----
    public AudioSource soundSource;
    public AudioClip missileFireSound;
    public float missileFireVolume = 0.5f;

    //private variables----
    private float missileReloadTimer = 0;
    private float cannonDelayTimer = 0;
    private bool firing = false;
    private bool cannonFiringBool = false;
    private float fireTimer = 0;
    private float burstDuration;

    
    private void Awake()
    {
        //Anti-tampering measures (SecretFloats)----
        missileReloadTime = new SecretFloat(sMissileReloadTime);
    }

    //Telling the rest of the script to start firing weapons when a target is within the fire control hitbox----
    private void OnTriggerEnter(Collider objectCollider)
    {
        firing = true;
    }

    // Update is called once per frame
    void Update()
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
                Quaternion rotation = fighter.rotation;
                if (!level2)
                {
                    ObjectPoolManager.manager.SpawnFromPool("Allied Air Missiles", fighter.position, rotation);
                }
                else
                {
                    ObjectPoolManager.manager.SpawnFromPool("Allied Air Missiles Level 2", fighter.position, rotation);
                }
                //Missile firing sound----
                soundSource.PlayOneShot(missileFireSound, missileFireVolume);
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


        //Resetting firing to false by default to prevent infinite firing----
        firing = false;
    }
}
