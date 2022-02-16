using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonImpact : MonoBehaviour {

    //Public variables----
    public float sCannonDamage = 5;
    public ParticleSystem cannonFire;
    public bool playerWeapon = false;

    //Sound files----
    public float volume = .5f;
    public AudioClip impact1;
    public AudioClip impact2;
    public AudioClip impact3;
    public AudioClip impact4;
    private bool filled = false;
    private AudioClip[] audioList = new AudioClip[4];
    private AudioSource source;

    //Anti-tampering measures----
    private SecretFloat cannonDamage;
    //Refined damage stats----
    private SecretFloat magneticShieldingDamage;
    private SecretFloat afterburnDamage;
    private SecretFloat fullDamageReduction;
    //Dump reserve variables----
    private SecretFloat dumpCannonDamage;

    //List of particle collision events----
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        //Anti-tampering measures----
        cannonDamage = new SecretFloat(sCannonDamage);

        //Refined damage stats----
        magneticShieldingDamage = new SecretFloat();
        afterburnDamage = new SecretFloat();
        fullDamageReduction = new SecretFloat();

        //Dump variable----
        dumpCannonDamage = new SecretFloat();
    }

    // Use this for initialization
    void Start()
    {
        //Applying upgrades to player fighter----
        if (playerWeapon)
        {
            //Incremental upgrades----
            sCannonDamage = sCannonDamage * Mathf.Pow(1.05f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));

            //Tech upgrades----
            if (controller.collisionAnalysisOwned.VerifyBool(controller.pCollisionAnalysisOwned))
            {
                sCannonDamage = sCannonDamage * 1.15f;
            }
            if (controller.armorPiercingRoundsOwned.VerifyBool(controller.pArmorPiercingRoundsOwned))
            {
                sCannonDamage = sCannonDamage * 1.15f;
            }
            if (controller.flightOptimizationOwned.VerifyBool(controller.pFlightOptimizationOwned))
            {
                sCannonDamage = sCannonDamage * 1.05f;
            }
            if (controller.aresClassWeaponSystemOwned.VerifyBool(controller.pAresClassWeaponSystemOwned))
            {
                sCannonDamage = sCannonDamage * 1.1f;
            }

            //Dumping reserve cannon damage----
            dumpCannonDamage.Reset(sCannonDamage);

            //Saving new values for anti-tampering measures---
            cannonDamage.Reset(sCannonDamage);

            //Checking for tampering (placing checks here to optimize game so that checks are not applied every time the player is hit) (or so that it's not constantly called in the coroutine)----
            controller.magneticShieldingOwned.VerifyBool(controller.pMagneticShieldingOwned);
            controller.escapeArtistOwned.VerifyBool(controller.pEscapeArtistOwned);
            controller.closeInSupportOwned.VerifyBool(controller.pCloseInSupportOwned);
            controller.kineticRecyclingOwned.VerifyBool(controller.pKineticRecyclingOwned);
            controller.empMunitionsEquipped.VerifyBool(controller.pEmpMunitionsEquipped);

            StartCoroutine("UpdateAircraftTechStatus");
        }

        //Tech upgrades damage reductions for the player fighter----
        magneticShieldingDamage.Reset(sCannonDamage * .7f);
        afterburnDamage.Reset(sCannonDamage * .7f);
        fullDamageReduction.Reset(sCannonDamage * .7f * .7f);
    }

    //Void OnParticleCollision----
    void OnParticleCollision(GameObject impactTarget)
    {
        //Getting rotation quaternion for prefab instantiation----
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        //Collision count and changing the list of particle collision events----
        int collisionCount = ParticlePhysicsExtensions.GetCollisionEvents(cannonFire, impactTarget, collisionEvents);


        //Running through the list of collision events----
        for(int i = 0; i < collisionCount; i++)
        {
            //Impact explosion instantiation----
            Vector3 impactLocation = collisionEvents[i].intersection;
            ObjectPoolManager.manager.SpawnFromPool("Cannon Impacts", impactLocation, rotation);
            //If impacted object has health----
            if (impactTarget.GetComponent<UnitHP>() != null)
            {
                UnitHP healthScript = impactTarget.GetComponent<UnitHP>();
                //If it hits the player----
                if (impactTarget.name == "Player Fighter")
                {
                    if (!filled)
                    {
                        filled = true;
                        //filling audiolist with audio----
                        audioList[0] = impact1;
                        audioList[1] = impact2;
                        audioList[2] = impact3;
                        audioList[3] = impact4;
                        //Referencing main camera for audiosource----
                        source = Camera.main.GetComponent<AudioSource>();
                    }

                    //Play random impact sound if bullet hits Player Fighter----
                    int selector = Random.Range(0, 4);
                    source.PlayOneShot(audioList[selector], volume);

                    //Damage reduction to Player Fighter if Magnetic Shielding is owned and escape artist afterburn is active----
                    if (controller.pMagneticShieldingOwned && controller.pEscapeArtistOwned && controller.afterburnActive)
                    {
                        healthScript.health = healthScript.health - fullDamageReduction.GetFloat();
                    }
                    //Just magnetic shielding----
                    else if (controller.pMagneticShieldingOwned)
                    {
                        healthScript.health = healthScript.health - magneticShieldingDamage.GetFloat();
                    }
                    //Just escape artist afterburn----
                    else if (controller.pEscapeArtistOwned && controller.afterburnActive)
                    {
                        healthScript.health = healthScript.health - afterburnDamage.GetFloat();
                    }
                    //Neither----
                    else
                    {
                        //Standard cannon damage----
                        healthScript.health = healthScript.health - cannonDamage.GetFloat();
                    }
                }
                //If target hit was not the player----
                else
                {
                    //Standard cannon damage----
                    healthScript.health = healthScript.health - cannonDamage.GetFloat();

                    //EMP effect if it is a player weapon and if EMP munitions are equipped----
                    if (playerWeapon && controller.pEmpMunitionsEquipped)
                    {
                        healthScript.empStart = true;
                    }
                }
            }
        }
    }

    //Updating Aircraft status+tech upgrades----
    IEnumerable UpdateAircraftTechStatus()
    {
        while (true)
        {
            //Kinetic Recycling and close-in support----
            if(controller.pCloseInSupportOwned && controller.slowdownActive && controller.pKineticRecyclingOwned && controller.recentlyDamaged)
            {
                cannonDamage.Reset(dumpCannonDamage.GetFloat() * 1.32f); //(1.1 * 1.2 = 1.32)----
            }
            //close-in support----
            else if (controller.pCloseInSupportOwned && controller.slowdownActive)
            {
                cannonDamage.Reset(dumpCannonDamage.GetFloat() * 1.1f);
            }
            //Kinetic Recycling----
            else if (controller.pKineticRecyclingOwned && controller.recentlyDamaged)
            {
                cannonDamage.Reset(dumpCannonDamage.GetFloat() * 1.2f);
            }
            //Neither----
            else
            {
                cannonDamage = dumpCannonDamage;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
