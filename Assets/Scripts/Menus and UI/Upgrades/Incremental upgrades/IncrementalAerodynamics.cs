using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementalAerodynamics : MonoBehaviour
{
    public GameObject confirmMenu;
    public GameObject upgradeMenu;
    public GameObject insufficientFundsMenu;

    public int pCreditsCost = 100;

    //Anti-tampering measures----
    ProtectedInt creditsCost;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        //Anti-tampering measures----
        creditsCost = new ProtectedInt(pCreditsCost);
    }

    public void Upgrade()
    {
        //If there are sufficient funds----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost))
        {
            //Take funds away and upgrade once----
            controller.pCredits -= pCreditsCost;
            controller.credits.Reset(controller.pCredits);

            //Verifying that the upgrade level checks out before upgrading----
            controller.aerodynamicsLevel.VerifyInt(controller.pAerodynamicsLevel);

            controller.pAerodynamicsLevel++;
            controller.aerodynamicsLevel.Reset(controller.pAerodynamicsLevel);

            //Save----
            controller.Save();

            //Return to upgrades----
            confirmMenu.SetActive(false);
            upgradeMenu.SetActive(true);
        }
        else
        {
            confirmMenu.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }
}
