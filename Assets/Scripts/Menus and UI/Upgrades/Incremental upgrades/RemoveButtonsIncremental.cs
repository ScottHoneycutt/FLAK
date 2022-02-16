using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButtonsIncremental : MonoBehaviour
{
    //Buttons to disable----
    public GameObject cannonButton;
    public GameObject missileButton;
    public GameObject armorButton;
    public GameObject aerodynamicsButton;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    //Right as the scene loads----
    void Start()
    {
        //Disabling various incremental upgrade buttons----
        if (controller.pCannonLevel > 4)
        {
            cannonButton.SetActive(false);
        }
        if (controller.pMissileLevel > 4)
        {
            missileButton.SetActive(false);
        }
        if (controller.pArmorLevel > 4)
        {
            armorButton.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 4)
        {
            aerodynamicsButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.pCannonLevel > 4)
        {
            cannonButton.SetActive(false);
        }
        if (controller.pMissileLevel > 4)
        {
            missileButton.SetActive(false);
        }
        if (controller.pArmorLevel > 4)
        {
            armorButton.SetActive(false);
        }
        if (controller.pAerodynamicsLevel > 4)
        {
            aerodynamicsButton.SetActive(false);
        }
    }
}
