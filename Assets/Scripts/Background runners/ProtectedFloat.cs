using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedFloat 
{
    //ProtectedFloat leaves the original float intact as bait and verifies it before use, inflicting punishments if tampering is detected----

    private float floatToProtect;
    private float randomization;

    //Default constructor (that should never be used)----
    public ProtectedFloat()
    {
        randomization = Random.Range(-10f, 10f);
        floatToProtect = randomization;
    }

    //Alternative constructor (use this one! :D )----
    public ProtectedFloat(float protect)
    {
        randomization = Random.Range(-10f, 10f);
        floatToProtect = protect + randomization;
    }

    //Verification method----
    public float VerifyFloat(float original)
    {
        //Detecting differences between original and the protected value----
        if(original != floatToProtect - randomization)
        {
            //Punishments go here----
            GameControl.control.PunishReset();
        }
        return (floatToProtect - randomization);
    }

    //Reset method----
    public void Reset(float protect)
    {
        randomization = Random.Range(-10f, 10f);
        floatToProtect = protect + randomization;
    }
}
