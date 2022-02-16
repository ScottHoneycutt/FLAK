using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlakTargeting : MonoBehaviour {
    //Public variables----
    public float maxRange = 300;
    public float minRange = 20;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float weaponPeriod = 2;
    public float flakVariance = 30;
    public float maxFlakVarience = 30;
    public float fireDelay = 1;
    public bool facingLeft = true;
    public Transform turret;
    public ParticleSystem muzzleFlash;
    public UnitHP healthStat;

    //Private variables----
    private List<Transform> targetsInRange = new List<Transform>();
    private Transform target;
    private float detectionTimer = 0;
    private float fireTimer = 0;
    private RaycastHit hit;
    private LayerMask mask = (1 << 18) | (1 << 8);
    Vector3 influenceDetector;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Work if it is not EMPed----
        if (!healthStat.isEmped)
        {
            //DetctionTimer updates----
            detectionTimer += Time.deltaTime;

            //Refresh detectionTimer every x seconds----
            if (detectionTimer > detectionRefreshDelay)
            {
                detectionTimer = 0;
                //Cleaing up targets that are no longer in range from targetInRange----
                targetsInRange.Clear();
                //Retreiving possibleTargets from the identifier----
                List<GameObject> possibleTargets = Identifiers.identifier.ReturnEnemyFlakMissileTargets();
                //Targets within maxRange and outside minRange and within the 90 degree influence are carried on to targetsInRange from possibleTargets----
                for (int i = 0; i < possibleTargets.Count; i++)
                {
                    if (possibleTargets[i] != null)
                    {
                        if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= maxRange && Vector3.Distance(transform.position, possibleTargets[i].transform.position) >= minRange)
                        {
                            //Direction vector to determine influence----
                            influenceDetector = new Vector3(possibleTargets[i].transform.position.x, possibleTargets[i].transform.position.y, turret.transform.position.z) - turret.transform.position;
                            //Within influence (180 degree area covered by turret)?----
                            if ((facingLeft && influenceDetector.x <= 0) || (!facingLeft && influenceDetector.x >= 0))
                            {
                                targetsInRange.Add(possibleTargets[i].transform);
                            }
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
                //Direction vector----
                Vector3 targetPoint = new Vector3(target.position.x, target.position.y, turret.position.z) - turret.position;
                if (facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, 1));
                    turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, stepSpeed);
                }
                if (!facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, -1));
                    turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, stepSpeed);
                }

                //Getting "hit" to determine LoS stuff----
                Vector3 rayDirection = target.position - turret.position;
                bool hitRegister = Physics.Raycast(turret.position, rayDirection, out hit, maxRange, mask);
                //If the ray hit nothing----
                if (!hitRegister)
                {
                    //If the target is within maxRange, outside minRange, and the first collider hit by the LoS ray is enemy----
                    if ((target.position - turret.position).magnitude <= maxRange && (target.position - turret.position).magnitude >= minRange)
                    {
                        //Incrementing fireTimer----
                        fireTimer += Time.deltaTime;
                        //Muzzle flash and flak explosion every weaponPeriod----
                        if (fireTimer >= weaponPeriod)
                        {
                            //Reset fireTimer upon firing----
                            fireTimer = 0;
                            //Muzzle flash----
                            muzzleFlash.Play();
                            //Target Coorinates----
                            Vector3 flakPoint = target.position;
                            //Random variance in flak explosions with range----
                            float rangeModifier = Vector3.Distance(influenceDetector, new Vector3(0, 0, 0));
                            rangeModifier = rangeModifier / 100 * flakVariance;
                            if (rangeModifier > maxFlakVarience)
                            {
                                rangeModifier = maxFlakVarience;
                            }
                            Vector3 randomVector = new Vector3(Random.Range(0 - rangeModifier, rangeModifier), Random.Range(0 - rangeModifier, rangeModifier), 0);
                            flakPoint = flakPoint + randomVector;
                            //Instantiating flak explosion----
                            Quaternion rotation = Quaternion.Euler(0, 0, 0);
                            ObjectPoolManager.manager.SpawnFromPool("Enemy Flak Explosions", flakPoint, rotation);
                        }
                    }
                }
            }
            //Delay between target aquisition and firing;
            if (target == null)
            {
                fireTimer = weaponPeriod - fireDelay;
            }
        }
    }
}

