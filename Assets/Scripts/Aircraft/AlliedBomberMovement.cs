using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedBomberMovement : MonoBehaviour
{
    //Public variables----
    public Transform bomberTransform;
    public List<Transform> waypoints = new List<Transform>();
    public float waypointDistance = 50;
    public List<Transform> targets = new List<Transform>();
    public float flightSpeed = 60;
    public float turnSpeed = 20; 
    public float bombingRange = 600;
    public float sDelayBetweenBombs = .5f;

    //Secret and protected values (anti-tampering measures)----
    private SecretFloat delayBetweenBombs;

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

    //Waypoint stuff for determinitng which waypoint to move towards----
    private int waypointIndex = 0;

    //Private bombing variables----
    private float bombTimer = 0;
    private bool bombing = false;


    private void Awake()
    {
        //Anti-tampering measures (SecretFloats)----
        delayBetweenBombs = new SecretFloat(sDelayBetweenBombs);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        bombTimer = delayBetweenBombs.GetFloat();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Updating waypoints----
        if (Vector3.Distance(bomberTransform.position, waypoints[waypointIndex].position) <= waypointDistance && waypointIndex < waypoints.Count)
        {
            waypointIndex++;
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

        //Forward flight----
        transform.Translate(new Vector3(0, 0, flightSpeed) * Time.deltaTime);


        //Angle between current waypoint position relative to aircraft and positive X direction----
        float waypointAngle = Vector3.Angle(waypoints[waypointIndex].position - bomberTransform.position, new Vector3(1, 0, 0));
        //patrolAngle adjustment----
        if (waypoints[waypointIndex].position.y < bomberTransform.position.y)
        {
            waypointAngle = 360 - waypointAngle;
        }


        //Rough aircraft angle change----
        rotation = Mathf.LerpAngle(flightAngle, waypointAngle, 1);

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


        bombing = false;
        //Checking to see if bomber should start dropping bombs----
        for(int i = 0; i < targets.Count; i++)
        {
            //Proximity to bomber and active/inactive gameobject----
            if(Vector3.Distance(targets[i].position, bomberTransform.position) <= bombingRange && targets[i].gameObject.activeInHierarchy)
            {
                bombing = true;
            }
        }
        //Dropping bombs every delay period----
        if (bombing)
        {
            bombTimer += Time.deltaTime;
            if (bombTimer >= delayBetweenBombs.GetFloat())
            {
                bombTimer = 0;
                ObjectPoolManager.manager.SpawnFromPool("Allied Bombs", bomberTransform.position, bomberTransform.rotation);
            }
        }
    }
}
