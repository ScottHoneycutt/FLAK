using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class GameControl : MonoBehaviour {

    //control object----
    public static GameControl control;

    //Profile variables----
    public int pCredits;
    public int pSupplies;

    //Incremental upgrades----
    public int pCannonLevel;
    public int pMissileLevel;
    public int pArmorLevel;
    public int pAerodynamicsLevel;

    //Tech Upgrades----
    public bool pExtendedAerofoilsOwned;
    public bool pBallisticArmorOwned;
    public bool pTungstenBarrelsOwned;
    public bool pCopperHeatSinksOwned;
    public bool pHeavyPayloadOwned;
    public bool pPersistenceHuntersOwned;
    public bool pTitaniumSkeletonOwned;
    public bool pCollisionAnalysisOwned;
    public bool pSecondaryHydraulicsOwned;
    public bool pFlightOptimizationOwned;
    public bool pThermalOpticsOwned;
    public bool pHeatTreatmentOwned;
    public bool pChaffOwned;
    public bool pEvasiveManeuversOwned;
    public bool pArmorPiercingRoundsOwned;
    public bool pMarkIIIAutoloadersOwned;
    public bool pKineticRecyclingOwned;
    public bool pSpecialDeliveryOwned;
    public bool pThrustVectoringOwned;
    public bool pMarkIVFeederOwned;
    public bool pDesperationModuleOwned;
    public bool pCloseInSupportOwned;
    public bool pNanoRepairsOwned;
    public bool pMagneticShieldingOwned;
    public bool pEscapeArtistOwned;
    public bool pAresClassWeaponSystemOwned;

    //Utilities----
    public bool pRocketPodsLocked;
    public bool pMissileBaysLocked;
    public bool pCarpetBombingLocked;
    public bool pFlakCannonLocked;
    public bool pRailgunCannonLocked;
    public bool pMissileJammerLocked;
    public bool pEscortDroneLocked;
    public bool pCannonTurretLocked;
    public bool pEmpMunitionsLocked;

    public bool pRocketPodsOwned;
    public bool pMissileBaysOwned;
    public bool pCarpetBombingOwned;
    public bool pFlakCannonOwned;
    public bool pRailgunCannonOwned;
    public bool pMissileJammerOwned;
    public bool pEscortDroneOwned;
    public bool pCannonTurretOwned;
    public bool pEmpMunitionsOwned;

    public bool pRocketPodsEquipped;
    public bool pMissileBaysEquipped;
    public bool pCarpetBombingEquipped;
    public bool pFlakCannonEquipped;
    public bool pRailgunCannonEquipped;
    public bool pMissileJammerEquipped;
    public bool pEscortDroneEquipped;
    public bool pCannonTurretEquipped;
    public bool pEmpMunitionsEquipped;

    //Options----
    public float dcmSens;
    public float dcmRange;
    public float volume;
    public bool fullScreen = true;

    //Levels completed/not completed variables----
    public bool tutorialCompleted;
    public bool pLevel1Completed;
    public bool pLevel2Completed;
    public bool pLevel3Completed;
    public bool pLevel4Completed;
    public bool pLevel5Completed;
    public bool pLevel6Completed;
    public bool pLevel7Completed;

    //Stuff that is not saved-------------------------------------------------------------------------------------------------------
    //Other aircraft status stuff----
    public bool afterburnActive;
    public bool slowdownActive;
    public bool recentlyDamaged;
    public bool criticalHealth;

    //Camera shake variables----
    public bool lightDamageBool;
    public bool mediumDamageBool;
    public bool heavyDamageBool;
    public bool railgunShootingBool;
    public bool cannonShootingBool;
    public bool missileShootBool;

    //Anti-tampering measures----------------------------------------------------------------------------------------------------------------------
    //Profile variables----
    public ProtectedInt credits;
    public ProtectedInt supplies;

    //Incremental upgrades----
    public ProtectedInt cannonLevel;
    public ProtectedInt missileLevel;
    public ProtectedInt armorLevel;
    public ProtectedInt aerodynamicsLevel;

    //Tech Upgrades----
    public ProtectedBool extendedAerofoilsOwned;
    public ProtectedBool ballisticArmorOwned;
    public ProtectedBool tungstenBarrelsOwned;
    public ProtectedBool copperHeatSinksOwned;
    public ProtectedBool heavyPayloadOwned;
    public ProtectedBool persistenceHuntersOwned;
    public ProtectedBool titaniumSkeletonOwned;
    public ProtectedBool collisionAnalysisOwned;
    public ProtectedBool secondaryHydraulicsOwned;
    public ProtectedBool flightOptimizationOwned;
    public ProtectedBool thermalOpticsOwned;
    public ProtectedBool heatTreatmentOwned;
    public ProtectedBool chaffOwned;
    public ProtectedBool evasiveManeuversOwned;
    public ProtectedBool armorPiercingRoundsOwned;
    public ProtectedBool markIIIAutoloadersOwned;
    public ProtectedBool kineticRecyclingOwned;
    public ProtectedBool specialDeliveryOwned;
    public ProtectedBool thrustVectoringOwned;
    public ProtectedBool markIVFeederOwned;
    public ProtectedBool desperationModuleOwned;
    public ProtectedBool closeInSupportOwned;
    public ProtectedBool nanoRepairsOwned;
    public ProtectedBool magneticShieldingOwned;
    public ProtectedBool escapeArtistOwned;
    public ProtectedBool aresClassWeaponSystemOwned;

    //Utilities----
    public ProtectedBool rocketPodsLocked;
    public ProtectedBool missileBaysLocked;
    public ProtectedBool carpetBombingLocked;
    public ProtectedBool flakCannonLocked;
    public ProtectedBool railgunCannonLocked;
    public ProtectedBool missileJammerLocked;
    public ProtectedBool escortDroneLocked;
    public ProtectedBool cannonTurretLocked;
    public ProtectedBool empMunitionsLocked;

    public ProtectedBool rocketPodsOwned;
    public ProtectedBool missileBaysOwned;
    public ProtectedBool carpetBombingOwned;
    public ProtectedBool flakCannonOwned;
    public ProtectedBool railgunCannonOwned;
    public ProtectedBool missileJammerOwned;
    public ProtectedBool escortDroneOwned;
    public ProtectedBool cannonTurretOwned;
    public ProtectedBool empMunitionsOwned;

    public ProtectedBool rocketPodsEquipped;
    public ProtectedBool missileBaysEquipped;
    public ProtectedBool carpetBombingEquipped;
    public ProtectedBool flakCannonEquipped;
    public ProtectedBool railgunCannonEquipped;
    public ProtectedBool missileJammerEquipped;
    public ProtectedBool escortDroneEquipped;
    public ProtectedBool cannonTurretEquipped;
    public ProtectedBool empMunitionsEquipped;

    //Levels completed/not completed variables----
    public ProtectedBool level1Completed;
    public ProtectedBool level2Completed;
    public ProtectedBool level3Completed;
    public ProtectedBool level4Completed;
    public ProtectedBool level5Completed;
    public ProtectedBool level6Completed;
    public ProtectedBool level7Completed;

    private string UiText;

    //Prior to Start of scene----
    private void Awake()
    {
        //Existing control takes priority over new control, new control created if there isn't one already----
        if (control == null)
        {
            DontDestroyOnLoad(this.gameObject);
            control = this;
            //Caching child UI text encryption key----
            UiText = transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        }
        else if (control != this)
        {
            Destroy(this.gameObject);
        }

        //Initializing anti-tampering measures-------------------------------------------------------------------------------------------------------------------------------------------
        credits = new ProtectedInt(pCredits);
        supplies = new ProtectedInt(pSupplies);

        cannonLevel = new ProtectedInt(pCannonLevel);
        missileLevel = new ProtectedInt(pMissileLevel); 
        armorLevel = new ProtectedInt(pArmorLevel);
        aerodynamicsLevel = new ProtectedInt(pAerodynamicsLevel);

        extendedAerofoilsOwned = new ProtectedBool(pExtendedAerofoilsOwned);
        ballisticArmorOwned = new ProtectedBool(pBallisticArmorOwned);
        tungstenBarrelsOwned = new ProtectedBool(pTungstenBarrelsOwned);
        copperHeatSinksOwned = new ProtectedBool(pCopperHeatSinksOwned);
        heavyPayloadOwned = new ProtectedBool(pHeavyPayloadOwned);
        persistenceHuntersOwned = new ProtectedBool(pPersistenceHuntersOwned);
        titaniumSkeletonOwned = new ProtectedBool(pTitaniumSkeletonOwned);
        collisionAnalysisOwned = new ProtectedBool(pCollisionAnalysisOwned);
        secondaryHydraulicsOwned = new ProtectedBool(pSecondaryHydraulicsOwned);
        flightOptimizationOwned = new ProtectedBool(pFlightOptimizationOwned);
        thermalOpticsOwned = new ProtectedBool(pThermalOpticsOwned);
        heatTreatmentOwned = new ProtectedBool(pHeatTreatmentOwned);
        chaffOwned = new ProtectedBool(pChaffOwned);
        evasiveManeuversOwned = new ProtectedBool(pEvasiveManeuversOwned);
        armorPiercingRoundsOwned = new ProtectedBool(pArmorPiercingRoundsOwned);
        markIIIAutoloadersOwned = new ProtectedBool(pMarkIIIAutoloadersOwned);
        kineticRecyclingOwned = new ProtectedBool(pKineticRecyclingOwned);
        specialDeliveryOwned = new ProtectedBool(pSpecialDeliveryOwned);
        thrustVectoringOwned = new ProtectedBool(pThrustVectoringOwned);
        markIVFeederOwned = new ProtectedBool(pMarkIVFeederOwned);
        desperationModuleOwned = new ProtectedBool(pDesperationModuleOwned);
        closeInSupportOwned = new ProtectedBool(pCloseInSupportOwned);
        nanoRepairsOwned = new ProtectedBool(pNanoRepairsOwned);
        magneticShieldingOwned = new ProtectedBool(pMagneticShieldingOwned);
        escapeArtistOwned = new ProtectedBool(pEscapeArtistOwned);
        aresClassWeaponSystemOwned = new ProtectedBool(pAresClassWeaponSystemOwned);

        rocketPodsLocked = new ProtectedBool(pRocketPodsLocked);
        missileBaysLocked = new ProtectedBool(pMissileBaysLocked);
        carpetBombingLocked = new ProtectedBool(pCarpetBombingLocked);
        flakCannonLocked = new ProtectedBool(pFlakCannonLocked);
        railgunCannonLocked = new ProtectedBool(pRailgunCannonLocked);
        missileJammerLocked = new ProtectedBool(pMissileJammerLocked);
        escortDroneLocked = new ProtectedBool(pEscortDroneLocked);
        cannonTurretLocked = new ProtectedBool(pCannonTurretLocked);
        empMunitionsLocked = new ProtectedBool(pEmpMunitionsLocked);

        rocketPodsOwned = new ProtectedBool(pRocketPodsOwned);
        missileBaysOwned = new ProtectedBool(pMissileBaysOwned);
        carpetBombingOwned = new ProtectedBool(pCarpetBombingOwned);
        flakCannonOwned = new ProtectedBool(pFlakCannonOwned);
        railgunCannonOwned = new ProtectedBool(pRailgunCannonOwned);
        missileJammerOwned = new ProtectedBool(pMissileJammerOwned);
        escortDroneOwned = new ProtectedBool(pEscortDroneOwned);
        cannonTurretOwned = new ProtectedBool(pCannonTurretOwned);
        empMunitionsOwned = new ProtectedBool(pEmpMunitionsOwned);

        rocketPodsEquipped = new ProtectedBool(pRocketPodsEquipped);
        missileBaysEquipped = new ProtectedBool(pMissileBaysEquipped);
        carpetBombingEquipped = new ProtectedBool(pCarpetBombingEquipped);
        flakCannonEquipped = new ProtectedBool(pFlakCannonEquipped);
        railgunCannonEquipped = new ProtectedBool(pRailgunCannonEquipped);
        missileJammerEquipped = new ProtectedBool(pMissileJammerEquipped);
        escortDroneEquipped = new ProtectedBool(pEscortDroneEquipped);
        cannonTurretEquipped = new ProtectedBool(pCannonTurretEquipped);
        empMunitionsEquipped = new ProtectedBool(pEmpMunitionsEquipped);


        level1Completed = new ProtectedBool(pLevel1Completed);
        level2Completed = new ProtectedBool(pLevel2Completed);
        level3Completed = new ProtectedBool(pLevel3Completed);
        level4Completed = new ProtectedBool(pLevel4Completed);
        level5Completed = new ProtectedBool(pLevel5Completed);
        level6Completed = new ProtectedBool(pLevel6Completed);
        level7Completed = new ProtectedBool(pLevel7Completed);

        //Load Game----
        Load();
    }

    //Override system----
    public void Override()
    {
        credits.Reset(pCredits);
        supplies.Reset(pSupplies);

        cannonLevel.Reset(pCannonLevel);
        missileLevel.Reset(pMissileLevel);
        armorLevel.Reset(pArmorLevel);
        aerodynamicsLevel.Reset(pAerodynamicsLevel);

        extendedAerofoilsOwned.Reset(pExtendedAerofoilsOwned);
        ballisticArmorOwned.Reset(pBallisticArmorOwned);
        tungstenBarrelsOwned.Reset(pTungstenBarrelsOwned);
        copperHeatSinksOwned.Reset(pCopperHeatSinksOwned);
        heavyPayloadOwned.Reset(pHeavyPayloadOwned);
        persistenceHuntersOwned.Reset(pPersistenceHuntersOwned);
        titaniumSkeletonOwned.Reset(pTitaniumSkeletonOwned);
        collisionAnalysisOwned.Reset(pCollisionAnalysisOwned);
        secondaryHydraulicsOwned.Reset(pSecondaryHydraulicsOwned);
        flightOptimizationOwned.Reset(pFlightOptimizationOwned);
        thermalOpticsOwned.Reset(pThermalOpticsOwned);
        heatTreatmentOwned.Reset(pHeatTreatmentOwned);
        chaffOwned.Reset(pChaffOwned);
        evasiveManeuversOwned.Reset(pEvasiveManeuversOwned);
        armorPiercingRoundsOwned.Reset(pArmorPiercingRoundsOwned);
        markIIIAutoloadersOwned.Reset(pMarkIIIAutoloadersOwned);
        kineticRecyclingOwned.Reset(pKineticRecyclingOwned);
        specialDeliveryOwned.Reset(pSpecialDeliveryOwned);
        thrustVectoringOwned.Reset(pThrustVectoringOwned);
        markIVFeederOwned.Reset(pMarkIVFeederOwned);
        desperationModuleOwned.Reset(pDesperationModuleOwned);
        closeInSupportOwned.Reset(pCloseInSupportOwned);
        nanoRepairsOwned.Reset(pNanoRepairsOwned);
        magneticShieldingOwned.Reset(pMagneticShieldingOwned);
        escapeArtistOwned.Reset(pEscapeArtistOwned);
        aresClassWeaponSystemOwned.Reset(pAresClassWeaponSystemOwned);

        rocketPodsLocked.Reset(pRocketPodsLocked);
        missileBaysLocked.Reset(pMissileBaysLocked);
        carpetBombingLocked.Reset(pCarpetBombingLocked);
        flakCannonLocked.Reset(pFlakCannonLocked);
        railgunCannonLocked.Reset(pRailgunCannonLocked);
        missileJammerLocked.Reset(pMissileJammerLocked);
        escortDroneLocked.Reset(pEscortDroneLocked);
        cannonTurretLocked.Reset(pCannonTurretLocked);
        empMunitionsLocked.Reset(pEmpMunitionsLocked);

        rocketPodsOwned.Reset(pRocketPodsOwned);
        missileBaysOwned.Reset(pMissileBaysOwned);
        carpetBombingOwned.Reset(pCarpetBombingOwned);
        flakCannonOwned.Reset(pFlakCannonOwned);
        railgunCannonOwned.Reset(pRailgunCannonOwned);
        missileJammerOwned.Reset(pMissileJammerOwned);
        escortDroneOwned.Reset(pEscortDroneOwned);
        cannonTurretOwned.Reset(pCannonTurretOwned);
        empMunitionsOwned.Reset(pEmpMunitionsOwned);

        rocketPodsEquipped.Reset(pRocketPodsEquipped);
        missileBaysEquipped.Reset(pMissileBaysEquipped);
        carpetBombingEquipped.Reset(pCarpetBombingEquipped);
        flakCannonEquipped.Reset(pFlakCannonEquipped);
        railgunCannonEquipped.Reset(pRailgunCannonEquipped);
        missileJammerEquipped.Reset(pMissileJammerEquipped);
        escortDroneEquipped.Reset(pEscortDroneEquipped);
        cannonTurretEquipped.Reset(pCannonTurretEquipped);
        empMunitionsEquipped.Reset(pEmpMunitionsEquipped);

        level1Completed.Reset(pLevel1Completed);
        level2Completed.Reset(pLevel2Completed);
        level3Completed.Reset(pLevel3Completed);
        level4Completed.Reset(pLevel4Completed);
        level5Completed.Reset(pLevel5Completed);
        level6Completed.Reset(pLevel6Completed);
        level7Completed.Reset(pLevel7Completed);
        Debug.Log("done.");
    }

    //Saving data----
    public void Save()
    {
        //ENCRYPTION CLASS INITIALIZATION AND SALT CREATION----
        RijndaelManaged encryptor = new RijndaelManaged();
        encryptor.GenerateIV();
        //Assigning key----
        encryptor.Key = Convert.FromBase64String(UiText);

        //Removing padding----
        encryptor.Padding = PaddingMode.None;

        //SALT CLASS INITIALIZATION----
        Playersalt saltData = new Playersalt();
        //Setting salt value to be saved----
        saltData.salt = encryptor.IV;

        //Salt file location, removing old salt----
        string saltLocation = Path.Combine(Application.persistentDataPath, "playerSalt");
        if (File.Exists(saltLocation))
        {
            File.Delete(saltLocation);
        }

        //Creating salt string to be saved----
        string saltFileString = Convert.ToBase64String(saltData.salt);
        //Writing salt to file----
        File.WriteAllText(saltLocation, saltFileString);


        //Initializing data storage class----
        Playerdata data = new Playerdata();

        //ALL DATA TRANSFERS HAPPEN HERE----
        //economy----
        data.credits = credits.VerifyInt(pCredits);
        data.supplies = supplies.VerifyInt(pSupplies);

        //incremental upgrades----
        data.cannonLevel = cannonLevel.VerifyInt(pCannonLevel);
        data.missileLevel = missileLevel.VerifyInt(pMissileLevel);
        data.armorLevel = armorLevel.VerifyInt(pArmorLevel);
        data.aerodynamicsLevel = aerodynamicsLevel.VerifyInt(pAerodynamicsLevel);

        //tech upgrades----
        data.extendedAerofoilsOwned = extendedAerofoilsOwned.VerifyBool(pExtendedAerofoilsOwned);
        data.ballisticArmorOwned = ballisticArmorOwned.VerifyBool(pBallisticArmorOwned);
        data.tungstenBarrelsOwned = tungstenBarrelsOwned.VerifyBool(pTungstenBarrelsOwned); 
        data.copperHeatSinksOwned = copperHeatSinksOwned.VerifyBool(pCopperHeatSinksOwned); 
        data.heavyPayloadOwned = heavyPayloadOwned.VerifyBool(pHeavyPayloadOwned); 
        data.persistenceHuntersOwned = persistenceHuntersOwned.VerifyBool(pPersistenceHuntersOwned); 
        data.titaniumSkeletonOwned = titaniumSkeletonOwned.VerifyBool(pTitaniumSkeletonOwned); 
        data.collisionAnalysisOwned = collisionAnalysisOwned.VerifyBool(pCollisionAnalysisOwned);
        data.secondaryHydraulicsOwned = secondaryHydraulicsOwned.VerifyBool(pSecondaryHydraulicsOwned);
        data.flightOptimizationOwned = flightOptimizationOwned.VerifyBool(pFlightOptimizationOwned);
        data.thermalOpticsOwned = thermalOpticsOwned.VerifyBool(pThermalOpticsOwned);
        data.heatTreatmentOwned = heatTreatmentOwned.VerifyBool(pHeatTreatmentOwned);
        data.chaffOwned = chaffOwned.VerifyBool(pChaffOwned);
        data.evasiveManeuversOwned = evasiveManeuversOwned.VerifyBool(pEvasiveManeuversOwned);
        data.armorPiercingRoundsOwned = armorPiercingRoundsOwned.VerifyBool(pArmorPiercingRoundsOwned);
        data.markIIIAutoloadersOwned = markIIIAutoloadersOwned.VerifyBool(pMarkIIIAutoloadersOwned);
        data.kineticRecyclingOwned = kineticRecyclingOwned.VerifyBool(pKineticRecyclingOwned);
        data.specialDeliveryOwned = specialDeliveryOwned.VerifyBool(pSpecialDeliveryOwned);
        data.thrustVectoringOwned = thrustVectoringOwned.VerifyBool(pThrustVectoringOwned);
        data.markIVFeederOwned = markIVFeederOwned.VerifyBool(pMarkIVFeederOwned);
        data.desperationModuleOwned = desperationModuleOwned.VerifyBool(pDesperationModuleOwned);
        data.closeInSupportOwned = closeInSupportOwned.VerifyBool(pCloseInSupportOwned);
        data.nanoRepairsOwned = nanoRepairsOwned.VerifyBool(pNanoRepairsOwned);
        data.magneticShieldingOwned = magneticShieldingOwned.VerifyBool(pMagneticShieldingOwned);
        data.escapeArtistOwned = escapeArtistOwned.VerifyBool(pEscapeArtistOwned);
        data.aresClassWeaponSystemOwned = aresClassWeaponSystemOwned.VerifyBool(pAresClassWeaponSystemOwned);

        //Utilities----
        data.rocketPodsLocked = rocketPodsLocked.VerifyBool(pRocketPodsLocked);
        data.missileBaysLocked = missileBaysLocked.VerifyBool(pMissileBaysLocked);
        data.carpetBombingLocked = carpetBombingLocked.VerifyBool(pCarpetBombingLocked);
        data.flakCannonLocked = flakCannonLocked.VerifyBool(pFlakCannonLocked);
        data.railgunCannonLocked = railgunCannonLocked.VerifyBool(pRailgunCannonLocked);
        data.missileJammerLocked = missileJammerLocked.VerifyBool(pMissileJammerLocked);
        data.escortDroneLocked = escortDroneLocked.VerifyBool(pEscortDroneLocked);
        data.cannonTurretLocked = cannonTurretLocked.VerifyBool(pCannonTurretLocked);
        data.empMunitionsLocked = empMunitionsLocked.VerifyBool(pEmpMunitionsLocked);

        data.rocketPodsOwned = rocketPodsOwned.VerifyBool(pRocketPodsOwned);
        data.missileBaysOwned = missileBaysOwned.VerifyBool(pMissileBaysOwned);
        data.carpetBombingOwned = carpetBombingOwned.VerifyBool(pCarpetBombingOwned);
        data.flakCannonOwned = flakCannonOwned.VerifyBool(pFlakCannonOwned);
        data.railgunCannonOwned = railgunCannonOwned.VerifyBool(pRailgunCannonOwned);
        data.missileJammerOwned = missileJammerOwned.VerifyBool(pMissileJammerOwned);
        data.escortDroneOwned = escortDroneOwned.VerifyBool(pEscortDroneOwned);
        data.cannonTurretOwned = cannonTurretOwned.VerifyBool(pCannonTurretOwned);
        data.empMunitionsOwned = empMunitionsOwned.VerifyBool(pEmpMunitionsOwned);

        data.rocketPodsEquipped = rocketPodsEquipped.VerifyBool(pRocketPodsEquipped);
        data.missileBaysEquipped = missileBaysEquipped.VerifyBool(pMissileBaysEquipped);
        data.carpetBombingEquipped = carpetBombingEquipped.VerifyBool(pCarpetBombingEquipped);
        data.flakCannonEquipped = flakCannonEquipped.VerifyBool(pFlakCannonEquipped);
        data.railgunCannonEquipped = railgunCannonEquipped.VerifyBool(pRailgunCannonEquipped);
        data.missileJammerEquipped = missileJammerEquipped.VerifyBool(pMissileJammerEquipped);
        data.escortDroneEquipped = escortDroneEquipped.VerifyBool(pEscortDroneEquipped);
        data.cannonTurretEquipped = cannonTurretEquipped.VerifyBool(pCannonTurretEquipped);
        data.empMunitionsEquipped = empMunitionsEquipped.VerifyBool(pEmpMunitionsEquipped);

        //options----
        data.dcmSens = dcmSens;
        data.dcmRange = dcmRange;
        data.volume = volume;
        data.fullScreen = fullScreen;

        //levels----
        data.tutorialCompleted = tutorialCompleted;
        data.level1Completed = level1Completed.VerifyBool(pLevel1Completed);
        data.level2Completed = level2Completed.VerifyBool(pLevel2Completed);
        data.level3Completed = level3Completed.VerifyBool(pLevel3Completed);
        data.level4Completed = level4Completed.VerifyBool(pLevel4Completed);
        data.level5Completed = level5Completed.VerifyBool(pLevel5Completed);
        data.level6Completed = level6Completed.VerifyBool(pLevel6Completed);
        data.level7Completed = level7Completed.VerifyBool(pLevel7Completed);

        //Turn all data into a single string----
        String dataString = JsonUtility.ToJson(data);        

        //Creating file name----
        String fileName = Path.Combine(Application.persistentDataPath, "playerInfo");

        //Removing old obsolete file----
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }


        //Encrypting dataString-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Declaring finalDataString----
        string finalDataString = "";

        //Loop to break the text into 1024 byte blocks----
        int blocks = dataString.Length/1024 +1;
        while (blocks > 0)
        {
            blocks--;
            //Breaking up dataString into full blocks of 1024 bytes----
            if (dataString.Length > 1024)
            {
                //Encryption tool----
                ICryptoTransform encryptTool = encryptor.CreateEncryptor(encryptor.Key, encryptor.IV);

                //Breaking first 1024 bytes off of dataString----
                string dataStringChunk = dataString.Substring(0, 1025);
                dataString = dataString.Substring(1024, dataString.Length - 1024);

                //Actual encrypting and writing to file----
                //Streams for encryption---- 
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptTool, CryptoStreamMode.Write);
                StreamWriter swEncrypt = new StreamWriter(csEncrypt);
                //Write all data to the stream----
                swEncrypt.Write(dataStringChunk);
                //Converting it to a string again and giving it to dataString----

                dataStringChunk = Convert.ToBase64String(msEncrypt.ToArray());

                //Add encrypted portion to the whole----
                finalDataString = string.Concat(finalDataString, dataStringChunk);
            }
            //Last chunk of string needs to be padded-----
            else if (dataString.Length > 0)
            {
                //Encryption tool----
                ICryptoTransform encryptTool = encryptor.CreateEncryptor(encryptor.Key, encryptor.IV);

                //Padding dataString----
                int paddingNeeded = 1025 - dataString.Length;
                String padding = new String('&', paddingNeeded);
                dataString = string.Concat(dataString, padding);


                //Actual encrypting and writing to file----
                //Streams for encryption---- 
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptTool, CryptoStreamMode.Write);
                StreamWriter swEncrypt = new StreamWriter(csEncrypt);
                //Write all data to the stream----
                swEncrypt.Write(dataString);
                //Converting it to a string again and giving it to dataString----
                dataString = Convert.ToBase64String(msEncrypt.ToArray());


                //Add encrypted portion to the whole----
                finalDataString = string.Concat(finalDataString, dataString);
            }
        }

        //Write new file at fileName location that contains dataString----
        File.WriteAllText(fileName, finalDataString);
    }

    //Loading data (opening game)----
    public void Load()
    {
        //INITIALIZING ENCRYPTOR----
        RijndaelManaged encryptor = new RijndaelManaged();
        UiText = transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        encryptor.Key = Convert.FromBase64String(UiText);

        //Removing padding----
        encryptor.Padding = PaddingMode.None;

        //INITIALIZING SALT----
        Playersalt saltData = new Playersalt();

        //Finding salt----
        string saltLocation = Path.Combine(Application.persistentDataPath, "playerSalt");
        if (File.Exists(saltLocation))
        {
            //Pulling the Playersalt (saltData) out of the file at saltLocation----
            String saltString = File.ReadAllText(saltLocation);
            saltData.salt = Convert.FromBase64String(saltString);

            //Setting encryptor salt to the saved salt----
            encryptor.IV = saltData.salt;
        }


        //File path----
        String fileName = Path.Combine(Application.persistentDataPath, "playerInfo");

        //If the files exist----
        if (File.Exists(fileName) && File.Exists(saltLocation))
        {
            //Getting string of all encrypted data----
            String dataString = File.ReadAllText(fileName);
            //Declaring finalDataString----
            string finalDataString = "";

            //Decyrpting dataString-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            //Loop to decrypt all chunks of encrypted text----
            while (dataString.Contains("=="))
            {
                //Decryption tool----
                ICryptoTransform decryptTool = encryptor.CreateDecryptor(encryptor.Key, encryptor.IV);

                //Breaking ciphertext off of dataString (using the "==" as breakpoints----
                int breakIndex = dataString.IndexOf("==") + 2;
                string dataStringChunk = dataString.Substring(0, breakIndex);
                dataString = dataString.Substring(breakIndex, dataString.Length - breakIndex);

                //Actual decryption----
                //Streams for decryption---- 
                MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(dataStringChunk));
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptTool, CryptoStreamMode.Read);
                StreamReader srDecrypt = new StreamReader(csDecrypt);
                //Read the decrypted bytes from the decrypting stream and place them in a string----
                dataStringChunk = srDecrypt.ReadToEnd();

                finalDataString = string.Concat(finalDataString, dataStringChunk);
            }

            //Removing the padding used for encryption purposes (&)----
            finalDataString = finalDataString.Replace("&", "");;

            //Turning string back into useable data----
            Playerdata data = JsonUtility.FromJson<Playerdata>(finalDataString);

            //ALL DATA TRANSFERS HAPPEN HERE----
            pCredits = data.credits;
            pSupplies = data.supplies;

            pCannonLevel = data.cannonLevel;
            pMissileLevel = data.missileLevel;
            pArmorLevel = data.armorLevel;
            pAerodynamicsLevel = data.aerodynamicsLevel;

            pExtendedAerofoilsOwned = data.extendedAerofoilsOwned;
            pBallisticArmorOwned = data.ballisticArmorOwned;
            pTungstenBarrelsOwned = data.tungstenBarrelsOwned;
            pCopperHeatSinksOwned = data.copperHeatSinksOwned;
            pHeavyPayloadOwned = data.heavyPayloadOwned;
            pPersistenceHuntersOwned = data.persistenceHuntersOwned;
            pTitaniumSkeletonOwned = data.titaniumSkeletonOwned;
            pCollisionAnalysisOwned = data.collisionAnalysisOwned;
            pSecondaryHydraulicsOwned = data.secondaryHydraulicsOwned;
            pFlightOptimizationOwned = data.flightOptimizationOwned;
            pThermalOpticsOwned = data.thermalOpticsOwned;
            pHeatTreatmentOwned = data.heatTreatmentOwned;
            pChaffOwned = data.chaffOwned;
            pEvasiveManeuversOwned = data.evasiveManeuversOwned;
            pArmorPiercingRoundsOwned = data.armorPiercingRoundsOwned;
            pMarkIIIAutoloadersOwned = data.markIIIAutoloadersOwned;
            pKineticRecyclingOwned = data.kineticRecyclingOwned;
            pSpecialDeliveryOwned = data.specialDeliveryOwned;
            pThrustVectoringOwned = data.thrustVectoringOwned;
            pMarkIVFeederOwned = data.markIVFeederOwned;
            pDesperationModuleOwned = data.desperationModuleOwned;
            pCloseInSupportOwned = data.closeInSupportOwned;
            pNanoRepairsOwned = data.nanoRepairsOwned;
            pMagneticShieldingOwned = data.magneticShieldingOwned;
            pEscapeArtistOwned = data.escapeArtistOwned;
            pAresClassWeaponSystemOwned = data.aresClassWeaponSystemOwned;

            pRocketPodsLocked = data.rocketPodsLocked;
            pMissileBaysLocked = data.missileBaysLocked;
            pCarpetBombingLocked = data.carpetBombingLocked;
            pFlakCannonLocked = data.flakCannonLocked;
            pRailgunCannonLocked = data.railgunCannonLocked;
            pMissileJammerLocked = data.missileJammerLocked;
            pEscortDroneLocked = data.escortDroneLocked;
            pCannonTurretLocked = data.cannonTurretLocked;
            pEmpMunitionsLocked = data.empMunitionsLocked;

            pRocketPodsOwned = data.rocketPodsOwned;
            pMissileBaysOwned = data.missileBaysOwned;
            pCarpetBombingOwned = data.carpetBombingOwned;
            pFlakCannonOwned = data.flakCannonOwned;
            pRailgunCannonOwned = data.railgunCannonOwned;
            pMissileJammerOwned = data.missileJammerOwned;
            pEscortDroneOwned = data.escortDroneOwned;
            pCannonTurretOwned = data.cannonTurretOwned;
            pEmpMunitionsOwned = data.empMunitionsOwned;

            pRocketPodsEquipped = data.rocketPodsEquipped;
            pMissileBaysEquipped = data.missileBaysEquipped;
            pCarpetBombingEquipped = data.carpetBombingEquipped;
            pFlakCannonEquipped = data.flakCannonEquipped;
            pRailgunCannonEquipped = data.railgunCannonEquipped;
            pMissileJammerEquipped = data.missileJammerEquipped;
            pEscortDroneEquipped = data.escortDroneEquipped;
            pCannonTurretEquipped = data.cannonTurretEquipped;
            pEmpMunitionsEquipped = data.empMunitionsEquipped;

            dcmSens = data.dcmSens;
            dcmRange = data.dcmRange;
            volume = data.volume;
            fullScreen = data.fullScreen;

            tutorialCompleted = data.tutorialCompleted;
            pLevel1Completed = data.level1Completed;
            pLevel2Completed = data.level2Completed;
            pLevel3Completed = data.level3Completed;
            pLevel4Completed = data.level4Completed;
            pLevel5Completed = data.level5Completed;
            pLevel6Completed = data.level6Completed;
            pLevel7Completed = data.level7Completed;

            //Resetting anti-tampering measures----
            credits.Reset(pCredits);
            supplies.Reset(pSupplies);

            cannonLevel.Reset(pCannonLevel);
            missileLevel.Reset(pMissileLevel);
            armorLevel.Reset(pArmorLevel);
            aerodynamicsLevel.Reset(pAerodynamicsLevel);

            extendedAerofoilsOwned.Reset(pExtendedAerofoilsOwned);
            ballisticArmorOwned.Reset(pBallisticArmorOwned);
            tungstenBarrelsOwned.Reset(pTungstenBarrelsOwned);
            copperHeatSinksOwned.Reset(pCopperHeatSinksOwned);
            heavyPayloadOwned.Reset(pHeavyPayloadOwned);
            persistenceHuntersOwned.Reset(pPersistenceHuntersOwned);
            titaniumSkeletonOwned.Reset(pTitaniumSkeletonOwned);
            collisionAnalysisOwned.Reset(pCollisionAnalysisOwned);
            secondaryHydraulicsOwned.Reset(pSecondaryHydraulicsOwned);
            flightOptimizationOwned.Reset(pFlightOptimizationOwned);
            thermalOpticsOwned.Reset(pThermalOpticsOwned);
            heatTreatmentOwned.Reset(pHeatTreatmentOwned);
            chaffOwned.Reset(pChaffOwned);
            evasiveManeuversOwned.Reset(pEvasiveManeuversOwned);
            armorPiercingRoundsOwned.Reset(pArmorPiercingRoundsOwned);
            markIIIAutoloadersOwned.Reset(pMarkIIIAutoloadersOwned);
            kineticRecyclingOwned.Reset(pKineticRecyclingOwned);
            specialDeliveryOwned.Reset(pSpecialDeliveryOwned);
            thrustVectoringOwned.Reset(pThrustVectoringOwned);
            markIVFeederOwned.Reset(pMarkIVFeederOwned);
            desperationModuleOwned.Reset(pDesperationModuleOwned);
            closeInSupportOwned.Reset(pCloseInSupportOwned);
            nanoRepairsOwned.Reset(pNanoRepairsOwned);
            magneticShieldingOwned.Reset(pMagneticShieldingOwned);
            escapeArtistOwned.Reset(pEscapeArtistOwned);
            aresClassWeaponSystemOwned.Reset(pAresClassWeaponSystemOwned);


            rocketPodsLocked.Reset(pRocketPodsLocked);
            missileBaysLocked.Reset(pMissileBaysLocked);
            carpetBombingLocked.Reset(pCarpetBombingLocked);
            flakCannonLocked.Reset(pFlakCannonLocked);
            railgunCannonLocked.Reset(pRailgunCannonLocked);
            missileJammerLocked.Reset(pMissileJammerLocked);
            escortDroneLocked.Reset(pEscortDroneLocked);
            cannonTurretLocked.Reset(pCannonTurretLocked);
            empMunitionsLocked.Reset(pEmpMunitionsLocked);

            rocketPodsOwned.Reset(pRocketPodsOwned);
            missileBaysOwned.Reset(pMissileBaysOwned);
            carpetBombingOwned.Reset(pCarpetBombingOwned);
            flakCannonOwned.Reset(pFlakCannonOwned);
            railgunCannonOwned.Reset(pRailgunCannonOwned);
            missileJammerOwned.Reset(pMissileJammerOwned);
            escortDroneOwned.Reset(pEscortDroneOwned);
            cannonTurretOwned.Reset(pCannonTurretOwned);
            empMunitionsOwned.Reset(pEmpMunitionsOwned);

            rocketPodsEquipped.Reset(pRocketPodsEquipped);
            missileBaysEquipped.Reset(pMissileBaysEquipped);
            carpetBombingEquipped.Reset(pCarpetBombingEquipped);
            flakCannonEquipped.Reset(pFlakCannonEquipped);
            railgunCannonEquipped.Reset(pRailgunCannonEquipped);
            missileJammerEquipped.Reset(pMissileJammerEquipped);
            escortDroneEquipped.Reset(pEscortDroneEquipped);
            cannonTurretEquipped.Reset(pCannonTurretEquipped);
            empMunitionsEquipped.Reset(pEmpMunitionsEquipped);


            level1Completed.Reset(pLevel1Completed);
            level2Completed.Reset(pLevel2Completed);
            level3Completed.Reset(pLevel3Completed);
            level4Completed.Reset(pLevel4Completed);
            level5Completed.Reset(pLevel5Completed);
            level6Completed.Reset(pLevel6Completed);
            level7Completed.Reset(pLevel7Completed);
        }
    }

    //Punishments for tampering with protected values----
    public void PunishReset()
    {
        
        //Deleting the old save file and salt----
        //Finding paths----
        String fileName = Path.Combine(Application.persistentDataPath, "playerInfo");
        string saltLocation = Path.Combine(Application.persistentDataPath, "playerSalt");
        //Deleting files if they exist----

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        if (File.Exists(saltLocation))
        {
            File.Delete(saltLocation);
        }

        //Quitting----
        Application.Quit();
        //Debug.Log("ding");
    }

    //Data storage----
    [Serializable]
    class Playerdata
    {
        public int credits = 100;
        public int supplies = 20;

        public int cannonLevel;
        public int missileLevel;
        public int armorLevel;
        public int aerodynamicsLevel;

        public bool extendedAerofoilsOwned;
        public bool ballisticArmorOwned;
        public bool tungstenBarrelsOwned;
        public bool copperHeatSinksOwned;
        public bool heavyPayloadOwned;
        public bool persistenceHuntersOwned;
        public bool titaniumSkeletonOwned;
        public bool collisionAnalysisOwned;
        public bool secondaryHydraulicsOwned;
        public bool flightOptimizationOwned;
        public bool thermalOpticsOwned;
        public bool heatTreatmentOwned;
        public bool chaffOwned;
        public bool evasiveManeuversOwned;
        public bool armorPiercingRoundsOwned;
        public bool markIIIAutoloadersOwned;
        public bool kineticRecyclingOwned;
        public bool specialDeliveryOwned;
        public bool thrustVectoringOwned;
        public bool markIVFeederOwned;
        public bool desperationModuleOwned;
        public bool closeInSupportOwned;
        public bool nanoRepairsOwned;
        public bool magneticShieldingOwned;
        public bool escapeArtistOwned;
        public bool aresClassWeaponSystemOwned;

        public bool rocketPodsLocked = true;
        public bool missileBaysLocked = true;
        public bool carpetBombingLocked = true;
        public bool flakCannonLocked = true;
        public bool railgunCannonLocked = true;
        public bool missileJammerLocked = true;
        public bool escortDroneLocked = true;
        public bool cannonTurretLocked = true;
        public bool empMunitionsLocked = true;

        public bool rocketPodsOwned;
        public bool missileBaysOwned;
        public bool carpetBombingOwned;
        public bool flakCannonOwned;
        public bool railgunCannonOwned;
        public bool missileJammerOwned;
        public bool escortDroneOwned;
        public bool cannonTurretOwned;
        public bool empMunitionsOwned;

        public bool rocketPodsEquipped;
        public bool missileBaysEquipped;
        public bool carpetBombingEquipped;
        public bool flakCannonEquipped;
        public bool railgunCannonEquipped;
        public bool missileJammerEquipped;
        public bool escortDroneEquipped;
        public bool cannonTurretEquipped;
        public bool empMunitionsEquipped;


        public float dcmSens = 1;
        public float dcmRange = 1;
        public float volume = .5f;
        public bool fullScreen = true;

        public bool tutorialCompleted;
        public bool level1Completed;
        public bool level2Completed;
        public bool level3Completed;
        public bool level4Completed;
        public bool level5Completed;
        public bool level6Completed;
        public bool level7Completed;
    }

    //Data storage----
    [Serializable]
    class Playersalt
    {
        public byte[] salt;
    }
}
	
