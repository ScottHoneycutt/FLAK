using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlakExplosion : MonoBehaviour
{
    //Public variables----
    public float shrapnelDamage = 1;
    public float vsShipDamageMultiplier = .5f;
    public ParticleSystem shrapnel;

    //private variables----
    private float dumpShrapnelDamage;

    //List of particle collision events----
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    private void OnEnable()
    {
        //Verifying GameControl save values----
        controller.closeInSupportOwned.VerifyBool(controller.pCloseInSupportOwned);
        controller.closeInSupportOwned.VerifyBool(controller.pCloseInSupportOwned);
    }

    //Void OnParticleCollision----
    void OnParticleCollision(GameObject impactTarget)
    {
        //Collision count and changing the list of particle collision events----
        int collisionCount = ParticlePhysicsExtensions.GetCollisionEvents(shrapnel, impactTarget, collisionEvents);


        //Running through the list of collision events----
        for (int i = 0; i < collisionCount; i++)
        {
            //If impacted object has health----
            if (impactTarget.GetComponent<UnitHP>() != null)
            {
                UnitHP healthScript = impactTarget.GetComponent<UnitHP>();

                //Checking damage modifiers----
                //Both----
                if (controller.recentlyDamaged && controller.pKineticRecyclingOwned && controller.slowdownActive && controller.pCloseInSupportOwned)
                {
                    shrapnelDamage = dumpShrapnelDamage * 1.2f;
                    shrapnelDamage = shrapnelDamage * 1.1f;
                }
                //Kinetic----
                else if (controller.recentlyDamaged && controller.pKineticRecyclingOwned)
                {
                    shrapnelDamage = dumpShrapnelDamage * 1.2f;
                }
                //Close-in----
                else if (controller.slowdownActive && controller.pCloseInSupportOwned)
                {
                    shrapnelDamage = dumpShrapnelDamage * 1.1f;
                }
                //Neither----
                else
                {
                    shrapnelDamage = dumpShrapnelDamage;
                }

                //Factor in damage reduction for enemy ships----
                if (impactTarget.layer == 19)
                {
                    //Reduce its health by shrapnelDamage * reduction value----
                    healthScript.health = healthScript.health - shrapnelDamage * vsShipDamageMultiplier;
                }
                else
                {
                    //Reduce its health by shrapnelDamage----
                    healthScript.health = healthScript.health - shrapnelDamage;

                    //EMP effect if EMP munitions are equipped----
                    if (controller.empMunitionsEquipped.VerifyBool(controller.pEmpMunitionsEquipped))
                    {
                        healthScript.empStart = true;
                    }
                }
            }
        }
    }

    private void Start()
    {
        //Applying upgrades to player fighter----
        //Incremental upgrades----
        shrapnelDamage = shrapnelDamage * Mathf.Pow(1.05f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));

        //Tech upgrades----
        if (controller.collisionAnalysisOwned.VerifyBool(controller.pCollisionAnalysisOwned))
        {
            shrapnelDamage = shrapnelDamage * 1.15f;
        }
        if (controller.armorPiercingRoundsOwned.VerifyBool(controller.pArmorPiercingRoundsOwned))
        {
            shrapnelDamage = shrapnelDamage * 1.15f;
        }
        if (controller.flightOptimizationOwned.VerifyBool(controller.pFlightOptimizationOwned))
        {
            shrapnelDamage = shrapnelDamage * 1.05f;
        }
        if (controller.aresClassWeaponSystemOwned.VerifyBool(controller.pAresClassWeaponSystemOwned))
        {
            shrapnelDamage = shrapnelDamage * 1.1f;
        }
        //Dumping damage----
        dumpShrapnelDamage = shrapnelDamage;
    }
}
