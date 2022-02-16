using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStormMissile : MonoBehaviour
{
    //public variables----
    public float sFlightSpeed = 270;
    public float sTurnSpeed = 120;
    public float randomization = 30;
    public float randomizationShuffleDelay = .4f;
    public float sLifetime = 4;
    public float randomLifetimeAddition = .5f;
    public float jammingProbability = 3;
    public float jammingPeriod = .1f;
    public GameObject target;
    public UnitHP hp;

    //Anti-tampering measures----
    private SecretFloat flightSpeed;
    private SecretFloat turnSpeed;
    private SecretFloat lifetime;

    //Randomizer for missile jammer----
    private int jamRandom;

    //Execution count, lifetime counter, and randomization timer----
    private int frameCounter = 0;
    private float lifetimeCounter = 0;
    private float trueLifetime;
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

    private void Awake()
    {
        flightSpeed = new SecretFloat(sFlightSpeed);
        turnSpeed = new SecretFloat(sTurnSpeed);
        lifetime = new SecretFloat(sLifetime);
    }

    // Use this for initialization
    void OnEnable()
    {
        trueLifetime = lifetime.GetFloat() + Random.Range(0, randomLifetimeAddition);
        //Instantly prompting a random variable for flight variation----
        randomizationTimer = randomizationShuffleDelay;
        //Resetting lifetime----
        lifetimeCounter = 0;
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


    // FixedUpdate is called when physics engine runs----
    void FixedUpdate()
    {
        //Incrementing lifetimeCounter----
        lifetimeCounter += Time.deltaTime;

        //Detonating missile if it is past its lifetime----
        if (lifetimeCounter >= trueLifetime)
        {
            this.GetComponent<UnitHP>().health = 0;
            lifetimeCounter = 0;
        }
        //Initaializing some variables to prevent errors----
        float rotation;
        float fullAngle;
        float basicAngle;
        Vector3 directionVector;

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeed.GetFloat()) * Time.deltaTime);
        
        //If there is a target (important to prevent errors if the target is eliminated)----
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
            directionVector = target.transform.position - transform.position;

            //Finding the angle (0-180) between directionVector and the positive X axis----
            basicAngle = Vector3.Angle(directionVector, new Vector3(1, 0, 0));

            //Updating/resetting randomizationTimer, refreshing random----
            randomizationTimer += Time.deltaTime;
            if (randomizationTimer > randomizationShuffleDelay)
            {
                randomizationTimer = 0;
                random = Random.Range(0 - randomization, randomization);
            }

            //Converting that angle into numbers from 0-360 and including randomization;----
            fullAngle = basicAngle + random;

            if (directionVector.y < 0)
            {
                fullAngle = 360 - basicAngle + random;
            }

            //Rough missile angle change----
            rotation = Mathf.LerpAngle(flightAngle, fullAngle, 1);

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
