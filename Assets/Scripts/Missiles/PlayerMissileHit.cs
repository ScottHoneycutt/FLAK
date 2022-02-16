using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerMissileHit : MonoBehaviour
{
    ///Public variables----
    public float sDamage = 25;

    //Private variables---- 
    private bool expended = false;

    //GameControl----
    GameControl controller;

    //Anti-tampering measures----
    private SecretFloat damage;
    //Upgrade stuff----
    private SecretFloat dumpDamage;

    private void Awake()
    {
        controller = GameControl.control;

        damage = new SecretFloat(sDamage);
        dumpDamage = new SecretFloat(0);
    }

    private void Start()
    {
        //Upgrade modifiers----
        sDamage = sDamage * Mathf.Pow(1.07f, controller.missileLevel.VerifyInt(controller.pMissileLevel));

        //Tech upgrades----
        if (controller.heavyPayloadOwned.VerifyBool(controller.pHeavyPayloadOwned))
        {
            sDamage = sDamage * 1.2f;
        }

        //Dumping variables----
        dumpDamage.Reset(damage.GetFloat());
    }

    private void OnEnable()
    {
        expended = false;

        //Verifying GameControl save values----
        controller.closeInSupportOwned.VerifyBool(controller.pCloseInSupportOwned);
        controller.closeInSupportOwned.VerifyBool(controller.pCloseInSupportOwned);
    }

    private void OnTriggerEnter(Collider objectCollision)
    {
        //If the missile collides with an object on the "Enemy"/"Terrain" layer and it hasn't already collided...----
        if ((objectCollision.gameObject.layer == 8 || objectCollision.gameObject.layer == 18 || objectCollision.gameObject.layer == 19) && !expended)
        {
            //If it has a health stat----
            if (objectCollision.gameObject.GetComponent<UnitHP>())
            {
                UnitHP healthScript = objectCollision.GetComponent<UnitHP>();

                //Checking damage modifiers----
                //Both----
                if (controller.recentlyDamaged && controller.pKineticRecyclingOwned && controller.slowdownActive && controller.pCloseInSupportOwned)
                {
                    damage.Reset(dumpDamage.GetFloat() * 1.2f);
                    damage.Reset(damage.GetFloat() * 1.1f);
                }
                //Kinetic----
                else if (controller.recentlyDamaged && controller.pKineticRecyclingOwned)
                {
                    damage.Reset(dumpDamage.GetFloat() * 1.2f);
                }
                //Close-in----
                else if (controller.slowdownActive && controller.pCloseInSupportOwned)
                {
                    damage.Reset(damage.GetFloat() * 1.1f);
                }
                //Neither----
                else
                {
                    damage.Reset(dumpDamage.GetFloat());
                }

                //Deal damage----
                healthScript.health -= damage.GetFloat();

                //EMP effect if EMP munitions are equipped----
                if (controller.empMunitionsEquipped.VerifyBool(controller.pEmpMunitionsEquipped))
                {
                    healthScript.empStart = true;
                }
            }
            //Die----
            this.GetComponent<UnitHP>().health = 0;
            expended = true;
        }
    }
}
