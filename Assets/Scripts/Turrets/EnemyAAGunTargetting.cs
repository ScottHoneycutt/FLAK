using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAAGunTargetting : MonoBehaviour {

    //Public variables----
    public float sightRange = 100;
    public float range = 70;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float burstDuration = 3.5f;
    public float burstCooldown = 1.5f;
    public Transform turret;
    public ParticleSystem gunfire;
    public AudioSource SoundRegulator;
    public UnitHP healthStat;

    //Private variables----
    private List<Transform> targetsInRange = new List<Transform>();
    private Transform target;
    private float detectionTimer = 0;
    private bool firing = false;
    private RaycastHit hit;
    private LayerMask mask = (1 << 18) | (1 << 8);

    private float burstTimer = 0;
    private float cooldownTimer = 0;

    // FixedUpdate for physics
    void FixedUpdate () {
        //Work if it is not EMPed----
        if (!healthStat.isEmped)
        {
            //Timer updates----
            detectionTimer += Time.deltaTime;

            //Refresh detectionTimer every x seconds----
            if (detectionTimer > detectionRefreshDelay)
            {
                detectionTimer = 0;
                //Cleaing up targets that are no longer in range from targetInRange----
                targetsInRange.Clear();
                //Retreiving possibleTargets from the identifier----
                List<GameObject> possibleTargets = Identifiers.identifier.ReturnEnemyAATargets();
                //Targets within sightRange are carried on to targetsInRange from possibleTargets----
                for (int i = 0; i < possibleTargets.Count; i++)
                {
                    if (possibleTargets[i] != null)
                    {
                        if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= sightRange)
                        {
                            targetsInRange.Add(possibleTargets[i].transform);
                        }
                    }
                }
                //Clearing old target----
                target = null;
                //Closest target becomes target----
                float tempfloat = 100000;
                for (int i = 0; i < targetsInRange.Count; i++)
                {
                    if (tempfloat > Vector3.Distance(targetsInRange[i].position, transform.position))
                    {
                        tempfloat = Vector3.Distance(targetsInRange[i].position, transform.position);
                        target = targetsInRange[i];
                    }
                }
            }
            //Converting turnSpeed into realtime turning speed (stepSpeed)----
            float stepSpeed = turnSpeed * Time.deltaTime;

            //If there is a target... ----
            if (target != null)
            {
                //I have no idea how this works, but it handles the rotation of the turret----
                Vector3 targetPoint = new Vector3(target.position.x, target.position.y, turret.position.z) - turret.position;
                Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, 1));
                turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, stepSpeed);

                //Getting "hit" to determine LoS stuff----
                Vector3 rayDirection = target.position - turret.position;
                bool hitRegister = Physics.Raycast(turret.position, rayDirection, out hit, range, mask);

                //If the ray did not hit non-targets, and gun is not firing, and gun's burst duration is not exceeded, and target is within range----
                if (!hitRegister && !firing && burstTimer < burstDuration && Vector3.Distance(target.position,turret.position) <= range)
                {
                    //Start firing----
                    firing = true;
                    gunfire.Play();
                    //Start Sound----
                    SoundRegulator.loop = true;
                    SoundRegulator.Play();
                }
                //If the ray hit non-targets or if its burst duration is over and gun is firing----
                if ((hitRegister || burstTimer >= burstDuration) && firing)
                {
                    //Stop firing----
                    firing = false;
                    gunfire.Stop();
                    //Stop sound----
                    SoundRegulator.loop = false;
                }
                //If target leaves range while firing----
                if(Vector3.Distance(target.position, turret.position) > range && firing)
                {
                    //Stop firing----
                    firing = false;
                    gunfire.Stop();
                    //Stop sound----
                    SoundRegulator.loop = false;
                }
            }
            //Stop firing when a target is lost----
            if (!target && firing)
            {
                //Stop firing----
                firing = false;
                gunfire.Stop();
                //Stop sound----
                SoundRegulator.loop = false;
            }

            //Gun burst and cooldown----
            if (firing)
            {
                burstTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer += Time.deltaTime;
            }
            //Reset both timers when cooldown ends----
            if (cooldownTimer >= burstCooldown)
            {
                cooldownTimer = 0;
                burstTimer = 0;
            }
        }
        //Stop firing when EMPed----
        else
        {
            if (firing)
            {
                firing = false;
                gunfire.Stop();
                //Stop sound----
                SoundRegulator.loop = false;
            }
        }
    }

    //Using Update to end firing sounds upon change of timeScale----
    void Update()
    {
        if (Time.timeScale != 1 && firing)
        {
            SoundRegulator.loop = false;
            firing = false;
            gunfire.Stop();
        }
    }
}
