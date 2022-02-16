using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    //Public variables----
    public float approachDistance = 900;
    public bool advance = false;
    public bool movesLeft = true;
    public float baseSpeed = 25;
    public float terrainDistance = 120;
    public float refreshPeriod = .5f;
    public GameObject leftFront;
    public GameObject rightFront;

    //Private variables----
    private float timer = 0;
    private bool canMove = true;
    private float speed = 0;
    private List<GameObject> alliedBoats = new List<GameObject>();
    private RaycastHit hit;
    private LayerMask mask = (1 << 18);

    // Update is called once per frame
    void Update()
    {
        //Only run if advance is on----
        if (advance)
        {
            //Refresh----
            timer += Time.deltaTime;
            if (timer >= refreshPeriod)
            {
                //Resetting variables----
                timer = 0;
                canMove = true;

                //Getting list of ship targeters----
                alliedBoats = Identifiers.identifier.ReturnAlliedShipTargeters();

                //Enemy boats use approachDistance----
                foreach (GameObject targeter in alliedBoats)
                {
                    //distance between this ship and targeter to determine if ship can move----
                    if (Vector3.Distance(transform.position, targeter.transform.position) <= approachDistance)
                    {
                        canMove = false;
                    }
                }

                //Only use raycast test if the ship is ready to move----
                if (canMove)
                {
                    //raycast left----
                    if (movesLeft)
                    {
                        bool hitRegister = Physics.Raycast(leftFront.transform.position, Vector3.left, out hit, terrainDistance, mask);
                        //Do not move if terrain is too close----
                        if (hitRegister)
                        {
                            canMove = false;
                        }
                    }
                    //raycast right----
                    if (!movesLeft)
                    {
                        bool hitRegister = Physics.Raycast(rightFront.transform.position, Vector3.right, out hit, terrainDistance, mask);
                        //Do not move if terrain is too close----
                        if (hitRegister)
                        {
                            canMove = false;
                        }
                    }
                }
            }


            //If there is room to advance----
            if (canMove)
            {
                speed = baseSpeed;
                //move left----
                if (movesLeft)
                {
                    transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
                }
                //move right----
                if (!movesLeft)
                {
                    transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
                }
            }
            //Slowdown----
            else
            {
                if (speed >= 1)
                {
                    speed = Mathf.Lerp(speed, 0, .05f);
                    //move left----
                    if (movesLeft)
                    {
                        transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
                    }
                    //move right----
                    if (!movesLeft)
                    {
                        transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
                    }
                }
            }
        }
    }
}
