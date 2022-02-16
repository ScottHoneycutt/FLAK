using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretFloat
{
    //SecretFloat exists to obscure the real value----

    private float floatToProtect;
    private float randomization;

    //Default constructor (that should never be used)----
    public SecretFloat()
    {
        randomization = Random.Range(-1000f, 1000f);
        floatToProtect = randomization;
    }

    //Alternative constructor (use this one! :D )----
    public SecretFloat(float protect)
    {
        randomization = Random.Range(-1000f, 1000f);
        floatToProtect = protect + randomization;
    }

    //Verification method----
    public float GetFloat()
    {
        return (floatToProtect - randomization);
    }

    //Reset method----
    public void Reset(float protect)
    {
        randomization = Random.Range(-1000f, 1000f);
        floatToProtect = protect + randomization;
    }
}
