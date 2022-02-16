using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    public GameObject level1Red;
    public GameObject level1Green;
    public GameObject level2Red;
    public GameObject level2Green;
    public GameObject level3Red;
    public GameObject level3Green;
    public GameObject level4Red;
    public GameObject level4Green;
    public GameObject level5Red;
    public GameObject level5Green;
    public GameObject level6Red;
    public GameObject level6Green;
    public GameObject level7Red;
    public GameObject level7Green;
    public GameObject nextMap;

    //GameControl----
    GameControl controller;
    
    private void Awake()
    {
        controller = GameControl.control;
    }

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        //Level 1 completed----
        if (controller.level1Completed.VerifyBool(controller.pLevel1Completed) && !controller.level2Completed.VerifyBool(controller.pLevel2Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            //Next Level----
            level2Red.SetActive(true);
        }
        //Level 2 completed----
        else if (controller.level2Completed.VerifyBool(controller.pLevel2Completed) && !controller.level3Completed.VerifyBool(controller.pLevel3Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            //Next Level----
            level3Red.SetActive(true);
        }
        //Level 3 completed----
        else if (controller.level3Completed.VerifyBool(controller.pLevel3Completed) && !controller.level4Completed.VerifyBool(controller.pLevel4Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            level3Green.SetActive(true);
            //Next Level----
            level4Red.SetActive(true);
        }
        //Level 4 completed----
        else if (controller.level4Completed.VerifyBool(controller.pLevel4Completed) && !controller.level5Completed.VerifyBool(controller.pLevel5Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            level3Green.SetActive(true);
            level4Green.SetActive(true);
            //Next Level----
            level5Red.SetActive(true);
        }
        //Level 5 completed----
        else if (controller.level5Completed.VerifyBool(controller.pLevel5Completed) && !controller.level6Completed.VerifyBool(controller.pLevel6Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            level3Green.SetActive(true);
            level4Green.SetActive(true);
            level5Green.SetActive(true);
            //Next Level----
            level6Red.SetActive(true);
        }
        //Level 6 completed----
        else if (controller.level6Completed.VerifyBool(controller.pLevel6Completed) && !controller.level7Completed.VerifyBool(controller.pLevel7Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            level3Green.SetActive(true);
            level4Green.SetActive(true);
            level5Green.SetActive(true);
            level6Green.SetActive(true);
            //Next Level----
            level7Red.SetActive(true);
        }
        //Level 7 completed----
        else if (controller.level7Completed.VerifyBool(controller.pLevel7Completed))
        {
            level1Red.SetActive(false);
            level1Green.SetActive(true);
            level2Green.SetActive(true);
            level3Green.SetActive(true);
            level4Green.SetActive(true);
            level5Green.SetActive(true);
            level6Green.SetActive(true);
            level7Green.SetActive(true);
            //Next Level----

        }
    }
}
