using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedFighterMovement : MonoBehaviour {

    //public variables----
    public float sFlightSpeed = 100;
    public float sTurnSpeed = 90;
    public float gravitySpeed = 0.1f;
    public float correctionTime = 1;
    public float correctionDelay = 0.75f;

    public float evadeDistance = 150;
    public float chaseTriggerDistance = 300;
    public float chasingDistance = 1000;
    public float rechaseTime = 2;
    public float randomizer = 5;
    public float randomRefresh = 2;

    public Transform fighter;

    public GameObject patrolPoint1;
    public GameObject patrolPoint2;

    public GameObject target = null;

    //Secret and protected values (anti-tampering measures)----
    SecretFloat flightSpeed;
    SecretFloat turnSpeed;

    //Variables for runway simulation----
    public bool takeoff = false;
    public float takeoffDuration = 1.5f;

    private float takeoffTargetAngle = 0;
    private float takeoffTimer = 0;
    private bool takeoffStarted = false;
    private float baseTakeoffTargetAngle;

    //Execution count----
    private int frameCounter = 0;

    //Aircraft angle variables----
    private float positionY1 = 0;
    private float positionY2 = 0;
    private float changeInY;

    private float flightAngle = 0;

    private float positionX1 = 0;
    private float positionX2 = 0;
    private float changeInX;

    private float rotation = 0;

    //Behavior regulation variables----
    //Leashed variable----
    private bool leashed = false;
    //Target position variable----
    private float targetAngle;
    //Switching between patrolPoints----
    private bool firstPatrol = false;
    //Randomization in patrol direction----
    private float random = 0;
    //Too close to ground----
    private bool groundClose = false;

    //Timers----
    private float timer1 = 0;
    private float timer2 = 0;
    private float randomizeTimer = 0;

    //Upside-down correction variables----
    private bool flyingRight;
    private bool orientedRight;
    private bool correcting1 = false;
    private bool correcting2 = false;

    //Dump variables----
    private float turnSpeedBase;
    private float flightSpeedBase;

    private void Awake()
    {
        //Anti-tampering measures (SecretFloats)----
        flightSpeed = new SecretFloat(sFlightSpeed);
        turnSpeed = new SecretFloat(sTurnSpeed);
    }

    private void OnEnable()
    {
        firstPatrol = false;

        turnSpeedBase = turnSpeed.GetFloat();
        flightSpeedBase = flightSpeed.GetFloat();
    }

    //Recieve message from Ground Proximity Hitbox----
    public void NotifyGroundClose(bool isclose)
    {
        //Is ground close----
        groundClose = isclose;
    }

    // FixedUpdate is called when physics engine runs----
    void FixedUpdate()
    {
        //Change variables if aircraft is taking off----
        if (takeoff)
        {
            //Dropping mobility to 0 at start of takeoff----
            if (!takeoffStarted)
            {
                takeoffStarted = true;
                turnSpeed.Reset(0);
                flightSpeed.Reset(0);
                baseTakeoffTargetAngle = takeoffTargetAngle;
            }
            //Icrementing takeoff timer----
            takeoffTimer += Time.deltaTime;

            //Increasing flightspeed and turnspeed over time----
            flightSpeed.Reset(Mathf.Lerp(flightSpeed.GetFloat(), flightSpeedBase, Time.deltaTime));
            turnSpeed.Reset(Mathf.Lerp(turnSpeed.GetFloat(), turnSpeedBase, .1f * Time.deltaTime));

            //Turning off takeoff once takeoff has completed----
            if (takeoffTimer >= takeoffDuration)
            {
                //Resetting variables once completed for when the same fighter is called again from object pooler----
                takeoffTimer = 0;
                takeoff = false;
                takeoffStarted = false;
                takeoffTargetAngle = baseTakeoffTargetAngle;
            }
        }
        else
        {
            turnSpeed.Reset(turnSpeedBase);
            flightSpeed.Reset(flightSpeedBase);
        }

        //Finding Y difference----
        frameCounter++;

        if (frameCounter % 2 == 1)
        {
            positionY1 = fighter.position.y;

            changeInY = positionY1 - positionY2;
            changeInY = changeInY / Time.deltaTime;
        }
        if (frameCounter % 2 == 0)
        {
            positionY2 = fighter.position.y;

            changeInY = positionY2 - positionY1;
            changeInY = changeInY / Time.deltaTime;
        }

        //Finding X difference----
        if (frameCounter % 2 == 1)
        {
            positionX1 = fighter.position.x;

            changeInX = positionX1 - positionX2;
            changeInX = changeInX / Time.deltaTime;
        }
        if (frameCounter % 2 == 0)
        {
            positionX2 = fighter.position.x;

            changeInX = positionX2 - positionX1;
            changeInX = changeInX / Time.deltaTime;
        }

        //Finding basic aircraft angle----
        flightAngle = Vector3.Angle(new Vector3(changeInX, changeInY, 0), new Vector3(1, 0, 0));

        //Finding actual aircraft angle----
        if (changeInY < 0)
        {
            flightAngle = 360 - flightAngle;
        }

        //Gravity simulation----
        float flightSpeedCom = flightSpeed.GetFloat();

        if (frameCounter > 3)
        {
            float gravityPlaneAngle = flightAngle;

            if (gravityPlaneAngle >= 90 && gravityPlaneAngle <= 180)
            {
                gravityPlaneAngle = -gravityPlaneAngle + 180;
            }
            else if (gravityPlaneAngle > 180)
            {
                gravityPlaneAngle = 0;
            }
            flightSpeedCom = flightSpeed.GetFloat() - (float)(gravityPlaneAngle * gravitySpeed);
        }

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeedCom) * Time.deltaTime);


        //Behavior stuff----
        //Increment timer----
        randomizeTimer += Time.deltaTime;

        //Refresh randomization for patrol----
        if (randomizeTimer >= randomRefresh)
        {
            randomizeTimer = 0;
            random = Random.Range(-randomizer, randomizer);
        }

        //Retrieving list of missiles from identifiers----
        List<GameObject> missiles = Identifiers.identifier.ReturnEnemyMissiles();
        //Setup for evasion----
        bool threatened = false;
        GameObject threat = null;

        //Missile threatening (go through list of missiles)?----
        float tempfloat = 0;
        for (int i = 0; i < missiles.Count; i++)
        {
            if (missiles[i])
            {
                //If there is a missile within a certain distance----
                if (Vector3.Distance(missiles[i].transform.position, fighter.position) <= evadeDistance)
                {
                    threatened = true;

                    //Find closest missile----
                    if (tempfloat < Vector3.Distance(missiles[i].transform.position, fighter.position))
                    {
                        tempfloat = Vector3.Distance(missiles[i].transform.position, fighter.position);
                        threat = missiles[i];
                    }
                }
            }
        }

        //Retrieving list of enemies from idenfiers----
        List<GameObject> hostiles = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
        //Setup for chase----
        bool engaged = false;
        target = null;

        //Target to chase (go through list of hostiles)?----
        float tempfloat2 = 0;
        for (int i = 0; i < hostiles.Count; i++)
        {
            if (hostiles[i])
            {
                //If there is a target within a certain distance----
                if (Vector3.Distance(hostiles[i].transform.position, fighter.position) <= chaseTriggerDistance)
                {
                    engaged = true;

                    //Find closest target----
                    if (tempfloat2 < Vector3.Distance(hostiles[i].transform.position, fighter.position))
                    {
                        tempfloat2 = Vector3.Distance(hostiles[i].transform.position, fighter.position);
                        target = hostiles[i];
                    }
                }
            }
        }
        //Resetting leashed----
        leashed = false;

        //Flipping firstPatrol for patrol behavior----
        if (Vector3.Distance(fighter.position, patrolPoint1.transform.position) < 15)
        {
            firstPatrol = false;
        }
        if (Vector3.Distance(fighter.position, patrolPoint2.transform.position) < 15)
        {
            firstPatrol = true;
        }

        turnSpeed.Reset(turnSpeedBase);
        //Behavior regulations----
        //If the hitbox intersects with the ground and the aircraft is not taking off----
        if (groundClose && !takeoff)
        {
            turnSpeed.Reset(turnSpeed.GetFloat() * 2);
            //Start crash avoidance behavior (pull up)----
            targetAngle = 90;
        }
        //Otherwise, proceed with evasion tests----
        else if (threatened)
        {
            //Angle between missile position relative to aircraft and positive X direction----
            float missileAngle = Vector3.Angle(threat.transform.position - fighter.position, new Vector3(1, 0, 0));

            //missileAngle adjustment----
            if (threat.transform.position.y < fighter.position.y)
            {
                missileAngle = 360 - missileAngle;
            }
            //Selecting which way to turn to evade a missile----
            float rng = Random.Range(0, 1);

            if (rng == 0)
            {
                targetAngle = missileAngle - 50;
            }
            else if (rng == 1)
            {
                targetAngle = missileAngle + 50;
            }
        }
        //Otherwise, proceed with chase tests----
        else if (engaged)
        {
            //Chase if target is within chasingDistance of patrolPoints or override distance (chasetrigger/2) of fighter----
            if (Vector3.Distance(target.transform.position, patrolPoint1.transform.position) < chasingDistance || Vector3.Distance(target.transform.position, patrolPoint2.transform.position) < chasingDistance || Vector3.Distance(target.transform.position, fighter.position) < chaseTriggerDistance / 2)
            {
                //Angle between target position relative to aircraft and positive X direction----
                float hostileAngle = Vector3.Angle(target.transform.position - transform.position, new Vector3(1, 0, 0));

                //hostileAngle adjustment----
                if (target.transform.position.y < transform.position.y)
                {
                    hostileAngle = 360 - hostileAngle;
                }
                //Setting targetAngle to hostileAngle----
                targetAngle = hostileAngle;
            }
            //If chasingDistance is exceeded for both patrolpoints----
            if (Vector3.Distance(target.transform.position, patrolPoint1.transform.position) > chasingDistance && Vector3.Distance(target.transform.position, patrolPoint2.transform.position) > chasingDistance)
            {
                leashed = true;
            }
        }
        //Otherwise, return to patrol----
        else
        {
            if (firstPatrol)
            {
                //Angle between patrolPoint1 position relative to aircraft and positive X direction----
                float patrolAngle = Vector3.Angle(patrolPoint1.transform.position - fighter.position, new Vector3(1, 0, 0));

                //patrolAngle adjustment----
                if (patrolPoint1.transform.position.y < fighter.position.y)
                {
                    patrolAngle = 360 - patrolAngle;
                }
                //Setting targetAngle to patrolAngle----
                targetAngle = patrolAngle + random;
            }
            else if (!firstPatrol)
            {
                //Angle between patrolPoint2 position relative to aircraft and positive X direction----
                float patrolAngle = Vector3.Angle(patrolPoint2.transform.position - fighter.position, new Vector3(1, 0, 0));

                //patrolAngle adjustment----
                if (patrolPoint2.transform.position.y < fighter.position.y)
                {
                    patrolAngle = 360 - patrolAngle;
                }
                //Setting targetAngle to patrolAngle----
                targetAngle = patrolAngle + random;
            }
        }
        //Initiating patrol if leashed and not pulling up----
        if (leashed && !groundClose)
        {
            if (firstPatrol)
            {
                //Angle between patrolPoint1 position relative to aircraft and positive X direction----
                float patrolAngle = Vector3.Angle(patrolPoint1.transform.position - fighter.position, new Vector3(1, 0, 0));

                //patrolAngle adjustment----
                if (patrolPoint1.transform.position.y < fighter.position.y)
                {
                    patrolAngle = 360 - patrolAngle;
                }
                //Setting targetAngle to patrolAngle----
                targetAngle = patrolAngle + random;
            }
            else if (!firstPatrol)
            {
                //Angle between patrolPoint2 position relative to aircraft and positive X direction----
                float patrolAngle = Vector3.Angle(patrolPoint2.transform.position - fighter.position, new Vector3(1, 0, 0));

                //patrolAngle adjustment----
                if (patrolPoint2.transform.position.y < fighter.position.y)
                {
                    patrolAngle = 360 - patrolAngle;
                }
                //Setting targetAngle to patrolAngle----
                targetAngle = patrolAngle + random;
            }
        }


        //Overriding behaviors target angle if takeoff is active----
        if (takeoff)
        {
            targetAngle = takeoffTargetAngle;
            takeoffTargetAngle += 2 * Time.deltaTime;
        }

        //Rough aircraft angle change----
        rotation = Mathf.LerpAngle(flightAngle, targetAngle, 1);

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

        //Flying right?----
        if (changeInX > 0)
        {
            flyingRight = true;
        }
        else if (changeInX < 0)
        {
            flyingRight = false;
        }

        //Facing right initialization----
        if (frameCounter == 2)
        {
            orientedRight = flyingRight;
        }

        //Matching values and correction delay/which correction to make----
        if (orientedRight == true && flyingRight == false && correcting1 == false && correcting2 == false)
        {
            timer1 = timer1 + (1 * Time.deltaTime);
            if (timer1 >= correctionDelay)
            {
                correcting1 = true;
                orientedRight = flyingRight;
            }
        }
        else if (orientedRight == flyingRight)
        {
            timer1 = 0;
        }

        if (orientedRight == false && flyingRight == true && correcting1 == false && correcting2 == false)
        {
            timer1 = timer1 + (1 * Time.deltaTime);
            if (timer1 >= correctionDelay)
            {
                correcting2 = true;
                orientedRight = flyingRight;
            }
        }
        else if (orientedRight == flyingRight)
        {
            timer1 = 0;
        }

        //Actual correction----
        if (correcting1)
        {
            timer2 = timer2 + (1 * Time.deltaTime);
            if (timer2 <= correctionTime)
            {
                transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime / correctionTime));
            }
            if (timer2 > correctionTime)
            {
                timer2 = 0;
                correcting1 = false;
            }
        }
        else if (correcting2)
        {
            timer2 = timer2 + (1 * Time.deltaTime);
            if (timer2 <= correctionTime)
            {
                transform.Rotate(new Vector3(0, 0, -180 * Time.deltaTime / correctionTime));
            }
            if (timer2 > correctionTime)
            {
                timer2 = 0;
                correcting2 = false;
            }
        }
    }
}
