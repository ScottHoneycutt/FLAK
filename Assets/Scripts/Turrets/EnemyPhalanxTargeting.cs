using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhalanxTargeting : MonoBehaviour
{
    //Public variables----
    public float sightRange = 100;
    public float range = 70;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float burstDuration = 1;
    public float burstDelay = .5f;
    public float barrelRotateRate = 40;
    public GameObject barrelCase;
    public GameObject turret;
    public ParticleSystem gunfire;
    public AudioSource SoundRegulator;
    public UnitHP healthStat;

    //Private variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private GameObject target;
    private float detectionTimer = 0;
    private bool firing = false;
    private float fireTimer = 0;
    private bool cooldown = false;
    private float cooldownTimer = 0;
    private RaycastHit hit;
    List<GameObject> possibleTargets = new List<GameObject>();
    List<GameObject> nonMissileTargets = new List<GameObject>();
    List<GameObject> missileTargets = new List<GameObject>();

    // FixedUpdate for physics
    void FixedUpdate()
    {
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
                nonMissileTargets = Identifiers.identifier.ReturnEnemyFlakMissileTargets();
                missileTargets = Identifiers.identifier.ReturnAlliedMissiles();
                //Clearing possibleTargets----
                possibleTargets = new List<GameObject>();
                //Combine both lists----
                foreach (GameObject i in nonMissileTargets)
                {
                    possibleTargets.Add(i);
                }
                foreach (GameObject i in missileTargets)
                {
                    possibleTargets.Add(i);
                }
                //Targets within sightRange are carried on to targetsInRange from possibleTargets----
                for (int i = 0; i < possibleTargets.Count; i++)
                {
                    if (possibleTargets[i] != null)
                    {
                        if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= sightRange)
                        {
                            targetsInRange.Add(possibleTargets[i]);
                        }
                    }
                }
                //Clearing old target----
                target = null;
                //Closest target becomes target----
                float tempfloat = 1000000;
                for (int i = 0; i < targetsInRange.Count; i++)
                {
                    if (tempfloat > Vector3.Distance(targetsInRange[i].transform.position, transform.position) && targetsInRange[i].activeSelf)
                    {
                        tempfloat = Vector3.Distance(targetsInRange[i].transform.position, transform.position);
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
                Vector3 targetPoint = new Vector3(target.transform.position.x, target.transform.position.y, turret.transform.position.z) - turret.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, 1));
                turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, stepSpeed);

                //Barrel Rotations----
                barrelCase.transform.Rotate(0, barrelRotateRate * Time.deltaTime, 0);

                //Getting "hit" to determine LoS stuff----
                Vector3 rayDirection = target.transform.position - turret.transform.position;
                bool hitRegister = Physics.Raycast(turret.transform.position, rayDirection, out hit, range);
                //If the ray hit something----
                if (hitRegister)
                {
                    //Testing for LoS with an enemy----
                    if (hit.transform.gameObject.layer == 11 || hit.transform.gameObject.layer == 13 || hit.transform.gameObject.layer == 14 || hit.transform.gameObject.layer == 15 || hit.transform.gameObject.layer == 20)
                    {
                        if (firing == false && !cooldown)
                        {
                            //Start firing----
                            firing = true;
                            gunfire.Play();
                            //Stop sound----
                            SoundRegulator.loop = true;
                            SoundRegulator.Play();
                        }
                    }
                }

                //Burst timer---
                if (firing)
                {
                    fireTimer += Time.deltaTime;
                    if (fireTimer >= burstDuration)
                    {
                        cooldown = true;
                        fireTimer = 0;
                        //Stop firing----
                        firing = false;
                        gunfire.Stop();
                        //Stop sound----
                        SoundRegulator.loop = false;
                    }
                }
                //Cooldown timer between bursts----
                else
                {
                    cooldownTimer += Time.deltaTime;
                    if (cooldownTimer >= burstDelay)
                    {
                        cooldownTimer = 0;
                        cooldown = false;
                    }
                }

                //if the ray hit nothing----
                if (!hitRegister && firing)
                {
                    //Stop firing----
                    firing = false;
                    gunfire.Stop();
                    //Stop sound----
                    SoundRegulator.loop = false;
                }
                //Otherwise if the target is outside of range or the first collider hit by the ray was not an enemy and the gun is firing----
                else if (hitRegister)
                {
                    if (!(hit.transform.gameObject.layer == 11 || hit.transform.gameObject.layer == 13 || hit.transform.gameObject.layer == 14 || hit.transform.gameObject.layer == 15 || hit.transform.gameObject.layer == 20) && firing)
                    {
                        //Stop firing----
                        firing = false;
                        gunfire.Stop();
                        //Stop sound----
                        SoundRegulator.loop = false;
                    }
                }
            }
            //Stop firing when a target is lost----
            if (!target && firing)
            {
                firing = false;
                gunfire.Stop();
                //Stop sound----
                SoundRegulator.loop = false;
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
        if (Time.timeScale != 1)
        {
            SoundRegulator.loop = false;
            firing = false;
            gunfire.Stop();
        }
    }
}
