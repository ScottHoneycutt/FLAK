using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedMissileBayTargeting : MonoBehaviour
{
    //Public variables----
    public float maxRange = 600;
    public float minRange = 50;
    public float detectionRefreshDelay = 0.5f;
    public float weaponPeriod = 4;
    public float fireDelay = 2;
    public bool facingLeft = true;
    public int fireQuantity = 6;
    public GameObject turret;
    public GameObject target;

    //Sound variables----
    public AudioSource soundSource;

    //Private variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private float detectionTimer = 0;
    private float fireTimer = 0;
    private Vector3 influenceDetector;
    private GameObject missile;

    private void OnEnable()
    {
        fireTimer = weaponPeriod;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //DetectionTimer updates----
        detectionTimer += Time.deltaTime;

        //Refresh detectionTimer every x seconds----
        if (detectionTimer > detectionRefreshDelay)
        {
            detectionTimer = 0;
            //Cleaing up targets that are no longer in range from targetInRange----
            targetsInRange.Clear();
            //Retreiving possibleTargets from the identifier----
            List<GameObject> possibleTargets = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
            //Targets within maxRange and outside minRange and within the 90 degree influence are carried on to targetsInRange from possibleTargets----
            for (int i = 0; i < possibleTargets.Count; i++)
            {
                if (possibleTargets[i] != null)
                {
                    if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= maxRange && Vector3.Distance(transform.position, possibleTargets[i].transform.position) >= minRange)
                    {
                        targetsInRange.Add(possibleTargets[i]);
                    }
                }
            }
            //Clearing old target----
            target = null;
            //Closest target becomes target----
            float tempfloat = maxRange;
            for (int i = 0; i < targetsInRange.Count; i++)
            {
                if (tempfloat > Vector3.Distance(targetsInRange[i].transform.position, transform.position))
                {
                    tempfloat = Vector3.Distance(targetsInRange[i].transform.position, transform.position);
                    target = targetsInRange[i];
                }
            }
        }

        //If there is a target... ----
        if (target != null)
        {
            //If the target is within maxRange and outside minRange----
            if ((target.transform.position - turret.transform.position).magnitude <= maxRange && (target.transform.position - turret.transform.position).magnitude >= minRange)
            {
                //Incrementing fireTimer----
                fireTimer += Time.deltaTime;
                //Fire every weaponPeriod----
                if (fireTimer >= weaponPeriod)
                {
                    //Reset fireTimer upon firing----
                    fireTimer = 0;
                    //Instantiating missile----
                    Quaternion turretRotation = turret.transform.rotation;
                    Vector3 turretPosition = turret.transform.position;
                    turretPosition.z = 0;
                    //Firing burst of missiles----
                    for (int i = 0; i < fireQuantity; i++)
                    {
                        missile = ObjectPoolManager.manager.SpawnFromPool("Allied Missile Bay Rockets", turretPosition, turretRotation);
                        missile.transform.Rotate(new Vector3(0, 180, -90));
                        //Setting the missile's target to the target identified by the turret at the time the missile is fired----
                        missile.GetComponent<MissileBayRockets>().target = target;
                    }
                    //Play firing sound upon launch----
                    soundSource.Play();
                }
            }
        }
        //Delay between target aquisition and firing;
        if (target == null && fireTimer > weaponPeriod - fireDelay)
        {
            fireTimer = weaponPeriod - fireDelay;
        }

    }
}
