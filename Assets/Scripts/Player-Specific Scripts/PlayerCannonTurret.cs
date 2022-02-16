using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannonTurret : MonoBehaviour
{
    //Public variables----
    public Transform cannonCase;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.cannonTurretEquipped.VerifyBool(GameControl.control.pCannonTurretEquipped))
        {
            StartCoroutine("CannonTurret");
        }
    }

    IEnumerator CannonTurret()
    {
        while (true)
        {
            //Mouse position----
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            mouseX = mouseX - (Screen.width) * (float)(.5);
            mouseY = mouseY - (Screen.height) * (float)(.5);

            cannonCase.LookAt(new Vector3(cannonCase.transform.position.x + mouseX, cannonCase.transform.position.y + mouseY, cannonCase.transform.position.z));

            yield return new WaitForSeconds(.02f);
        }
    }
}
