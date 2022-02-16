using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechUpgradeManager : MonoBehaviour
{
    public static TechUpgradeManager techManager;

    //Red light boxes----
    public GameObject tbR;
    public GameObject chsR;
    public GameObject hpR;
    public GameObject tsR;
    public GameObject shR;
    public GameObject phR;
    public GameObject caR;
    public GameObject foR;
    public GameObject aprR;
    public GameObject m4fR;
    public GameObject m3alR;
    public GameObject toR;
    public GameObject nrR;
    public GameObject cR;
    public GameObject htR;
    public GameObject krR;
    public GameObject emR;
    public GameObject tvR;
    public GameObject msR;
    public GameObject sdR;
    public GameObject dmR;
    public GameObject eaR;
    public GameObject cisR;
    public GameObject acwsR;

    //Blue light boxes----
    public GameObject eafB;
    public GameObject baB;
    public GameObject tbB;
    public GameObject chsB;
    public GameObject hpB;
    public GameObject tsB;
    public GameObject shB;
    public GameObject phB;
    public GameObject caB;
    public GameObject foB;
    public GameObject aprB;
    public GameObject m4fB;
    public GameObject m3alB;
    public GameObject toB;
    public GameObject nrB;
    public GameObject cB;
    public GameObject htB;
    public GameObject krB;
    public GameObject emB;
    public GameObject tvB;
    public GameObject msB;
    public GameObject sdB;
    public GameObject dmB;
    public GameObject eaB;
    public GameObject cisB;
    public GameObject acwsB;

    //Green light boxes----
    public GameObject eafG;
    public GameObject baG;
    public GameObject tbG;
    public GameObject chsG;
    public GameObject hpG;
    public GameObject tsG;
    public GameObject shG;
    public GameObject phG;
    public GameObject caG;
    public GameObject foG;
    public GameObject aprG;
    public GameObject m4fG;
    public GameObject m3alG;
    public GameObject toG;
    public GameObject nrG;
    public GameObject cG;
    public GameObject htG;
    public GameObject krG;
    public GameObject emG;
    public GameObject tvG;
    public GameObject msG;
    public GameObject sdG;
    public GameObject dmG;
    public GameObject eaG;
    public GameObject cisG;
    public GameObject acwsG;

    //Lit bars----
    public GameObject eafBar1;
    public GameObject eafBar2;
    public GameObject tbBar;
    public GameObject chsBar;
    public GameObject tsBar1;
    public GameObject tsBar2;
    public GameObject caBar;
    public GameObject toBar;
    public GameObject htBar;
    public GameObject m3alBar;
    public GameObject krBar;
    public GameObject cBar;
    public GameObject sdBar1;
    public GameObject sdBar2;
    public GameObject dmBar;
    public GameObject cisBar;
    public GameObject baBar1;
    public GameObject baBar2;
    public GameObject hpBar;
    public GameObject phBar;
    public GameObject shBar;
    public GameObject foBar1;
    public GameObject foBar2;
    public GameObject emBar;
    public GameObject aprBar;
    public GameObject tvBar;
    public GameObject m4fBar;
    public GameObject msBar;
    public GameObject nrBar;

    //Unlit bars----
    public GameObject eafBar1u;
    public GameObject eafBar2u;
    public GameObject tbBaru;
    public GameObject chsBaru;
    public GameObject tsBar1u;
    public GameObject tsBar2u;
    public GameObject caBaru;
    public GameObject toBaru;
    public GameObject htBaru;
    public GameObject m3alBaru;
    public GameObject krBaru;
    public GameObject cBaru;
    public GameObject sdBar1u;
    public GameObject sdBar2u;
    public GameObject dmBaru;
    public GameObject cisBaru;
    public GameObject baBar1u;
    public GameObject baBar2u;
    public GameObject hpBaru;
    public GameObject phBaru;
    public GameObject shBaru;
    public GameObject foBar1u;
    public GameObject foBar2u;
    public GameObject emBaru;
    public GameObject aprBaru;
    public GameObject tvBaru;
    public GameObject m4fBaru;
    public GameObject msBaru;
    public GameObject nrBaru;


    private void Awake()
    {
        techManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Changing box colors according to owned, unlocked, or locked----
        //Extended aerofoils----
        if (GameControl.control.pExtendedAerofoilsOwned)
        {
            eafB.SetActive(false);
            eafG.SetActive(true);

            //Bars---
            eafBar1.SetActive(true);
            eafBar2.SetActive(true);
            eafBar1u.SetActive(false);
            eafBar2u.SetActive(false);

            //Tungsten barrels branch----
            if (GameControl.control.pTungstenBarrelsOwned)
            {
                tbR.SetActive(false);
                tbG.SetActive(true);

                //Bars---
                tbBar.SetActive(true);
                tbBaru.SetActive(false);

                //Titanium Skeleton----
                if (GameControl.control.pTitaniumSkeletonOwned)
                {
                    tsR.SetActive(false);
                    tsG.SetActive(true);

                    //Bars---
                    tsBar1.SetActive(true);
                    tsBar2.SetActive(true);
                    tsBar1u.SetActive(false);
                    tsBar2u.SetActive(false);

                    //Thermal optics branch----
                    if (GameControl.control.pThermalOpticsOwned)
                    {
                        toR.SetActive(false);
                        toG.SetActive(true);

                        //Bars---
                        toBar.SetActive(true);
                        toBaru.SetActive(false);

                        //Mark III autoloaders----
                        if (GameControl.control.pMarkIIIAutoloadersOwned)
                        {
                            m3alR.SetActive(false);
                            m3alG.SetActive(true);

                            //Bars---
                            m3alBar.SetActive(true);
                            m3alBaru.SetActive(false);

                            //Branch merges with Kinetic recycling----
                        }
                        else
                        {
                            m3alR.SetActive(false);
                            m3alB.SetActive(true);
                        }
                    }
                    else
                    {
                        toR.SetActive(false);
                        toB.SetActive(true);
                    }

                    //Heat treatment branch----
                    if (GameControl.control.pHeatTreatmentOwned)
                    {
                        htR.SetActive(false);
                        htG.SetActive(true);

                        //Bars---
                        htBar.SetActive(true);
                        htBaru.SetActive(false);

                        //Kinetic Recycling----
                        if (GameControl.control.pKineticRecyclingOwned)
                        {
                            krR.SetActive(false);
                            krG.SetActive(true);

                            //Bars---
                            krBar.SetActive(true);
                            krBaru.SetActive(false);

                            //Branch merges with Mark III autoloaders----
                        }
                        else
                        {
                            krR.SetActive(false);
                            krB.SetActive(true);
                        }
                    }
                    else
                    {
                        htR.SetActive(false);
                        htB.SetActive(true);
                    }
                }
                else
                {
                    tsR.SetActive(false);
                    tsB.SetActive(true);
                }
            }
            else
            {
                tbR.SetActive(false);
                tbB.SetActive(true);
            }

            //Copper heat sinks branch----
            if (GameControl.control.pCopperHeatSinksOwned)
            {
                chsR.SetActive(false);
                chsG.SetActive(true);

                //Bars---
                chsBar.SetActive(true);
                chsBaru.SetActive(false);

                //Collision analysis----
                if (GameControl.control.pCollisionAnalysisOwned)
                {
                    caR.SetActive(false);
                    caG.SetActive(true);

                    //Bars---
                    caBar.SetActive(true);
                    caBaru.SetActive(false);

                    //Merges with Secondary hydraulics----
                }
                else
                {
                    caR.SetActive(false);
                    caB.SetActive(true);
                }
            }
            else
            {
                chsR.SetActive(false);
                chsB.SetActive(true);
            }
        }



        //Ballistic armor----
        if (GameControl.control.pBallisticArmorOwned)
        {
            baB.SetActive(false);
            baG.SetActive(true);

            //Bars---
            baBar1.SetActive(true);
            baBar2.SetActive(true);
            baBar1u.SetActive(false);
            baBar2u.SetActive(false);

            //Persistence hunters branch----
            if (GameControl.control.pPersistenceHuntersOwned)
            {
                phR.SetActive(false);
                phG.SetActive(true);

                //Bars---
                phBar.SetActive(true);
                phBaru.SetActive(false);

                //Flight optimization----
                if (GameControl.control.pFlightOptimizationOwned)
                {
                    foR.SetActive(false);
                    foG.SetActive(true);

                    //Bars---
                    foBar1.SetActive(true);
                    foBar2.SetActive(true);
                    foBar1u.SetActive(false);
                    foBar2u.SetActive(false);

                    //Armor-piercing rounds branch----
                    if (GameControl.control.pArmorPiercingRoundsOwned)
                    {
                        aprR.SetActive(false);
                        aprG.SetActive(true);

                        //Bars---
                        aprBar.SetActive(true);
                        aprBaru.SetActive(false);

                        //Mark IV feeder----
                        if (GameControl.control.pMarkIVFeederOwned)
                        {
                            m4fR.SetActive(false);
                            m4fG.SetActive(true);

                            //Bars---
                            m4fBar.SetActive(true);
                            m4fBaru.SetActive(false);

                            //Branch merges with Thrust vectoring----
                        }
                        else
                        {
                            m4fR.SetActive(false);
                            m4fB.SetActive(true);
                        }
                    }
                    else
                    {
                        aprR.SetActive(false);
                        aprB.SetActive(true);
                    }

                    //Evasive maneuvers branch----
                    if (GameControl.control.pEvasiveManeuversOwned)
                    {
                        emR.SetActive(false);
                        emG.SetActive(true);

                        //Bars---
                        emBar.SetActive(true);
                        emBaru.SetActive(false);

                        //Thrust vectoring----
                        if (GameControl.control.pThrustVectoringOwned)
                        {
                            tvR.SetActive(false);
                            tvG.SetActive(true);

                            //Bars---
                            tvBar.SetActive(true);
                            tvBaru.SetActive(false);

                            //Branch merges with Mark III autoloaders----
                        }
                        else
                        {
                            tvR.SetActive(false);
                            tvB.SetActive(true);
                        }
                    }
                    else
                    {
                        emR.SetActive(false);
                        emB.SetActive(true);
                    }
                }
                else
                {
                    foR.SetActive(false);
                    foB.SetActive(true);
                }
            }
            else
            {
                phR.SetActive(false);
                phB.SetActive(true);
            }

            //Heavy payload branch----
            if (GameControl.control.pHeavyPayloadOwned)
            {
                hpR.SetActive(false);
                hpG.SetActive(true);

                //Bars---
                hpBar.SetActive(true);
                hpBaru.SetActive(false);

                //Secondary hydraulics----
                if (GameControl.control.pSecondaryHydraulicsOwned)
                {
                    shR.SetActive(false);
                    shG.SetActive(true);

                    //Bars---
                    shBar.SetActive(true);
                    shBaru.SetActive(false);

                    //Merges with Collision analysis----
                }
                else
                {
                    shR.SetActive(false);
                    shB.SetActive(true);
                }
            }
            else
            {
                hpR.SetActive(false);
                hpB.SetActive(true);
            }
        }

        //Picking up at the Chaff branch (secondary hydraulics and collision analysis intersection)----
        if (GameControl.control.pSecondaryHydraulicsOwned || GameControl.control.pCollisionAnalysisOwned)
        {
            if (GameControl.control.pChaffOwned)
            {
                cR.SetActive(false);
                cG.SetActive(true);

                //Bars---
                cBar.SetActive(true);
                cBaru.SetActive(false);

                //Special delivery---
                if (GameControl.control.pSpecialDeliveryOwned)
                {
                    sdR.SetActive(false);
                    sdG.SetActive(true);

                    //Bars---
                    sdBar1.SetActive(true);
                    sdBar2.SetActive(true);
                    sdBar1u.SetActive(false);
                    sdBar2u.SetActive(false);

                    //Close-in support branch----
                    if (GameControl.control.pCloseInSupportOwned)
                    {
                        cisR.SetActive(false);
                        cisG.SetActive(true);

                        //Bars---
                        cisBar.SetActive(true);
                        cisBaru.SetActive(false);

                        //Merges with Desperation module----
                    }
                    else
                    {
                        cisR.SetActive(false);
                        cisB.SetActive(true);
                    }

                    //Nano-repairs branch----
                    if (GameControl.control.pNanoRepairsOwned)
                    {
                        nrR.SetActive(false);
                        nrG.SetActive(true);

                        //Bars---
                        nrBar.SetActive(true);
                        nrBaru.SetActive(false);
                    }
                    else
                    {
                        nrR.SetActive(false);
                        nrB.SetActive(true);
                    }
                }
                else
                {
                    sdR.SetActive(false);
                    sdB.SetActive(true);
                }
            }
            else
            {
                cR.SetActive(false);
                cB.SetActive(true);
            }
        }

        //Picking up at Desperation module (mark III autoloaders and kinetic recycling intersection)----
        if (GameControl.control.pMarkIIIAutoloadersOwned || GameControl.control.pKineticRecyclingOwned)
        {
            if (GameControl.control.pDesperationModuleOwned)
            {
                dmR.SetActive(false);
                dmG.SetActive(true);

                //Bars---
                dmBar.SetActive(true);
                dmBaru.SetActive(false);
            }
            else
            {
                dmR.SetActive(false);
                dmB.SetActive(true);
            }
        }
        //Picking up at Magnetic Shielding (mark IV Feeder and thrust vectoring intersection)----
        if (GameControl.control.pThrustVectoringOwned || GameControl.control.pMarkIVFeederOwned)
        {
            if (GameControl.control.pMagneticShieldingOwned)
            {
                msR.SetActive(false);
                msG.SetActive(true);

                //Bars---
                msBar.SetActive(true);
                msBaru.SetActive(false);
            }
            else
            {
                msR.SetActive(false);
                msB.SetActive(true);
            }
        }
        //Picking up at Ares-class weapons system (desperation module and close-in support intersection)----
        if (GameControl.control.pDesperationModuleOwned || GameControl.control.pCloseInSupportOwned)
        {
            if (GameControl.control.pAresClassWeaponSystemOwned)
            {
                acwsR.SetActive(false);
                acwsG.SetActive(true);
            }
            else
            {
                acwsR.SetActive(false);
                acwsB.SetActive(true);
            }
        }
        //Picking up at Escape artist (magnetic shielding and nano-repairs intersection)----
        if (GameControl.control.pMagneticShieldingOwned || GameControl.control.pNanoRepairsOwned)
        {
            if (GameControl.control.pEscapeArtistOwned)
            {
                eaR.SetActive(false);
                eaG.SetActive(true);
            }
            else
            {
                eaR.SetActive(false);
                eaB.SetActive(true);
            }
        }
    }

    //Refresh to be called whenever an update occurs (identical to start function)----
    public void Refresh()
    {
        //Changing box colors according to owned, unlocked, or locked----
        //Extended aerofoils----
        if (GameControl.control.pExtendedAerofoilsOwned)
        {
            eafB.SetActive(false);
            eafG.SetActive(true);

            //Bars---
            eafBar1.SetActive(true);
            eafBar2.SetActive(true);
            eafBar1u.SetActive(false);
            eafBar2u.SetActive(false);

            //Tungsten barrels branch----
            if (GameControl.control.pTungstenBarrelsOwned)
            {
                tbR.SetActive(false);
                tbG.SetActive(true);

                //Bars---
                tbBar.SetActive(true);
                tbBaru.SetActive(false);

                //Titanium Skeleton----
                if (GameControl.control.pTitaniumSkeletonOwned)
                {
                    tsR.SetActive(false);
                    tsG.SetActive(true);

                    //Bars---
                    tsBar1.SetActive(true);
                    tsBar2.SetActive(true);
                    tsBar1u.SetActive(false);
                    tsBar2u.SetActive(false);

                    //Thermal optics branch----
                    if (GameControl.control.pThermalOpticsOwned)
                    {
                        toR.SetActive(false);
                        toG.SetActive(true);

                        //Bars---
                        toBar.SetActive(true);
                        toBaru.SetActive(false);

                        //Mark III autoloaders----
                        if (GameControl.control.pMarkIIIAutoloadersOwned)
                        {
                            m3alR.SetActive(false);
                            m3alG.SetActive(true);

                            //Bars---
                            m3alBar.SetActive(true);
                            m3alBaru.SetActive(false);

                            //Branch merges with Kinetic recycling----
                        }
                        else
                        {
                            m3alR.SetActive(false);
                            m3alB.SetActive(true);
                        }
                    }
                    else
                    {
                        toR.SetActive(false);
                        toB.SetActive(true);
                    }

                    //Heat treatment branch----
                    if (GameControl.control.pHeatTreatmentOwned)
                    {
                        htR.SetActive(false);
                        htG.SetActive(true);

                        //Bars---
                        htBar.SetActive(true);
                        htBaru.SetActive(false);

                        //Kinetic Recycling----
                        if (GameControl.control.pKineticRecyclingOwned)
                        {
                            krR.SetActive(false);
                            krG.SetActive(true);

                            //Bars---
                            krBar.SetActive(true);
                            krBaru.SetActive(false);

                            //Branch merges with Mark III autoloaders----
                        }
                        else
                        {
                            krR.SetActive(false);
                            krB.SetActive(true);
                        }
                    }
                    else
                    {
                        htR.SetActive(false);
                        htB.SetActive(true);
                    }
                }
                else
                {
                    tsR.SetActive(false);
                    tsB.SetActive(true);
                }
            }
            else
            {
                tbR.SetActive(false);
                tbB.SetActive(true);
            }

            //Copper heat sinks branch----
            if (GameControl.control.pCopperHeatSinksOwned)
            {
                chsR.SetActive(false);
                chsG.SetActive(true);

                //Bars---
                chsBar.SetActive(true);
                chsBaru.SetActive(false);

                //Collision analysis----
                if (GameControl.control.pCollisionAnalysisOwned)
                {
                    caR.SetActive(false);
                    caG.SetActive(true);

                    //Bars---
                    caBar.SetActive(true);
                    caBaru.SetActive(false);

                    //Merges with Secondary hydraulics----
                }
                else
                {
                    caR.SetActive(false);
                    caB.SetActive(true);
                }
            }
            else
            {
                chsR.SetActive(false);
                chsB.SetActive(true);
            }
        }



        //Ballistic armor----
        if (GameControl.control.pBallisticArmorOwned)
        {
            baB.SetActive(false);
            baG.SetActive(true);

            //Bars---
            baBar1.SetActive(true);
            baBar2.SetActive(true);
            baBar1u.SetActive(false);
            baBar2u.SetActive(false);

            //Persistence hunters branch----
            if (GameControl.control.pPersistenceHuntersOwned)
            {
                phR.SetActive(false);
                phG.SetActive(true);

                //Bars---
                phBar.SetActive(true);
                phBaru.SetActive(false);

                //Flight optimization----
                if (GameControl.control.pFlightOptimizationOwned)
                {
                    foR.SetActive(false);
                    foG.SetActive(true);

                    //Bars---
                    foBar1.SetActive(true);
                    foBar2.SetActive(true);
                    foBar1u.SetActive(false);
                    foBar2u.SetActive(false);

                    //Armor-piercing rounds branch----
                    if (GameControl.control.pArmorPiercingRoundsOwned)
                    {
                        aprR.SetActive(false);
                        aprG.SetActive(true);

                        //Bars---
                        aprBar.SetActive(true);
                        aprBaru.SetActive(false);

                        //Mark IV feeder----
                        if (GameControl.control.pMarkIVFeederOwned)
                        {
                            m4fR.SetActive(false);
                            m4fG.SetActive(true);

                            //Bars---
                            m4fBar.SetActive(true);
                            m4fBaru.SetActive(false);

                            //Branch merges with Thrust vectoring----
                        }
                        else
                        {
                            m4fR.SetActive(false);
                            m4fB.SetActive(true);
                        }
                    }
                    else
                    {
                        aprR.SetActive(false);
                        aprB.SetActive(true);
                    }

                    //Evasive maneuvers branch----
                    if (GameControl.control.pEvasiveManeuversOwned)
                    {
                        emR.SetActive(false);
                        emG.SetActive(true);

                        //Bars---
                        emBar.SetActive(true);
                        emBaru.SetActive(false);

                        //Thrust vectoring----
                        if (GameControl.control.pThrustVectoringOwned)
                        {
                            tvR.SetActive(false);
                            tvG.SetActive(true);

                            //Bars---
                            tvBar.SetActive(true);
                            tvBaru.SetActive(false);

                            //Branch merges with Mark III autoloaders----
                        }
                        else
                        {
                            tvR.SetActive(false);
                            tvB.SetActive(true);
                        }
                    }
                    else
                    {
                        emR.SetActive(false);
                        emB.SetActive(true);
                    }
                }
                else
                {
                    foR.SetActive(false);
                    foB.SetActive(true);
                }
            }
            else
            {
                phR.SetActive(false);
                phB.SetActive(true);
            }

            //Heavy payload branch----
            if (GameControl.control.pHeavyPayloadOwned)
            {
                hpR.SetActive(false);
                hpG.SetActive(true);

                //Bars---
                hpBar.SetActive(true);
                hpBaru.SetActive(false);

                //Secondary hydraulics----
                if (GameControl.control.pSecondaryHydraulicsOwned)
                {
                    shR.SetActive(false);
                    shG.SetActive(true);

                    //Bars---
                    shBar.SetActive(true);
                    shBaru.SetActive(false);

                    //Merges with Collision analysis----
                }
                else
                {
                    shR.SetActive(false);
                    shB.SetActive(true);
                }
            }
            else
            {
                hpR.SetActive(false);
                hpB.SetActive(true);
            }
        }

        //Picking up at the Chaff branch (secondary hydraulics and collision analysis intersection)----
        if (GameControl.control.pSecondaryHydraulicsOwned || GameControl.control.pCollisionAnalysisOwned)
        {
            if (GameControl.control.pChaffOwned)
            {
                cR.SetActive(false);
                cG.SetActive(true);

                //Bars---
                cBar.SetActive(true);
                cBaru.SetActive(false);

                //Special delivery---
                if (GameControl.control.pSpecialDeliveryOwned)
                {
                    sdR.SetActive(false);
                    sdG.SetActive(true);

                    //Bars---
                    sdBar1.SetActive(true);
                    sdBar2.SetActive(true);
                    sdBar1u.SetActive(false);
                    sdBar2u.SetActive(false);

                    //Close-in support branch----
                    if (GameControl.control.pCloseInSupportOwned)
                    {
                        cisR.SetActive(false);
                        cisG.SetActive(true);

                        //Bars---
                        cisBar.SetActive(true);
                        cisBaru.SetActive(false);

                        //Merges with Desperation module----
                    }
                    else
                    {
                        cisR.SetActive(false);
                        cisB.SetActive(true);
                    }

                    //Nano-repairs branch----
                    if (GameControl.control.pNanoRepairsOwned)
                    {
                        nrR.SetActive(false);
                        nrG.SetActive(true);

                        //Bars---
                        nrBar.SetActive(true);
                        nrBaru.SetActive(false);
                    }
                    else
                    {
                        nrR.SetActive(false);
                        nrB.SetActive(true);
                    }
                }
                else
                {
                    sdR.SetActive(false);
                    sdB.SetActive(true);
                }
            }
            else
            {
                cR.SetActive(false);
                cB.SetActive(true);
            }
        }

        //Picking up at Desperation module (mark III autoloaders and kinetic recycling intersection)----
        if (GameControl.control.pMarkIIIAutoloadersOwned || GameControl.control.pKineticRecyclingOwned)
        {
            if (GameControl.control.pDesperationModuleOwned)
            {
                dmR.SetActive(false);
                dmG.SetActive(true);

                //Bars---
                dmBar.SetActive(true);
                dmBaru.SetActive(false);
            }
            else
            {
                dmR.SetActive(false);
                dmB.SetActive(true);
            }
        }
        //Picking up at Magnetic Shielding (mark IV Feeder and thrust vectoring intersection)----
        if (GameControl.control.pThrustVectoringOwned || GameControl.control.pMarkIVFeederOwned)
        {
            if (GameControl.control.pMagneticShieldingOwned)
            {
                msR.SetActive(false);
                msG.SetActive(true);

                //Bars---
                msBar.SetActive(true);
                msBaru.SetActive(false);
            }
            else
            {
                msR.SetActive(false);
                msB.SetActive(true);
            }
        }
        //Picking up at Ares-class weapons system (desperation module and close-in support intersection)----
        if (GameControl.control.pDesperationModuleOwned || GameControl.control.pCloseInSupportOwned)
        {
            if (GameControl.control.pAresClassWeaponSystemOwned)
            {
                acwsR.SetActive(false);
                acwsG.SetActive(true);
            }
            else
            {
                acwsR.SetActive(false);
                acwsB.SetActive(true);
            }
        }
        //Picking up at Escape artist (magnetic shielding and nano-repairs intersection)----
        if (GameControl.control.pMagneticShieldingOwned || GameControl.control.pNanoRepairsOwned)
        {
            if (GameControl.control.pEscapeArtistOwned)
            {
                eaR.SetActive(false);
                eaG.SetActive(true);
            }
            else
            {
                eaR.SetActive(false);
                eaB.SetActive(true);
            }
        }
    }
}
