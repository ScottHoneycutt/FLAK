using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCarpetBombs : MonoBehaviour
{
   public void Unlock()
    {
        GameControl controller = GameControl.control;

        //Checking for tampering----
        controller.carpetBombingLocked.VerifyBool(controller.pCarpetBombingLocked);

        //Changing protected value----
        controller.pCarpetBombingLocked = false;

        //Reseting protector----
        controller.carpetBombingLocked.Reset(controller.pCarpetBombingLocked);
        GameControl.control.Save();
    }
}
