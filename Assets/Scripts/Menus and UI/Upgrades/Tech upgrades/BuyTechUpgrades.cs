using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTechUpgrades : MonoBehaviour
{
    public int pCreditsCost;
    public int pSuppliesCost;

    //Anti-tampering measures----
    private ProtectedInt creditsCost;
    private ProtectedInt suppliesCost;

    public GameObject insufficientFundsMenu;
    public GameObject techUpgradesPanel;

    //GameControl----
    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        //Anti-tampering measures----
        creditsCost = new ProtectedInt(pCreditsCost);
        suppliesCost = new ProtectedInt(pSuppliesCost);
    }

    //Extended Aerofoils----
    public void BuyEAF()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pExtendedAerofoilsOwned = true;
            controller.extendedAerofoilsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Ballistic armor----
    public void BuyBA()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pBallisticArmorOwned = true;
            controller.ballisticArmorOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Tungsten Barrels----
    public void BuyTB()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pTungstenBarrelsOwned = true;
            controller.tungstenBarrelsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Copper Heat Sinks----
    public void BuyCHS()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pCopperHeatSinksOwned = true;
            controller.copperHeatSinksOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Heavy Payload----
    public void BuyHP()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pHeavyPayloadOwned = true;
            controller.heavyPayloadOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Titanium Skeleton----
    public void BuyTS()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pTitaniumSkeletonOwned = true;
            controller.titaniumSkeletonOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Secondary Hydraulics----
    public void BuySH()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pSecondaryHydraulicsOwned = true;
            controller.secondaryHydraulicsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Persistence Hunters----
    public void BuyPH()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pPersistenceHuntersOwned = true;
            controller.persistenceHuntersOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Collision Analysis----
    public void BuyCA()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pCollisionAnalysisOwned = true;
            controller.collisionAnalysisOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Flight Optimization----
    public void BuyFO()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pFlightOptimizationOwned = true;
            controller.flightOptimizationOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Armor-Piercing Rounds----
    public void BuyAPR()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pArmorPiercingRoundsOwned = true;
            controller.armorPiercingRoundsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Mark IV Feeder----
    public void BuyM4F()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pMarkIVFeederOwned = true;
            controller.markIVFeederOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Mark III Autoloaders----
    public void BuyM3AL()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pMarkIIIAutoloadersOwned = true;
            controller.markIIIAutoloadersOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Thermal Optics----
    public void BuyTO()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pThermalOpticsOwned = true;
            controller.thermalOpticsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Nano-Repairs----
    public void BuyNR()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pNanoRepairsOwned = true;
            controller.nanoRepairsOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Chaff----
    public void BuyC()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pChaffOwned = true;
            controller.chaffOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Heat Treatment----
    public void BuyHT()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pHeatTreatmentOwned = true;
            controller.heatTreatmentOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Kinetic Recycling----
    public void BuyKR()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pKineticRecyclingOwned = true;
            controller.kineticRecyclingOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Evasive Maneuvers----
    public void BuyEM()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pEvasiveManeuversOwned = true;
            controller.evasiveManeuversOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Special Delivery----
    public void BuySD()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pSpecialDeliveryOwned = true;
            controller.specialDeliveryOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Close-In Support----
    public void BuyCIS()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pCloseInSupportOwned = true;
            controller.closeInSupportOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Desperation Module----
    public void BuyDM()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pDesperationModuleOwned = true;
            controller.desperationModuleOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Thrust Vectoring----
    public void BuyTV()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pThrustVectoringOwned = true;
            controller.thrustVectoringOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Magnetic Shielding----
    public void BuyMS()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pMagneticShieldingOwned = true;
            controller.magneticShieldingOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Escape Artist----
    public void BuyEA()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pEscapeArtistOwned = true;
            controller.escapeArtistOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Ares-Class Weapon System----
    public void BuyACWS()
    {
        //If there are sufficient funds (check for security violations)----
        if (controller.credits.VerifyInt(controller.pCredits) >= creditsCost.VerifyInt(pCreditsCost) && controller.supplies.VerifyInt(controller.pSupplies) >= suppliesCost.VerifyInt(pSuppliesCost))
        {
            //Take funds away and upgrade----
            controller.pCredits -= pCreditsCost;
            controller.pSupplies -= pSuppliesCost;
            controller.credits.Reset(controller.pCredits);
            controller.supplies.Reset(controller.pSupplies);

            controller.pAresClassWeaponSystemOwned = true;
            controller.aresClassWeaponSystemOwned.Reset(true);

            //Telling the TechUpgradeManager to refresh----
            TechUpgradeManager.techManager.Refresh();

            //Saving----
            controller.Save();

            //Returning to tech upgrades panel----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            techUpgradesPanel.SetActive(true);
        }
        else
        {
            //Turning off active confirmation menu and turning on insufficientFundsMenu----
            this.gameObject.transform.parent.gameObject.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }
}
