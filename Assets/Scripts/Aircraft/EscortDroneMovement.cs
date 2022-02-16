using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortDroneMovement : MonoBehaviour
{
    //public variables----
    public float flightSpeed = 100;
    public float turnSpeed = 90;
    public float gravitySpeed = 0.1f;
    public float correctionTime = 1;
    public float correctionDelay = 0.75f;

    public float chaseTriggerDistance = 300;
    public float chasingDistance = 1000;
    public float rechaseTime = 2;

    public GameObject homePoint;

    public GameObject target = null;

    public PlayerFighterMovement fighter;
    
    public UnitHP escortHp;

    public bool engaged = false;

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
    private bool groundClose = false;
    //Leashed variable----
    private bool leashed = false;
    //Target position variable----
    private float targetAngle;

    //Timers----
    private float timer1 = 0;
    private float timer2 = 0;

    //Upside-down correction variables----
    private bool flyingRight;
    private bool orientedRight;
    private bool correcting1 = false;
    private bool correcting2 = false;

    //Setting variables----
    private void Start()
    {
        turnSpeed = fighter.turnSpeed.GetFloat();
        flightSpeed = fighter.flightSpeed.GetFloat();
        Identifiers.identifier.GetEscortDrone(this.gameObject);
    }

    //Recieve message from Ground Proximity Hitbox----
    public void NotifyGroundClose(bool isclose)
    {
        //Is the ground close?----
        groundClose = isclose;
    }

    // FixedUpdate is called when physics engine runs----
    void FixedUpdate()
    {
        //Drone dies with player fighter----
        if (!fighter.gameObject.activeSelf)
        {
            escortHp.health = 0;
        }

        //Finding Y difference----
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

        //Finding X difference----
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

        //Finding basic aircraft angle----
        flightAngle = Vector3.Angle(new Vector3(changeInX, changeInY, 0), new Vector3(1, 0, 0));

        //Finding actual aircraft angle----
        if (changeInY < 0)
        {
            flightAngle = 360 - flightAngle;
        }

        //Gravity simulation----
        float flightSpeedCom = flightSpeed;

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
            flightSpeedCom = flightSpeed - (gravityPlaneAngle * gravitySpeed);
        }

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeedCom) * Time.deltaTime);

        //Behavior stuff----

        //Refresh behavior stuff----
        //Retrieving list of enemies from idenfiers----
        List<GameObject> hostiles = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
        //Setup for chase----
        engaged = false;
        target = null;

        //Target to chase (go through list of hostiles)?----
        float tempfloat2 = 0;
        for (int i = 0; i < hostiles.Count; i++)
        {
            if (hostiles[i])
            {
                //If there is a target within a certain distance----
                if (Vector3.Distance(hostiles[i].transform.position, transform.position) <= chaseTriggerDistance)
                {
                    engaged = true;

                    //Find closest target----
                    if (tempfloat2 < Vector3.Distance(hostiles[i].transform.position, transform.position))
                    {
                        tempfloat2 = Vector3.Distance(hostiles[i].transform.position, transform.position);
                        target = hostiles[i];
                    }
                }
            }
        }
        //Resetting leashed----
        leashed = false;

        //Grabbing player fighter variables to keep up----
        flightSpeed = fighter.flightSpeed.GetFloat();
        turnSpeed = fighter.turnSpeed.GetFloat();

        //Behavior regulations----
        //If the ray hit the ground----
        if (groundClose)
        {
            turnSpeed*=2;
            //Start crash avoidance behavior (pull up)----
            targetAngle = 90;
        }
        //Otherwise, proceed with chase tests----
        else if (engaged)
        {
            //Chase if target is within chasingDistance of patrolPoints----
            if (Vector3.Distance(target.transform.position, homePoint.transform.position) < chasingDistance || Vector3.Distance(target.transform.position, homePoint.transform.position) < chasingDistance)
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
            //If chasingDistance is exceeded----
            if (Vector3.Distance(target.transform.position, homePoint.transform.position) > chasingDistance || Vector3.Distance(target.transform.position, homePoint.transform.position) > chasingDistance)
            {
                leashed = true;
            }
        }
        //Otherwise, return to patrol----
        else
        {
            //Angle between patrolPoint1 position relative to aircraft and positive X direction----
            float patrolAngle = Vector3.Angle(homePoint.transform.position - transform.position, new Vector3(1, 0, 0));

            //patrolAngle adjustment----
            if (homePoint.transform.position.y < transform.position.y)
            {
                patrolAngle = 360 - patrolAngle;
            }
            //Setting targetAngle to patrolAngle----
            targetAngle = patrolAngle;

            //Increasing maneuverability if the player is far away----
            if (Vector3.Distance(transform.position, homePoint.transform.position) > 100)
            {
                flightSpeed *= 1.5f;
                turnSpeed *= 1.5f;
            }
        }
        //Initiating patrol if leashed and not pulling up----
        if (leashed && !groundClose)
        {
            //Angle between patrolPoint1 position relative to aircraft and positive X direction----
            float patrolAngle = Vector3.Angle(homePoint.transform.position - transform.position, new Vector3(1, 0, 0));

            //patrolAngle adjustment----
            if (homePoint.transform.position.y < transform.position.y)
            {
                patrolAngle = 360 - patrolAngle;
            }
            //Setting targetAngle to patrolAngle----
            targetAngle = patrolAngle;

            //Increasing maneuverability if the player is far away----
            if (Vector3.Distance(transform.position, homePoint.transform.position) > 100)
            {
                flightSpeed *= 1.5f;
                turnSpeed *= 1.5f;
            }
        }


        //Rough aircraft angle change----
        rotation = Mathf.LerpAngle(flightAngle, targetAngle, 1);

        if ((rotation > flightAngle + 3 || rotation < flightAngle - 3))
        {
            if (rotation > flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime), Space.World);
            }

            if (rotation < flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime), Space.World);
            }
        }

        //Slowdown when approaching target angle----
        else if ((rotation > flightAngle + 2 || rotation < flightAngle - 2))
        {
            if (rotation > flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime / 3), Space.World);
            }

            if (rotation < flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime / 3), Space.World);
            }
        }

        else if ((rotation > flightAngle + 1 || rotation < flightAngle - 1))
        {
            if (rotation > flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime / 5), Space.World);
            }

            if (rotation < flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime / 5), Space.World);
            }
        }

        else if ((rotation > flightAngle + .5 || rotation < flightAngle - .5))
        {
            if (rotation > flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime / 10), Space.World);
            }

            if (rotation < flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime / 10), Space.World);
            }
        }

        else if ((rotation > flightAngle + .1 || rotation < flightAngle - .1))
        {
            if (rotation > flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime / 15), Space.World);
            }

            if (rotation < flightAngle)
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime / 15), Space.World);
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
