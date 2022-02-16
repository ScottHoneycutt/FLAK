using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketPods : MonoBehaviour
{
    //Public variables----
    public float sFlightSpeed = 250;
    public float sTurnSpeed = 110;
    public float freeRandomization = 10;
    public float targetedRandomization = 10;
    public float randomizationShuffleDelay = 1.5f;
    public float sLifetime = 2;
    public float randomLifetimeAddition = .5f;
    public float targetRefreshDelay = 0.5f;
    public float detectionRange = 200;
    public float angleRange = 20;
    public GameObject missileLead;
    public GameObject missileRear;

    //Anti-tampering variables----
    private SecretFloat flightSpeed;
    private SecretFloat turnSpeed;
    private SecretFloat lifetime;

    //Target variables----
    private List<GameObject> targetsInRange = new List<GameObject>();
    private GameObject target;
    private float targetRefreshTimer = 0;
    private Vector3 influenceDetector;
    private Vector3 missileDirection;

    //Execution count, lifetime counter, and randomization timer----
    private int frameCounter = 0;
    private float lifetimeCounter = 0;
    private float trueLifetime;
    private float randomizationTimer;

    //Random variables----
    private float freeRandom = 0;
    private float targetedRandom = 0;

    //Missile angle variables----
    private float positionY1;
    private float positionY2;
    private float changeInY;

    private float flightAngle;

    private float positionX1;
    private float positionX2;
    private float changeInX;

    private float startAngle = 0;
    private bool startAngleTaken = false;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        flightSpeed = new SecretFloat(sFlightSpeed);
        turnSpeed = new SecretFloat(sTurnSpeed);
        lifetime = new SecretFloat(sLifetime);
    }

    void Start()
    {
        //Upgrades go here----
        sLifetime = sLifetime * Mathf.Pow(1.05f, controller.missileLevel.VerifyInt(controller.pMissileLevel));
        sTurnSpeed = sTurnSpeed * Mathf.Pow(1.05f, controller.missileLevel.VerifyInt(controller.pMissileLevel));
        sFlightSpeed = sFlightSpeed * Mathf.Pow(1.03f, controller.missileLevel.VerifyInt(controller.pMissileLevel));

        //Tech upgrades----
        if (controller.secondaryHydraulicsOwned.VerifyBool(controller.pSecondaryHydraulicsOwned))
        {
            sTurnSpeed = sTurnSpeed * 1.05f;
        }
        if (controller.persistenceHuntersOwned.VerifyBool(controller.pPersistenceHuntersOwned))
        {
            sLifetime = sLifetime * 1.4f;
        }
        if (controller.heavyPayloadOwned.VerifyBool(controller.pHeavyPayloadOwned))
        {
            sLifetime = sLifetime * .88f;
        }
        if (controller.thermalOpticsOwned.VerifyBool(controller.pThermalOpticsOwned))
        {
            angleRange = angleRange * 1.6f;
        }
        if (controller.aresClassWeaponSystemOwned.VerifyBool(controller.pAresClassWeaponSystemOwned))
        {
            sFlightSpeed = sFlightSpeed * 1.3f;
            sTurnSpeed = sTurnSpeed * 1.4f;
        }

        turnSpeed.Reset(sTurnSpeed);
        flightSpeed.Reset(sFlightSpeed);
        lifetime.Reset(sLifetime);
    }

    // Use this for initialization----
    void OnEnable()
    {
        target = null;
        //Instantly prompting a random variable for flight variation----
        randomizationTimer = randomizationShuffleDelay;
        //Instantly prompting a target identification----
        targetRefreshTimer = targetRefreshDelay;
        //Resetting lifetime----
        lifetimeCounter = 0;
        //Resetting startAngle variables----
        startAngle = 0;
        startAngleTaken = false;
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
            this.GetComponent<UnitHP>().health = 0;
        }

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeed.GetFloat()) * Time.deltaTime);

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

        //Setting startAngle----
        if (!startAngleTaken)
        {
            startAngle = Vector3.Angle(Identifiers.identifier.GetPlayerVelocity(), new Vector3(1,0,0));
            if (Identifiers.identifier.GetPlayerVelocity().y < 0)
            {
                startAngle = 360 - startAngle;
            }

            startAngleTaken = true;
        }

        //Updating/resetting randomizationTimer, refreshing random----
        randomizationTimer += Time.deltaTime;
        if (randomizationTimer > randomizationShuffleDelay)
        {
            randomizationTimer = 0;
            freeRandom = Random.Range(0 - freeRandomization, freeRandomization);
            targetedRandom = Random.Range(0 - targetedRandomization, targetedRandomization);
        }

        //Finding a target/new target (If target is unnasigned) and factoring randomization into otherwise linear path----
        if (!target || !target.activeInHierarchy)
        {
            //Rough missile angle change----
            float rotation = Mathf.LerpAngle(flightAngle, startAngle + freeRandom, 1);

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




            //Only allow a refresh every targetRefreshDelay seconds----
            if (targetRefreshTimer >= targetRefreshDelay)
            {
                //Reset targetRefreshTimer----
                targetRefreshTimer = 0;

                //Cleaing up targets that are no longer in range from targetInRange----
                targetsInRange.Clear();
                //Retreiving possibleTargets from the identifier----
                List<GameObject> possibleTargets = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
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
                            missileDirection = missileLead.transform.position - missileRear.transform.position;
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
            //Finding the direction vector from the missile to the target (target is assigned by SAMTurretTargetting)----
            Vector3 directionVector = target.transform.position - transform.position;

            //Finding the angle (0-180) between directionVector and the positive X axis----
            float basicAngle = Vector3.Angle(directionVector, new Vector3(1, 0, 0));

            //Converting that angle into numbers from 0-360 and including randomization;----
            float fullAngle = basicAngle + targetedRandom;

            if (directionVector.y < 0)
            {
                fullAngle = 360 - basicAngle + targetedRandom;
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
        }
    }
}
