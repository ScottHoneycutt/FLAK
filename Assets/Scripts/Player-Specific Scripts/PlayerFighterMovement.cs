using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighterMovement : MonoBehaviour {

    //public variables----
    public float sFlightSpeed = 100;
    public float sTurnSpeed = 90;
    public float gravitySpeed = 1;
    public float correctionTime = 1;
    public float correctionDelay = 2;
    public float leftBoundary = -1000;
    public float rightBoundary = 1000;
    public float topBoundary = 1000;
    public GameObject returnToBattle;
    public GameObject droneHomePoint;
    public Object dronePrefab;

    //Anti-tampering measures----
    public SecretFloat flightSpeed; //Public because the Escort Drone needs to reference----
    public SecretFloat turnSpeed;   //Public because the Escort Drone needs to reference----
    private SecretFloat dumpTurnSpeed;

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

    private Vector3 playerVelocityVector = new Vector3(0, 0, 0);

    //Timer----
    private float timer1 = 0;
    private float timer2 = 0;

    //Upside-down correction variables----
    private bool flyingRight;
    private bool orientedRight;
    private bool correcting1 = false;
    private bool correcting2 = false;

    //Boundary variables----
    private float fighterX;
    private float fighterY;

    //GameControl----
    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        flightSpeed = new SecretFloat(sFlightSpeed);
        turnSpeed = new SecretFloat(sTurnSpeed);
        dumpTurnSpeed = new SecretFloat(sTurnSpeed);
    }

    private void Start()
    {
        //Implimenting upgrades----
        //Incremental upgrades----
        sTurnSpeed = sTurnSpeed * Mathf.Pow(1.05f, controller.aerodynamicsLevel.VerifyInt(controller.pAerodynamicsLevel));

        //Tech upgrades----
        if (controller.extendedAerofoilsOwned.VerifyBool(controller.pExtendedAerofoilsOwned))
        {
            sTurnSpeed *= 1.1f;
            sFlightSpeed *= .98f;
        }
        if (controller.secondaryHydraulicsOwned.VerifyBool(controller.pSecondaryHydraulicsOwned))
        {
            sTurnSpeed *= 1.05f;
        }
        if (controller.thrustVectoringOwned.VerifyBool(controller.pThrustVectoringOwned))
        {
            sFlightSpeed *= 1.03f;
            sTurnSpeed *= 1.05f;
        }
        //Dumping turnspeed----
        dumpTurnSpeed.Reset(sTurnSpeed);

        flightSpeed.Reset(sFlightSpeed);
        turnSpeed.Reset(sTurnSpeed);

        StartCoroutine("RefreshEvasiveManeuvers");

        //Setting up drone if the player has the utility equipped----
        if (controller.escortDroneEquipped.VerifyBool(controller.pEscortDroneEquipped))
        {
            GameObject drone = Instantiate(dronePrefab, droneHomePoint.transform.position, droneHomePoint.transform.rotation) as GameObject;
            //Assigning missing variables to the drone----
            drone.GetComponent<EscortDroneMovement>().fighter = droneHomePoint.transform.parent.GetComponent<PlayerFighterMovement>();
            drone.GetComponent<EscortDroneMovement>().homePoint = droneHomePoint;
        }

    }

    IEnumerable RefreshEvasiveManeuvers()
    {
        while (true)
        {
            if (controller.recentlyDamaged)
            {
                turnSpeed.Reset(dumpTurnSpeed.GetFloat() * 1.1f);
            }
            else
            {
                turnSpeed.Reset(dumpTurnSpeed.GetFloat());
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    // FixedUpdate is called when physics engine runs
    void FixedUpdate () {

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

        playerVelocityVector = new Vector3(changeInX, changeInY, 0);

        //Sending aircraft velocity vector to Identifiers for bomb use----
        Identifiers.identifier.SendPlayerVelocity(playerVelocityVector);

        //Finding basic aircraft angle----
        flightAngle = Vector3.Angle(playerVelocityVector, new Vector3(1, 0, 0));

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

        //Mouse position----
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        mouseX = mouseX - (Screen.width) * (float)(.5);
        mouseY = mouseY - (Screen.height) * (float)(.5);

        //Mouse radius----
        float radius = Mathf.Sqrt(Mathf.Pow(mouseX , 2) + Mathf.Pow(mouseY, 2));

        //Mouse angle----
        float mouseAngle = Mathf.Asin(mouseY / radius)*Mathf.Rad2Deg;

        if (mouseY > 0)
        {
            if (mouseX < 0)
            {
                mouseAngle = -mouseAngle + 180;
            }
            else if (mouseX > 0)
            {
                //No change----
            }
        }
        else if (mouseY < 0)
        {
            if (mouseX > 0)
            {
                mouseAngle = mouseAngle + 360;
            }
            if (mouseX < 0)
            {
                mouseAngle = -mouseAngle + 180;
            }
        }

        //Boundary violation tests and corrections----
        fighterX = transform.position.x;
        fighterY = transform.position.y;

        if (fighterX > rightBoundary)
        {
            //only turn up to prevent forced ground collisions----
            if (changeInY < 0)
            {
                mouseAngle = 90;
            }
            else
            {
                mouseAngle = 179;
            }
            returnToBattle.SetActive(true);
        }
        else if (fighterX < leftBoundary)
        {
            //only turn up to prevent forced ground collisions----
            if (changeInY < 0)
            {
                mouseAngle = 90;
            }
            else
            {
                mouseAngle = 1;
            }
            returnToBattle.SetActive(true);
        }
        else if (fighterY > topBoundary)
        {
            mouseAngle = 270+Random.Range(-1,1);
            returnToBattle.SetActive(true);
        }
        else if (returnToBattle.activeSelf)
        {
            returnToBattle.SetActive(false);
        }

        //Rough aircraft angle change----
        rotation = Mathf.LerpAngle(flightAngle, mouseAngle, 1);

        if (rotation > flightAngle + 3 || rotation < flightAngle - 3)
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
        else if(orientedRight == flyingRight)
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
