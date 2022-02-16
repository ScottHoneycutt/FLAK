using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirMissileTracking : MonoBehaviour {

    //Public variables----
    public float sFlightSpeed = 200;
    public float sTurnSpeed = 100;
    public float randomization = 10;
    public float randomizationShuffleDelay = 1.5f;
    public float sLifetime = 3;
    public float targetRefreshDelay = 0.5f;
    public float detectionRange = 200;
    public float angleRange = 20;
    public float jammingProbability = 3;
    public Transform missileLead;
    public Transform missileRear;
    public UnitHP hp;

    //Anti-tampering measures----
    private SecretFloat flightSpeed;
    private SecretFloat turnSpeed;
    private SecretFloat lifetime;

    //Randomizer for missile jammer----
    private int jamRandom;

    //Target variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private GameObject target;
    private float targetRefreshTimer = 0;
    private Vector3 influenceDetector;
    private Vector3 missileDirection;

    //Execution count, lifetime counter, and randomization timer----
    private int frameCounter = 0;
    private float lifetimeCounter = 0;
    private float randomizationTimer;

    //Random variable;
    private float random = 0;

    //Missile angle variables----
    private float positionY1;
    private float positionY2;
    private float changeInY;

    private float flightAngle;

    private float positionX1;
    private float positionX2;
    private float changeInX;

    //Anti-tampering measures----
    private void Awake()
    {
        flightSpeed = new SecretFloat(sFlightSpeed);
        turnSpeed = new SecretFloat(sTurnSpeed);
        lifetime = new SecretFloat(sLifetime);
    }

    private void Start()
    {
        //Missile Jammer Utility----
        if (GameControl.control.missileJammerEquipped.VerifyBool(GameControl.control.pMissileJammerEquipped))
        {
            //Random chance of jamming----
            jamRandom = Random.Range(0, 100);
            StartCoroutine("MissileJammer");
        }
    }

    //Jamming----
    IEnumerator MissileJammer()
    {
        while (true)
        {
            //Only works if missile is targetting player----
            if (target)
            {
                //If within jamming range of player as well (150)----
                if (target.transform.parent.name == "Player Fighter" && Vector3.Distance(target.transform.position, this.transform.position) < 150)
                {
                    if (jamRandom < jammingProbability)
                    {
                        //jammed = missile destroyed----
                        hp.health = 0;
                    }
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        target = null;
        //Instantly prompting a random variable for flight variation----
        randomizationTimer = randomizationShuffleDelay;
        //Instantly prompting a target identification----
        targetRefreshTimer = targetRefreshDelay;
        //Resetting lifetime----
        lifetimeCounter = 0;
    }

    // FixedUpdate is called when physics engine runs----
    void FixedUpdate()
    {
        //Incrementing lifetimeCounter and targetRefreshTimer----
        lifetimeCounter += Time.deltaTime;
        targetRefreshTimer += Time.deltaTime;

        //Detonating missile if it is past its lifetime----
        if (lifetimeCounter >= lifetime.GetFloat())
        {
            hp.health = 0;
            lifetimeCounter = 0;
        }

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeed.GetFloat()) * Time.deltaTime);

        //Finding a target/new target----
        if (!target || !target.activeInHierarchy)
        {
            //Only allow a refresh every targetRefreshDelay seconds----
            if (targetRefreshTimer >= targetRefreshDelay)
            {
                //Reset targetRefreshTimer----
                targetRefreshTimer = 0;

                //Cleaing up targets that are no longer in range from targetInRange----
                targetsInRange.Clear();
                //Retreiving possibleTargets from the identifier----
                List<GameObject> possibleTargets = Identifiers.identifier.ReturnEnemyFlakMissileTargets();
                //Targets within detectionRange are carried on to targetsInRange from possibleTargets----
                for (int i = 0; i < possibleTargets.Count; i++)
                {
                    if (possibleTargets[i] != null)
                    {
                        if (Vector3.Distance(transform.position, possibleTargets[i].transform.position) <= detectionRange)
                        {
                            //Direction vector from hypothetical target to the missile to determine influence----
                            influenceDetector = new Vector3(possibleTargets[i].transform.position.x, possibleTargets[i].transform.position.y, transform.position.z) - this.transform.position;
                            //Finding direction vector for the missile----
                            missileDirection = missileLead.position - missileRear.position;
                            //Within angleRange?----
                            if ((Vector3.Angle(missileDirection, influenceDetector) < angleRange))
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
        }

        //If there is a target/Once a target is identified (important to prevent errors if the target is eliminated)----
        if (target)
        {
            //Finding Y differences----
            frameCounter++;

            if (frameCounter % 2 == 1)
            {
                positionY1 = transform.position.y;

                changeInY = positionY1 - positionY2;
                changeInY = changeInY / Time.deltaTime;
            }
            if (frameCounter % 2 == 0)
            {
                positionY2 = transform.position.y;

                changeInY = positionY2 - positionY1;
                changeInY = changeInY / Time.deltaTime;
            }

            //Finding X differences----
            if (frameCounter % 2 == 1)
            {
                positionX1 = transform.position.x;

                changeInX = positionX1 - positionX2;
                changeInX = changeInX / Time.deltaTime;
            }
            if (frameCounter % 2 == 0)
            {
                positionX2 = transform.position.x;

                changeInX = positionX2 - positionX1;
                changeInX = changeInX / Time.deltaTime;
            }

            //Finding basic missile angle----
            flightAngle = Vector3.Angle(new Vector3(changeInX, changeInY, 0), new Vector3(1, 0, 0));

            //Finding actual missile angle----
            if (changeInY < 0)
            {
                flightAngle = 360 - flightAngle;
            }

            //Finding the direction vector from the missile to the target (target is assigned by SAMTurretTargetting)----
            Vector3 directionVector = target.transform.position - transform.position;

            //Finding the angle (0-180) between directionVector and the positive X axis----
            float basicAngle = Vector3.Angle(directionVector, new Vector3(1, 0, 0));

            //Updating/resetting randomizationTimer, refreshing random----
            randomizationTimer += Time.deltaTime;
            if (randomizationTimer > randomizationShuffleDelay)
            {
                randomizationTimer = 0;
                random = Random.Range(0 - randomization, randomization);
            }

            //Converting that angle into numbers from 0-360 and including randomization;----
            float fullAngle = basicAngle + random;

            if (directionVector.y < 0)
            {
                fullAngle = 360 - basicAngle + random;
            }

            //Rough missile angle change----
            float rotation = Mathf.LerpAngle(flightAngle, fullAngle, 1);

            if ((rotation > flightAngle + 3 || rotation < flightAngle - 3))
            {
                if (rotation > flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, turnSpeed.GetFloat() * Time.deltaTime), Space.World);
                }

                if (rotation < flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, -turnSpeed.GetFloat() * Time.deltaTime), Space.World);
                }
            }

            //Slowdown when approaching target angle----
            else if ((rotation > flightAngle + 2 || rotation < flightAngle - 2))
            {
                if (rotation > flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, turnSpeed.GetFloat() * Time.deltaTime / 3), Space.World);
                }

                if (rotation < flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, -turnSpeed.GetFloat() * Time.deltaTime / 3), Space.World);
                }
            }

            else if ((rotation > flightAngle + 1 || rotation < flightAngle - 1))
            {
                if (rotation > flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, turnSpeed.GetFloat() * Time.deltaTime / 5), Space.World);
                }

                if (rotation < flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, -turnSpeed.GetFloat() * Time.deltaTime / 5), Space.World);
                }
            }

            else if ((rotation > flightAngle + .5 || rotation < flightAngle - .5))
            {
                if (rotation > flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, turnSpeed.GetFloat() * Time.deltaTime / 10), Space.World);
                }

                if (rotation < flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, -turnSpeed.GetFloat() * Time.deltaTime / 10), Space.World);
                }
            }

            else if ((rotation > flightAngle + .1 || rotation < flightAngle - .1))
            {
                if (rotation > flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, turnSpeed.GetFloat() * Time.deltaTime / 15), Space.World);
                }

                if (rotation < flightAngle)
                {
                    transform.Rotate(new Vector3(0, 0, -turnSpeed.GetFloat() * Time.deltaTime / 15), Space.World);
                }
            }
        }

    }
}
