using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStormTurretTargeting : MonoBehaviour
{
    //Public variables----
    public float maxRange = 600;
    public float minRange = 50;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float weaponPeriod = 4;
    public float fireDelay = 2;
    public bool facingLeft = true;
    public float angleRange = 35;
    public int fireQuantity = 8;
    public GameObject turret;
    public GameObject target;
    public UnitHP healthStat;

    //Sound variables----
    public AudioSource soundSource;

    //Private variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private float detectionTimer = 0;
    private float fireTimer = 0;
    private RaycastHit hit;
    private LayerMask mask = (1 << 18) | (1 << 8);
    private Vector3 influenceDetector;
    private GameObject missile;

    private void OnEnable()
    {
        fireTimer = weaponPeriod;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Work if it is not EMPed----
        if (!healthStat.isEmped)
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
                            //Within influence and its respective angleRange?----
                            if ((facingLeft && influenceDetector.x <= 0 && Vector3.Angle(new Vector3(-1, 0, 0), influenceDetector) < angleRange) || (!facingLeft && influenceDetector.x >= 0 && Vector3.Angle(new Vector3(-1, 0, 0), influenceDetector) > 180 - angleRange))
                            {
                                targetsInRange.Add(possibleTargets[i]);
                            }
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
            //Converting turnSpeed into realtime turning speed (stepSpeed)----
            float stepSpeed = turnSpeed * Time.deltaTime;

            //If there is a target... ----
            if (target != null)
            {
                //Direction vector----
                Vector3 targetPoint = new Vector3(target.transform.position.x, target.transform.position.y, turret.transform.position.z) - turret.transform.position;
                //Rotation using direction vector----
                if (facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, 1));
                    turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, stepSpeed);
                }
                if (!facingLeft)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-targetPoint, new Vector3(0, 0, -1));
                    turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, stepSpeed);
                }

                //Getting "hit" to determine LoS stuff----
                Vector3 rayDirection = target.transform.position - turret.transform.position;
                bool hitRegister = Physics.Raycast(turret.transform.position, rayDirection, out hit, maxRange, mask);
                //If the ray hit something----
                if (!hitRegister)
                {
                    //If the target is within maxRange, outside minRange, and the first collider hit by the LoS ray is Ally----
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
                                missile = ObjectPoolManager.manager.SpawnFromPool("Enemy Storm Rockets", turretPosition, turretRotation);
                                missile.transform.Rotate(new Vector3(0, 180, -90));
                                //Setting the missile's target to the target identified by the turret at the time the missile is fired----
                                missile.GetComponent<EnemyStormMissile>().target = target;
                            }
                            //Play firing sound upon launch----
                            soundSource.Play();
                        }
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
}
