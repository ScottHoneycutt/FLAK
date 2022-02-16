using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUtility : MonoBehaviour
{
    //Menus----
    public GameObject confirmCanvas;
    public GameObject insufficientFundsMenu;
    public GameObject utilitiesMenu;
    public GameObject purchaseSuccessfulMenu;
    public GameObject equipCanvas;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    //Purchase methods----
    public void BuyMissileBays()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 500)
        {
            //Buy upgrade----
            controller.pSupplies -= 500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pMissileBaysOwned = true;
            controller.missileBaysOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyCannonTurret()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 500)
        {
            //Buy upgrade----
            controller.pSupplies -= 500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pCannonTurretOwned = true;
            controller.cannonTurretOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyMissileJammers()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 500)
        {
            //Buy upgrade----
            controller.pSupplies -= 500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pMissileJammerOwned = true;
            controller.missileJammerOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyFlakCannon()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1000)
        {
            //Buy upgrade----
            controller.pSupplies -= 1000;
            controller.supplies.Reset(controller.pSupplies);

            controller.pFlakCannonOwned = true;
            controller.flakCannonOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyRocketPods()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1000)
        {
            //Buy upgrade----
            controller.pSupplies -= 1000;
            controller.supplies.Reset(controller.pSupplies);

            controller.pRocketPodsOwned = true;
            controller.rocketPodsOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyEscortDrone()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1000)
        {
            //Buy upgrade----
            controller.pSupplies -= 1000;
            controller.supplies.Reset(controller.pSupplies);

            controller.pEscortDroneOwned = true;
            controller.escortDroneOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyRailgun()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1500)
        {
            //Buy upgrade----
            controller.pSupplies -= 1500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pRailgunCannonOwned = true;
            controller.railgunCannonOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyCarpetBombs()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1500)
        {
            //Buy upgrade----
            controller.pSupplies -= 1500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pCarpetBombingOwned = true;
            controller.carpetBombingOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    public void BuyEmpMunitions()
    {
        //If sufficient funds are available----
        if (controller.supplies.VerifyInt(controller.pSupplies) >= 1500)
        {
            //Buy upgrade----
            controller.pSupplies -= 1500;
            controller.supplies.Reset(controller.pSupplies);

            controller.pEmpMunitionsOwned = true;
            controller.empMunitionsOwned.Reset(true);

            //Updating menus----
            confirmCanvas.SetActive(false);
            purchaseSuccessfulMenu.SetActive(true);
            RefreshUtilityMenu.refresher.Refresh();
            //Saving----
            controller.Save();
        }
        else
        {
            confirmCanvas.SetActive(false);
            insufficientFundsMenu.SetActive(true);
        }
    }

    //Equip methods----
    //Slot 1----
    public void EquipMissileBays()
    {
        controller.pMissileBaysEquipped = true;
        controller.pFlakCannonEquipped = false;
        controller.pRailgunCannonEquipped = false;

        controller.missileBaysEquipped.Reset(true);
        controller.flakCannonEquipped.Reset(false);
        controller.railgunCannonEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipFlakCannon()
    {
        controller.pMissileBaysEquipped = false;
        controller.pFlakCannonEquipped = true;
        controller.pRailgunCannonEquipped = false;

        controller.missileBaysEquipped.Reset(false);
        controller.flakCannonEquipped.Reset(true);
        controller.railgunCannonEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipRailgunCannon()
    {
        controller.pMissileBaysEquipped = false;
        controller.pFlakCannonEquipped = false;
        controller.pRailgunCannonEquipped = true;

        controller.missileBaysEquipped.Reset(false);
        controller.flakCannonEquipped.Reset(false);
        controller.railgunCannonEquipped.Reset(true);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    //Slot 2----
    public void EquipCannonTurret()
    {
        controller.pCannonTurretEquipped = true;
        controller.pRocketPodsEquipped = false;
        controller.pCarpetBombingEquipped = false;

        controller.cannonTurretEquipped.Reset(true);
        controller.rocketPodsEquipped.Reset(false);
        controller.carpetBombingEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipRocketPods()
    {
        controller.pCannonTurretEquipped = false;
        controller.pRocketPodsEquipped = true;
        controller.pCarpetBombingEquipped = false;

        controller.cannonTurretEquipped.Reset(false);
        controller.rocketPodsEquipped.Reset(true);
        controller.carpetBombingEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipCarpetBombs()
    {
        controller.pCannonTurretEquipped = false;
        controller.pRocketPodsEquipped = false;
        controller.pCarpetBombingEquipped = true;

        controller.cannonTurretEquipped.Reset(false);
        controller.rocketPodsEquipped.Reset(false);
        controller.carpetBombingEquipped.Reset(true);
        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    //Slot 3----
    public void EquipMissileJammers()
    {
        controller.pMissileJammerEquipped = true;
        controller.pEscortDroneEquipped = false;
        controller.pEmpMunitionsEquipped = false;

        controller.missileJammerEquipped.Reset(true);
        controller.escortDroneEquipped.Reset(false);
        controller.empMunitionsEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipEscortDrone()
    {
        controller.pMissileJammerEquipped = false;
        controller.pEscortDroneEquipped = true;
        controller.pEmpMunitionsEquipped = false;

        controller.missileJammerEquipped.Reset(false);
        controller.escortDroneEquipped.Reset(true);
        controller.empMunitionsEquipped.Reset(false);

        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void EquipEmpMunitions()
    {
        controller.pMissileJammerEquipped = false;
        controller.pEscortDroneEquipped = false;
        controller.pEmpMunitionsEquipped = true;

        controller.missileJammerEquipped.Reset(false);
        controller.escortDroneEquipped.Reset(false);
        controller.empMunitionsEquipped.Reset(true);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }

    //Unequip methods----
    public void UnequipMissileBays()
    {
        controller.pMissileBaysEquipped = false;
        controller.missileBaysEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipMissileJammers()
    {
        controller.pMissileJammerEquipped = false;
        controller.missileJammerEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipCannonTurret()
    {
        controller.pCannonTurretEquipped = false;
        controller.cannonTurretEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipFlakCannon()
    {
        controller.pFlakCannonEquipped = false;
        controller.flakCannonEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipRocketPods()
    {
        controller.pRocketPodsEquipped = false;
        controller.rocketPodsEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipEscortDrone()
    {
        controller.pEscortDroneEquipped = false;
        controller.escortDroneEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipRailgun()
    {
        controller.pRailgunCannonEquipped = false;
        controller.railgunCannonEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipCarpetBombs()
    {
        controller.pCarpetBombingEquipped = false;
        controller.carpetBombingEquipped.Reset(false);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
    public void UnequipEmpMunitions()
    {
        controller.pEmpMunitionsEquipped = false;
        controller.empMunitionsEquipped.Reset(true);

        //Updating menus----
        equipCanvas.SetActive(false);
        utilitiesMenu.SetActive(true);
        RefreshUtilityMenu.refresher.Refresh();
        //Saving----
        controller.Save();
    }
}
