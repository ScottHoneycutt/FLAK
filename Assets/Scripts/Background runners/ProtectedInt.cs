using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedInt
{
    //ProtectedInt leaves the original int intact as bait and verifies it before use, inflicting punishments if tampering is detected----

    private int  intToProtect;
    private int randomization;

    //Default constructor (that should never be used)----
    public ProtectedInt()
    {
        randomization = Random.Range(-10, 10);
        intToProtect = randomization;
    }

    //Alternative constructor (use this one! :D )----
    public ProtectedInt(int protect)
    {
        randomization = Random.Range(-10, 10);
        intToProtect = protect + randomization;
    }

    //Verification method----
    public int VerifyInt(int original)
    {
        //Detecting differences between original and the protected value----
        if (original != intToProtect - randomization)
        {
            //Punishments go here----
            GameControl.control.PunishReset();
        }
        return (intToProtect - randomization);
    }

    //Reset method----
    public void Reset(int protect)
    {
        randomization = Random.Range(-10, 10);
        intToProtect = protect + randomization;
    }
}
