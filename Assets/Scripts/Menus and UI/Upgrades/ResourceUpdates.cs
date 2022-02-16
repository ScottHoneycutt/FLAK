using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUpdates : MonoBehaviour
{
    public Text display;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (controller.pCredits > 99999 && controller.pSupplies > 99999)
        {
            display.text = "Credits: 99999+ \nSupplies: 99999+";
        }
        else if (controller.pCredits > 99999)
        {
            display.text = "Credits: 99999+ \nSupplies: " + controller.pSupplies;
        }
        else if (controller.pSupplies > 99999)
        {
            display.text = "Credits: " + controller.pCredits + "\nSupplies: 99999+";
        }
        else
        {
            display.text = "Credits: " + controller.pCredits + "\nSupplies: " + controller.pSupplies;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.pCredits > 99999 && controller.pSupplies > 99999)
        {
            display.text = "Credits: 99999+ \nSupplies: 99999+";
        }
        else if (controller.pCredits > 99999)
        {
            display.text = "Credits: 99999+ \nSupplies: " + controller.pSupplies;
        }
        else if (controller.pSupplies > 99999)
        {
            display.text = "Credits: " + controller.pCredits + "\nSupplies: 99999+";
        }
        else
        {
            display.text = "Credits: " + controller.pCredits + "\nSupplies: " + controller.pSupplies;
        }
    }
}
