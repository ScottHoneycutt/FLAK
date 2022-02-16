using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretInt
{
    //SecretInt exists to obscure the real value----

    private int intToProtect;
    private int randomization;

    //Default constructor (that should never be used)----
    public SecretInt()
    {
        randomization = Random.Range(-1000, 1000);
        intToProtect = randomization;
    }

    //Alternative constructor (use this one! :D )----
    public SecretInt(int protect)
    {
        randomization = Random.Range(-1000, 1000);
        intToProtect = protect + randomization;
    }

    //Verification method----
    public int GetInt()
    {
        return (intToProtect - randomization);
    }

    //Reset method----
    public void Reset(int protect)
    {
        randomization = Random.Range(-1000, 1000);
        intToProtect = protect + randomization;
    }
}
