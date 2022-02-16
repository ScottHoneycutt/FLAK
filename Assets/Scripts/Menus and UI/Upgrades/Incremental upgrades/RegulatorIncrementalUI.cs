using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulatorIncrementalUI : MonoBehaviour
{
    //Cannons upgrade indicators----
    public GameObject cannonsUi1;
    public GameObject cannonsUi2;
    public GameObject cannonsUi3;
    public GameObject cannonsUi4;
    public GameObject cannonsUi5;

    //Missiles upgrade indicators----
    public GameObject missilesUi1;
    public GameObject missilesUi2;
    public GameObject missilesUi3;
    public GameObject missilesUi4;
    public GameObject missilesUi5;

    //Armor upgrade incators----
    public GameObject armorUi1;
    public GameObject armorUi2;
    public GameObject armorUi3;
    public GameObject armorUi4;
    public GameObject armorUi5;

    //Aerodynamics upgrade indicators----
    public GameObject aerodynamicsUi1;
    public GameObject aerodynamicsUi2;
    public GameObject aerodynamicsUi3;
    public GameObject aerodynamicsUi4;
    public GameObject aerodynamicsUi5;

    //B Cannons upgrade indicators----
    public GameObject cannonsUi1B;
    public GameObject cannonsUi2B;
    public GameObject cannonsUi3B;
    public GameObject cannonsUi4B;
    public GameObject cannonsUi5B;

    //B Missiles upgrade indicators----
    public GameObject missilesUi1B;
    public GameObject missilesUi2B;
    public GameObject missilesUi3B;
    public GameObject missilesUi4B;
    public GameObject missilesUi5B;

    //B Armor upgrade incators----
    public GameObject armorUi1B;
    public GameObject armorUi2B;
    public GameObject armorUi3B;
    public GameObject armorUi4B;
    public GameObject armorUi5B;

    //B Aerodynamics upgrade indicators----
    public GameObject aerodynamicsUi1B;
    public GameObject aerodynamicsUi2B;
    public GameObject aerodynamicsUi3B;
    public GameObject aerodynamicsUi4B;
    public GameObject aerodynamicsUi5B;

    private GameControl controller;

    //Awake method----
    void Awake()
    {
        controller = GameControl.control;
        //Swapping out Cannon upgrade indicators----
        if (controller.pCannonLevel > 0)
        {
            cannonsUi1.SetActive(true);
            cannonsUi1B.SetActive(false);
        }
        if (controller.pCannonLevel > 1)
        {
            cannonsUi2.SetActive(true);
            cannonsUi2B.SetActive(false);
        }
        if (controller.pCannonLevel > 2)
        {
            cannonsUi3.SetActive(true);
            cannonsUi3B.SetActive(false);
        }
        if (controller.pCannonLevel > 3)
        {
            cannonsUi4.SetActive(true);
            cannonsUi4B.SetActive(false);
        }
        if (controller.pCannonLevel > 4)
        {
            cannonsUi5.SetActive(true);
            cannonsUi5B.SetActive(false);
        }

        //Swapping out missile indicators----
        if (controller.pMissileLevel > 0)
        {
            missilesUi1.SetActive(true);
            missilesUi1B.SetActive(false);
        }
        if (controller.pMissileLevel > 1)
        {
            missilesUi2.SetActive(true);
            missilesUi2B.SetActive(false);
        }
        if (controller.pMissileLevel > 2)
        {
            missilesUi3.SetActive(true);
            missilesUi3B.SetActive(false);
        }
        if (controller.pMissileLevel > 3)
        {
            missilesUi4.SetActive(true);
            missilesUi4B.SetActive(false);
        }
        if (controller.pMissileLevel > 4)
        {
            missilesUi5.SetActive(true);
            missilesUi5B.SetActive(false);
        }

        //Swapping out armor indicators----
        if (controller.pArmorLevel > 0)
        {
            armorUi1.SetActive(true);
            armorUi1B.SetActive(false);
        }
        if (controller.pArmorLevel > 1)
        {
            armorUi2.SetActive(true);
            armorUi2B.SetActive(false);
        }
        if (controller.pArmorLevel > 2)
        {
            armorUi3.SetActive(true);
            armorUi3B.SetActive(false);
        }
        if (controller.pArmorLevel > 3)
        {
            armorUi4.SetActive(true);
            armorUi4B.SetActive(false);
        }
        if (controller.pArmorLevel > 4)
        {
            armorUi5.SetActive(true);
            armorUi5B.SetActive(false);
        }

        //Swapping out aerodynamics indicators----
        if (controller.pAerodynamicsLevel > 0)
        {
            aerodynamicsUi1.SetActive(true);
            aerodynamicsUi1B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 1)
        {
            aerodynamicsUi2.SetActive(true);
            aerodynamicsUi2B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 2)
        {
            aerodynamicsUi3.SetActive(true);
            aerodynamicsUi3B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 3)
        {
            aerodynamicsUi4.SetActive(true);
            aerodynamicsUi4B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 4)
        {
            aerodynamicsUi5.SetActive(true);
            aerodynamicsUi5B.SetActive(false);
        }
    }




    //Refresh for when upgrades are made----
    public void refreshUpgradeUi()
    {
        //Swapping out Cannon upgrade indicators----
        if (controller.pCannonLevel > 0)
        {
            cannonsUi1.SetActive(true);
            cannonsUi1B.SetActive(false);
        }
        if (controller.pCannonLevel > 1)
        {
            cannonsUi2.SetActive(true);
            cannonsUi2B.SetActive(false);
        }
        if (controller.pCannonLevel > 2)
        {
            cannonsUi3.SetActive(true);
            cannonsUi3B.SetActive(false);
        }
        if (controller.pCannonLevel > 3)
        {
            cannonsUi4.SetActive(true);
            cannonsUi4B.SetActive(false);
        }
        if (controller.pCannonLevel > 4)
        {
            cannonsUi5.SetActive(true);
            cannonsUi5B.SetActive(false);
        }

        //Swapping out missile indicators----
        if (controller.pMissileLevel > 0)
        {
            missilesUi1.SetActive(true);
            missilesUi1B.SetActive(false);
        }
        if (controller.pMissileLevel > 1)
        {
            missilesUi2.SetActive(true);
            missilesUi2B.SetActive(false);
        }
        if (controller.pMissileLevel > 2)
        {
            missilesUi3.SetActive(true);
            missilesUi3B.SetActive(false);
        }
        if (controller.pMissileLevel > 3)
        {
            missilesUi4.SetActive(true);
            missilesUi4B.SetActive(false);
        }
        if (controller.pMissileLevel > 4)
        {
            missilesUi5.SetActive(true);
            missilesUi5B.SetActive(false);
        }

        //Swapping out armor indicators----
        if (controller.pArmorLevel > 0)
        {
            armorUi1.SetActive(true);
            armorUi1B.SetActive(false);
        }
        if (controller.pArmorLevel > 1)
        {
            armorUi2.SetActive(true);
            armorUi2B.SetActive(false);
        }
        if (controller.pArmorLevel > 2)
        {
            armorUi3.SetActive(true);
            armorUi3B.SetActive(false);
        }
        if (controller.pArmorLevel > 3)
        {
            armorUi4.SetActive(true);
            armorUi4B.SetActive(false);
        }
        if (controller.pArmorLevel > 4)
        {
            armorUi5.SetActive(true);
            armorUi5B.SetActive(false);
        }

        //Swapping out aerodynamics indicators----
        if (controller.pAerodynamicsLevel > 0)
        {
            aerodynamicsUi1.SetActive(true);
            aerodynamicsUi1B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 1)
        {
            aerodynamicsUi2.SetActive(true);
            aerodynamicsUi2B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 2)
        {
            aerodynamicsUi3.SetActive(true);
            aerodynamicsUi3B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 3)
        {
            aerodynamicsUi4.SetActive(true);
            aerodynamicsUi4B.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 4)
        {
            aerodynamicsUi5.SetActive(true);
            aerodynamicsUi5B.SetActive(false);
        }
    }
}

