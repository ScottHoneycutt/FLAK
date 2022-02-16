using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyerCannonTargeting : MonoBehaviour
{
    //Public variables----
    public float maxRange = 1100;
    public float minRange = 300;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float weaponPeriod = 2;
    public float shellVariance = 45;
    public float fireDelay = 4;
    public bool facingLeft = true;
    public GameObject barrels;
    public AudioSource firingSound;
    public ParticleSystem muzzleFlash;
    public UnitHP healthStat;

    //Private variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private GameObject target;
    private float detectionTimer = 0;
    private float fireTimer = 0;
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
                List<GameObject> possibleTargets = Identifiers.identifier.ReturnAlliedShipTargeters();
                //Targets within maxRange and outside minRange and within the 90 degree influence are carried on to targetsInRange from possibleTargets----
                for (int i = 0; i < possibleTargets.Count; i++)
                {
                    if (possibleTargets[i] != null)
                    {
                        if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= maxRange && Vector3.Distance(transform.position, possibleTargets[i].transform.position) >= minRange)
                        {
                            //Direction vector to determine influence----
                            influenceDetector = new Vector3(possibleTargets[i].transform.position.x, possibleTargets[i].transform.position.y, barrels.transform.position.z) - barrels.transform.position;
                            //Within influence (180 degree area covered by turret)?----
                            if ((facingLeft && influenceDetector.x <= 0) || (!facingLeft && influenceDetector.x >= 0))
                            {
                                targetsInRange.Add(possibleTargets[i]);
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
                    if (tempfloat > Vector3.Distance(targetsInRange[i].transform.position, transform.position))
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
                //Direction vector with y value that scales with the distance between the turret and the target----
                Vector3 targetPoint = new Vector3(target.transform.position.x, target.transform.position.y + (Mathf.Pow(target.transform.position.x - barrels.transform.position.x, 2) / 4500), barrels.transform.position.z) - barrels.transform.position;
                if (facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetPoint, new Vector3(0, 0, -1));
                    barrels.transform.rotation = Quaternion.Slerp(barrels.transform.rotation, targetRotation, stepSpeed);
                }
                if (!facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetPoint, new Vector3(0, 0, 1));
                    barrels.transform.rotation = Quaternion.Slerp(barrels.transform.rotation, targetRotation, stepSpeed);
                }

                //If the target is within maxRange and outside minrange----
                if ((target.transform.position - barrels.transform.position).magnitude <= maxRange && (target.transform.position - barrels.transform.position).magnitude >= minRange)
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
                        Vector3 shellPoint = target.transform.position;
                        //Random variance and increased height----
                        Vector3 adjustedVector = new Vector3(Random.Range(-shellVariance, shellVariance), 30, 0);
                        shellPoint = shellPoint + adjustedVector;
                        //Instantiating flak explosion----
                        Quaternion rotation = Quaternion.Euler(0, 0, 0);
                        firingSound.Play();
                        ObjectPoolManager.manager.SpawnFromPool("Enemy Shell Points", shellPoint, rotation);
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
