using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBool
{
    //SecretBool exists to obscure the real value----

    private int boolToInt;
    private int randomization;

    //Default constructor (that should never be used)----
    public SecretBool()
    {
        boolToInt = 1;
        //Randomizing----
        randomization = Random.Range(-1000, 1000);
        boolToInt += randomization;
    }

    //Alternative constructor (use this one! :D )----
    public SecretBool(bool protect)
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
    public bool GetBool()
    {
        boolToInt -= randomization;
        if (boolToInt == 1)
        {
            return (true);
        }
        else
        {
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
