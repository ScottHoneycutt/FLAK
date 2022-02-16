using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    public int pCreditsReward = 120;
    public int pSuppliesReward = 20;

    public float endDelay = 2;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    public GameObject player;
    public GameObject alliedDestroyer;
    public UnitHP objectiveHp;
    private float percentage = 100;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    private bool gameOver = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;

    private List<GameObject> listOfEnemies = new List<GameObject>();

    //Anti-tampering measures----
    private void Awake()
    {
        creditsReward = new ProtectedInt(pCreditsReward);
        suppliesReward = new ProtectedInt(pSuppliesReward);
    }

    // Update is called once per frame
    void Update()
    {
        //Updating objective status in the objectives text----
        percentage = objectiveHp.health / objectiveHp.maximumHealth * 100;
        percentage = Mathf.Round(percentage);
        objectives.text = "Objectives:\n- Destroy all enemies.\n- Protect the allied destroyer.\n- Hull integrety: " + percentage + "%";

        listOfEnemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
        //If player is dead----
        if (!player.activeSelf)
        {
            timer += Time.deltaTime;
            if(timer >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If allied destroyer is dead----
        else if (!alliedDestroyer.activeSelf)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failText.text = "Your allied warship was destroyed.";
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If all enemies are dead----
        else if (listOfEnemies.Count == 0)
        {
            timer3 += Time.deltaTime;
            if (timer3 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                victoryMenu.SetActive(true);
                playerUi.SetActive(false);
                //Updating GameControl----
                GameControl controller = GameControl.control;

                //Verifying that values have not been tampered with----
                controller.supplies.VerifyInt(controller.pSupplies);
                controller.credits.VerifyInt(controller.pCredits);
                controller.level2Completed.VerifyBool(controller.pLevel2Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel2Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level2Completed.Reset(controller.pLevel2Completed);

                controller.Save();
            }
        }
    }
}
