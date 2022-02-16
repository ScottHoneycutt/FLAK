using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level7Manager : MonoBehaviour
{
    public int pCreditsReward = 275;
    public int pSuppliesReward = 70;

    public float endDelay = 2;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    public float delayBeforeCanvas1 = 2;
    public GameObject canvas1;
    private bool canvas1done = false;

    public GameObject player;
    public GameObject bomber1;
    public GameObject bomber2;
    public GameObject bomber3;
    public GameObject carrier;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    private bool gameOver = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float canvas1timer = 0;

    private float bomberTimer = 72;
    private float bomberMinutes = 0;
    private float bomberSeconds = 0;

    //Anti-tampering measures----
    private void Awake()
    {
        suppliesReward = new ProtectedInt(pSuppliesReward);
        creditsReward = new ProtectedInt(pCreditsReward);
    }

    // Update is called once per frame
    void Update()
    {
        //Updating timer for objectives text----
        if (bomberTimer > 0)
        {
            bomberTimer -= Time.deltaTime;
            bomberMinutes = (int)bomberTimer / 60;
            bomberSeconds = (int)bomberTimer % 60;
            //Adding the zero to the timer display when needed----
            if (bomberSeconds < 10)
            {
                objectives.text = "Objectives:\n- Protect all allied bombers.\n- Bombers within range in: " + bomberMinutes + ":0" + bomberSeconds;
            }
            else
            {
                objectives.text = "Objectives:\n- Protect all allied bombers.\n- Bombers within range in: " + bomberMinutes + ":" + bomberSeconds;
            }
        }

        //first canvas----
        if (canvas1timer >= delayBeforeCanvas1 && !canvas1done)
        {
            //canvas----
            canvas1done = true;
            canvas1.SetActive(true);
            Time.timeScale = 0;
            playerUi.SetActive(false);
        }
        //incrementing timer for first canvas----
        else if (!canvas1done)
        {
            canvas1timer += Time.deltaTime;
        }

        //If player is dead----
        if (!player.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If any allied bombers are dead----
        else if (!bomber1.activeSelf || !bomber2.activeSelf || !bomber3.activeSelf)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failText.text = "An allied bomber was destroyed.";
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If enemy carrier is dead----
        else if (!carrier.activeSelf)
        {
            //Ending game after delay----
            timer3 += Time.deltaTime;
            if (timer3 >= endDelay)
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
                controller.level7Completed.VerifyBool(controller.pLevel7Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel7Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level7Completed.Reset(controller.pLevel7Completed);

                controller.Save();
            }
        }
    }
}
