using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshUtilityMenu : MonoBehaviour
{
    public static RefreshUtilityMenu refresher;

    //missile bays----
    public GameObject mbr;
    public GameObject mby;
    public GameObject mbb;
    public GameObject mbg;
    //flak cannon----
    public GameObject fcr;
    public GameObject fcy;
    public GameObject fcb;
    public GameObject fcg;
    //railgun----
    public GameObject rr;
    public GameObject ry;
    public GameObject rb;
    public GameObject rg;
    //cannon turret----
    public GameObject ctr;
    public GameObject cty;
    public GameObject ctb;
    public GameObject ctg;
    //rocket pods----
    public GameObject rpr;
    public GameObject rpy;
    public GameObject rpb;
    public GameObject rpg;
    //carpet bombs----
    public GameObject cbr;
    public GameObject cby;
    public GameObject cbb;
    public GameObject cbg;
    //missile jammers----
    public GameObject mjr;
    public GameObject mjy;
    public GameObject mjb;
    public GameObject mjg;
    //support drone----
    public GameObject edr;
    public GameObject edy;
    public GameObject edb;
    public GameObject edg;
    //emp munitions----
    public GameObject emr;
    public GameObject emy;
    public GameObject emb;
    public GameObject emg;

    //GameControl----
    GameControl controller;


    private void Awake()
    {
        controller = GameControl.control;

        refresher = this;
        Refresh();
    }

    public void Refresh()
    {
        //Setting buttons active/inactive based upon gamecontrol variables----
        //Missile Bays----
        if (controller.missileBaysLocked.VerifyBool(controller.pMissileBaysLocked))
        {
            mbr.SetActive(true);
            mby.SetActive(false);
            mbb.SetActive(false);
            mbg.SetActive(false);
        }
        else if (controller.missileBaysEquipped.VerifyBool(controller.pMissileBaysEquipped))
        {
            mbr.SetActive(false);
            mby.SetActive(false);
            mbb.SetActive(false);
            mbg.SetActive(true);
        }
        else if (controller.missileBaysOwned.VerifyBool(controller.pMissileBaysOwned))
        {
            mbr.SetActive(false);
            mby.SetActive(false);
            mbb.SetActive(true);
            mbg.SetActive(false);
        }
        else
        {
            mbr.SetActive(false);
            mby.SetActive(true);
            mbb.SetActive(false);
            mbg.SetActive(false);
        }
        //Cannon Turret----
        if (controller.cannonTurretLocked.VerifyBool(controller.pCannonTurretLocked))
        {
            ctr.SetActive(true);
            cty.SetActive(false);
            ctb.SetActive(false);
            ctg.SetActive(false);
        }
        else if (controller.cannonTurretEquipped.VerifyBool(controller.pCannonTurretEquipped))
        {
            ctr.SetActive(false);
            cty.SetActive(false);
            ctb.SetActive(false);
            ctg.SetActive(true);
        }
        else if (controller.cannonTurretOwned.VerifyBool(controller.pCannonTurretOwned))
        {
            ctr.SetActive(false);
            cty.SetActive(false);
            ctb.SetActive(true);
            ctg.SetActive(false);
        }
        else
        {
            ctr.SetActive(false);
            cty.SetActive(true);
            ctb.SetActive(false);
            ctg.SetActive(false);
        }
        //Missile Jammers----
        if (controller.missileJammerLocked.VerifyBool(controller.pMissileJammerLocked))
        {
            mjr.SetActive(true);
            mjy.SetActive(false);
            mjb.SetActive(false);
            mjg.SetActive(false);
        }
        else if (controller.missileJammerEquipped.VerifyBool(controller.pMissileJammerEquipped))
        {
            mjr.SetActive(false);
            mjy.SetActive(false);
            mjb.SetActive(false);
            mjg.SetActive(true);
        }
        else if (controller.missileJammerOwned.VerifyBool(controller.pMissileJammerOwned))
        {
            mjr.SetActive(false);
            mjy.SetActive(false);
            mjb.SetActive(true);
            mjg.SetActive(false);
        }
        else
        {
            mjr.SetActive(false);
            mjy.SetActive(true);
            mjb.SetActive(false);
            mjg.SetActive(false);
        }
        //Flak Cannon----
        if (controller.flakCannonLocked.VerifyBool(controller.pFlakCannonLocked))
        {
            fcr.SetActive(true);
            fcy.SetActive(false);
            fcb.SetActive(false);
            fcg.SetActive(false);
        }
        else if (controller.flakCannonEquipped.VerifyBool(controller.pFlakCannonEquipped))
        {
            fcr.SetActive(false);
            fcy.SetActive(false);
            fcb.SetActive(false);
            fcg.SetActive(true);
        }
        else if (controller.flakCannonOwned.VerifyBool(controller.pFlakCannonOwned))
        {
            fcr.SetActive(false);
            fcy.SetActive(false);
            fcb.SetActive(true);
            fcg.SetActive(false);
        }
        else
        {
            fcr.SetActive(false);
            fcy.SetActive(true);
            fcb.SetActive(false);
            fcg.SetActive(false);
        }
        //Rocket Pods----
        if (controller.rocketPodsLocked.VerifyBool(controller.pRocketPodsLocked))
        {
            rpr.SetActive(true);
            rpy.SetActive(false);
            rpb.SetActive(false);
            rpg.SetActive(false);
        }
        else if (controller.rocketPodsEquipped.VerifyBool(controller.pRocketPodsEquipped))
        {
            rpr.SetActive(false);
            rpy.SetActive(false);
            rpb.SetActive(false);
            rpg.SetActive(true);
        }
        else if (controller.rocketPodsOwned.VerifyBool(controller.pRocketPodsOwned))
        {
            rpr.SetActive(false);
            rpy.SetActive(false);
            rpb.SetActive(true);
            rpg.SetActive(false);
        }
        else
        {
            rpr.SetActive(false);
            rpy.SetActive(true);
            rpb.SetActive(false);
            rpg.SetActive(false);
        }
        //Escort Drone----
        if (controller.escortDroneLocked.VerifyBool(controller.pEscortDroneLocked))
        {
            edr.SetActive(true);
            edy.SetActive(false);
            edb.SetActive(false);
            edg.SetActive(false);
        }
        else if (controller.escortDroneEquipped.VerifyBool(controller.pEscortDroneEquipped))
        {
            edr.SetActive(false);
            edy.SetActive(false);
            edb.SetActive(false);
            edg.SetActive(true);
        }
        else if (controller.escortDroneOwned.VerifyBool(controller.pEscortDroneOwned))
        {
            edr.SetActive(false);
            edy.SetActive(false);
            edb.SetActive(true);
            edg.SetActive(false);
        }
        else
        {
            edr.SetActive(false);
            edy.SetActive(true);
            edb.SetActive(false);
            edg.SetActive(false);
        }
        //railgun----
        if (controller.railgunCannonLocked.VerifyBool(controller.pRailgunCannonLocked))
        {
            rr.SetActive(true);
            ry.SetActive(false);
            rb.SetActive(false);
            rg.SetActive(false);
        }
        else if (controller.railgunCannonEquipped.VerifyBool(controller.pRailgunCannonEquipped))
        {
            rr.SetActive(false);
            ry.SetActive(false);
            rb.SetActive(false);
            rg.SetActive(true);
        }
        else if (controller.railgunCannonOwned.VerifyBool(controller.pRailgunCannonOwned))
        {
            rr.SetActive(false);
            ry.SetActive(false);
            rb.SetActive(true);
            rg.SetActive(false);
        }
        else
        {
            rr.SetActive(false);
            ry.SetActive(true);
            rb.SetActive(false);
            rg.SetActive(false);
        }
        //carpet bombs----
        if (controller.carpetBombingLocked.VerifyBool(controller.pCarpetBombingLocked))
        {
            cbr.SetActive(true);
            cby.SetActive(false);
            cbb.SetActive(false);
            cbg.SetActive(false);
        }
        else if (controller.carpetBombingEquipped.VerifyBool(controller.pCarpetBombingEquipped))
        {
            cbr.SetActive(false);
            cby.SetActive(false);
            cbb.SetActive(false);
            cbg.SetActive(true);
        }
        else if (controller.carpetBombingOwned.VerifyBool(controller.pCarpetBombingOwned))
        {
            cbr.SetActive(false);
            cby.SetActive(false);
            cbb.SetActive(true);
            cbg.SetActive(false);
        }
        else
        {
            cbr.SetActive(false);
            cby.SetActive(true);
            cbb.SetActive(false);
            cbg.SetActive(false);
        }
        //EMP Munitions----
        if (controller.empMunitionsLocked.VerifyBool(controller.pEmpMunitionsLocked))
        {
            emr.SetActive(true);
            emy.SetActive(false);
            emb.SetActive(false);
            emg.SetActive(false);
        }
        else if (controller.empMunitionsEquipped.VerifyBool(controller.pEmpMunitionsEquipped))
        {
            emr.SetActive(false);
            emy.SetActive(false);
            emb.SetActive(false);
            emg.SetActive(true);
        }
        else if (controller.empMunitionsOwned.VerifyBool(controller.pEmpMunitionsOwned))
        {
            emr.SetActive(false);
            emy.SetActive(false);
            emb.SetActive(true);
            emg.SetActive(false);
        }
        else
        {
            emr.SetActive(false);
            emy.SetActive(true);
            emb.SetActive(false);
            emg.SetActive(false);
        }
    }
}
