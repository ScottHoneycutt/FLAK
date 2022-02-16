using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannonUtility : MonoBehaviour
{
    public GameObject normalCannon;
    public GameObject flakCannon;
    public GameObject railgunCannon;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.flakCannonEquipped.VerifyBool(GameControl.control.pFlakCannonEquipped))
        {
            normalCannon.SetActive(false);
            flakCannon.SetActive(true);
        }
        else if (GameControl.control.railgunCannonEquipped.VerifyBool(GameControl.control.pRailgunCannonEquipped))
        {
            normalCannon.SetActive(false);
            railgunCannon.SetActive(true);
        }
    }
}
