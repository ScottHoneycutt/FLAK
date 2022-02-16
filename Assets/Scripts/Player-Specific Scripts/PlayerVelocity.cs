using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocity: MonoBehaviour
{
    //Public variables----
    public float topSpeed = 130;
    public float mininumSpeed = 70;
    public float basePitch = 0.8f;
    public float highPitch = 1.1f;
    public float lowPitch = 0.5f;
    public float minParticleSpeed = 1;
    public float maxParticleSpeed = 2;
    public float baseParticleSpeed = 1.5f;
    public PlayerFighterMovement fighter;
    public AudioSource soundRegulator;
    public ParticleSystem thrusterFlames;

    //Private variables----
    private float normalSpeed;
    private ParticleSystem.MainModule flames;
    private Camera cameracache;

    //Dump variables for tech upgrades----
    private float dumpTopSpeed;

    //GameControl-----
    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    //Setting normalSpeed----
    private void Start()
    {
        cameracache = Camera.main;
        normalSpeed = fighter.flightSpeed.GetFloat();
        flames = thrusterFlames.main;

        //Applying upgrades----
        //Incremental upgrades----
        mininumSpeed = mininumSpeed * Mathf.Pow(0.96f, controller.aerodynamicsLevel.VerifyInt(controller.pAerodynamicsLevel));
        topSpeed = topSpeed * Mathf.Pow(1.04f, controller.aerodynamicsLevel.VerifyInt(controller.pAerodynamicsLevel));

        //Tech upgrades----
        if (controller.extendedAerofoilsOwned.VerifyBool(controller.pExtendedAerofoilsOwned))
        {
            topSpeed = topSpeed * .98f;
            mininumSpeed = mininumSpeed * .98f;
        }
        if (controller.flightOptimizationOwned.VerifyBool(controller.pFlightOptimizationOwned))
        {
            topSpeed = topSpeed * 1.05f;
        }
        if (controller.thrustVectoringOwned.VerifyBool(controller.pThrustVectoringOwned))
        {
            topSpeed = topSpeed * 1.05f;
        }

        //dumping topspeed----
        dumpTopSpeed = topSpeed;

        if (controller.evasiveManeuversOwned.VerifyBool(controller.pEvasiveManeuversOwned))
        {
            StartCoroutine("RefreshEvasiveManeuvers");
        }
    }

    IEnumerable RefreshEvasiveManeuvers()
    {
        while (true)
        {
            if (controller.recentlyDamaged)
            {
                topSpeed = dumpTopSpeed * 1.1f;
            }
            else
            {
                topSpeed = dumpTopSpeed;
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    void Update()
    {
        //If Accelerate axis is greater than 0----
        if (Input.GetAxis("Accelerate") > 0)
        {
            fighter.flightSpeed.Reset(Mathf.Lerp(normalSpeed, topSpeed, Input.GetAxis("Accelerate")));
            //Accessing camera to begin camera shake----
            cameracache.GetComponent<CameraPlayerMovement>().afterburnBool = true;
            //Altering aircraft flight sound pitch----
            soundRegulator.pitch =  Mathf.Lerp(basePitch, highPitch, Input.GetAxis("Accelerate"));

            //Altering particle start speed for thrusters----
            flames.startSpeed = Mathf.Lerp(baseParticleSpeed, maxParticleSpeed, Input.GetAxis("Accelerate"));

            //Telling the GameControl to turn afterburnActive on----
            controller.afterburnActive = true;
        }
        //If Accelerate axis is less than 0----
        if (Input.GetAxis("Accelerate") < 0)
        {
            fighter.flightSpeed.Reset(Mathf.Lerp(mininumSpeed, normalSpeed, Input.GetAxis("Accelerate") + 1));
            //Accessing camera to stop camera shake----
            cameracache.GetComponent<CameraPlayerMovement>().afterburnBool = false;
            //Altering aircraft flight sound pitch----
            soundRegulator.pitch = Mathf.Lerp(lowPitch, basePitch, Input.GetAxis("Accelerate") + 1);

            //Altering particle start speed for thrusters----
            flames.startSpeed = Mathf.Lerp(minParticleSpeed, baseParticleSpeed, Input.GetAxis("Accelerate")) + 1;

            //Telling the GameControl to turn afterburnActive off----
            controller.afterburnActive = false;
        }
        //if Accelerate axis is 0----
        if (Input.GetAxis("Accelerate") == 0)
        {
            fighter.flightSpeed.Reset(normalSpeed);
            //Accessing camera to stop camera shake----
            cameracache.GetComponent<CameraPlayerMovement>().afterburnBool = false;
            //Setting aircraft flight sound pitch back to base----
            soundRegulator.pitch = basePitch;

            //Setting thruster particle speed back to base----
            flames.startSpeed = baseParticleSpeed;

            //Telling the GameControl to turn afterburnActive off----
            controller.afterburnActive = false;
        }
    }
}
