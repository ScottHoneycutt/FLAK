using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedBool
{
    //SecretBool exists to obscure the real value----

    private int boolToInt;
    private int randomization;

    //Default constructor (that should never be used)----
    public ProtectedBool()
    {
        boolToInt = 1;
        //Randomizing----
        randomization = Random.Range(-1000, 1000);
        boolToInt += randomization;
    }

    //Alternative constructor (use this one! :D )----
    public ProtectedBool(bool protect)
    {
        //True = 1;
        if (protect)
        {
            boolToInt = 1;
        }
        //False = 0;
        else
        {
            boolToInt = 0;
        }
        //Randomizing----
        randomization = Random.Range(-1000, 1000);
        boolToInt += randomization;
    }

    //Verification method----
    public bool VerifyBool(bool original)
    {
        //Real value true----
        if (boolToInt-randomization == 1)
        {
            //Mismatch----
            if (!original)
            {
                //Punishments go here----
                GameControl.control.PunishReset();
            }
            return (true);
        }
        //Real value false----
        else
        {
            //Mismatch----
            if (original)
            {
                //Punishments go here----
                GameControl.control.PunishReset();
            }
            return (false);
        }
    }

    //Reset method----
    public void Reset(bool protect)
    {
        //True = 1;
        if (protect)
        {
            boolToInt = 1;
        }
        //False = 0;
        else
        {
            boolToInt = 0;
        }
        //Randomizing----
        randomization = Random.Range(-1000, 1000);
        boolToInt += randomization;
    }
}
