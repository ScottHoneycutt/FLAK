using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedSAMTargeting : MonoBehaviour {

    //Public variables----
    public float maxRange = 600;
    public float minRange = 50;
    public float detectionRefreshDelay = 0.5f;
    public float turnSpeed = 1;
    public float weaponPeriod = 4;
    public float fireDelay = 2;
    public bool facingLeft = true;
    public bool level1Turret = true;
    public float angleRange = 35;
    public Transform turret;
    public Object missilePrefab;
    public Transform target;

    //Sound variables----
    public AudioSource soundSource;

    //Private variables----
    private List<Transform> targetsInRange = new List<Transform>();
    private float detectionTimer = 0;
    private float fireTimer = 0;
    private RaycastHit hit;
    private LayerMask mask = (1 << 18) | (1 << 11);
    private Vector3 influenceDetector;
    private GameObject missile;

    private void OnEnable()
    {
        fireTimer = weaponPeriod;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            List<GameObject> possibleTargets = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
            //Targets within maxRange and outside minRange and within the 90 degree influence are carried on to targetsInRange from possibleTargets----
            for (int i = 0; i < possibleTargets.Count; i++)
            {
                if (possibleTargets[i] != null)
                {
                    if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= maxRange && Vector3.Distance(transform.position, possibleTargets[i].transform.position) >= minRange)
                    {
                        //Direction vector to determine influence----
                        influenceDetector = new Vector3(possibleTargets[i].transform.position.x, possibleTargets[i].transform.position.y, turret.position.z) - turret.position;
                        //Within influence and its respective angleRange?----
                        if ((facingLeft && influenceDetector.x <= 0 && Vector3.Angle(new Vector3(-1, 0, 0), influenceDetector) < angleRange) || (!facingLeft && influenceDetector.x >= 0 && Vector3.Angle(new Vector3(-1, 0, 0), influenceDetector) > 180 - angleRange))
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
            //Rotation using direction vector----
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
                //If the target is within maxRange and outside minRange,----
                if ((target.position - turret.position).magnitude <= maxRange && (target.position - turret.position).magnitude >= minRange)
                {
                    //Incrementing fireTimer----
                    fireTimer += Time.deltaTime;
                    //Fire every weaponPeriod----
                    if (fireTimer >= weaponPeriod)
                    {
                        //Reset fireTimer upon firing----
                        fireTimer = 0;
                        //Instantiating missile----
                        Quaternion turretRotation = turret.rotation;
                        Vector3 turretPosition = turret.position;
                        turretPosition.z = 0;
                        if (level1Turret)
                        {
                            missile = ObjectPoolManager.manager.SpawnFromPool("Allied SAMs", turretPosition, turretRotation);
                        }
                        else
                        {
                            missile = ObjectPoolManager.manager.SpawnFromPool("Allied SAMs Level 2", turretPosition, turretRotation);
                        }
                        missile.transform.Rotate(new Vector3(180, 0, 0));
                        //Setting the missile's target to the target identified by the turret at the time the missile is fired----
                        missile.GetComponent<SAMTracking>().target = target.gameObject;
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
