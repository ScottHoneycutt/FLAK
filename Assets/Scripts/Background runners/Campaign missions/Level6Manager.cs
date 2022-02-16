using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level6Manager : MonoBehaviour
{
    public int pCreditsReward = 275;
    public int pSuppliesReward = 70;

    public float endDelay = 2;
    public float delayBetweenWaves = 20;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    public float delayBeforeCanvas1 = 2;
    public GameObject canvas1;
    private bool canvas1done = false;
    public GameObject canvas2;
    private bool canvas2done = false;

    public List<GameObject> waves = new List<GameObject>();

    public GameObject player;
    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject cannon4;
    public GameObject cannon5;
    public GameObject cannon6;
    public GameObject cannon7;
    public GameObject destroyer1;
    public GameObject destroyer2;
    public GameObject destroyer3;
    public EnemyShipMovement carrier;
    public ParticleSystem forwardParticles;
    public ParticleSystem backwardParticles;
    private int cannonCount = 0;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    private bool gameOver = false;
    private bool awardsGiven = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float canvas1timer = 0;
    private float waveTimer = 0;
    private int wavesSpawned = 0;


    //Anti-tampering measures----
    private void Awake()
    {
        suppliesReward = new ProtectedInt(pSuppliesReward);
        creditsReward = new ProtectedInt(pCreditsReward);
    }

    private void Start()
    {
        //Prompting first wave immediately----
        waveTimer = delayBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //first canvas----
        if (canvas1timer >= delayBeforeCanvas1 && !canvas1done)
        {
            //canvas----
            canvas1done = true;
            canvas1.SetActive(true);
            Time.timeScale = 0;
            playerUi.SetActive(false);
            //objectives text----
            objectives.text = "Objectives:\n- Destroy the hostile carrier's escort.\n- Protect the island's artillery. \n- Cannons remaining: 7";
        }
        //incrementing timer for first canvas----
        else if (!canvas1done)
        {
            canvas1timer += Time.deltaTime;
        }

        //start updating objectives text----
        if (canvas1done)
        {
            //Updating objectives text based upon number of cannons----
            cannonCount = 0;
            if (cannon1.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon2.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon3.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon4.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon5.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon6.activeInHierarchy)
            {
                cannonCount++;
            }
            if (cannon7.activeInHierarchy)
            {
                cannonCount++;
            }
            objectives.text = "Objectives:\n- Destroy the hostile carrier's escort.\n- Protect the island's artillery.\n- Cannons remaining: " + cannonCount;
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
        //If allied cannons are dead----
        else if (!cannon1.activeInHierarchy && !cannon2.activeInHierarchy && !cannon3.activeInHierarchy && !cannon4.activeInHierarchy && !cannon5.activeInHierarchy && !cannon6.activeInHierarchy && !cannon7.activeInHierarchy)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failText.text = "All allied cannons were destroyed.";
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If all destroyers are dead----
        else if (!destroyer1.activeSelf && !destroyer2.activeSelf && !destroyer3.activeSelf)
        {
            //End of game dialogue canvas----
            if (!canvas2done && timer3 >= endDelay && !gameOver)
            {
                canvas2done = true;
                canvas2.SetActive(true);
                Time.timeScale = 0;
                playerUi.SetActive(false);
                gameOver = true;
                //Carrier starts moving away----
                carrier.movesLeft = false;
                forwardParticles.Stop();
                backwardParticles.Play();
            }
            //Ending game after twice the delay----
            timer3 += Time.deltaTime;
            if (timer3 >= endDelay*3 && !awardsGiven)
            {
                Time.timeScale = 0;
                awardsGiven = true;
                victoryMenu.SetActive(true);
                playerUi.SetActive(false);
                //Updating GameControl----
                GameControl controller = GameControl.control;

                //Verifying that values have not been tampered with----
                controller.supplies.VerifyInt(controller.pSupplies);
                controller.credits.VerifyInt(controller.pCredits);
                controller.level6Completed.VerifyBool(controller.pLevel6Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel6Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level6Completed.Reset(controller.pLevel6Completed);

                controller.Save();
            }
        }
        //Spawning waves every period----
        waveTimer += Time.deltaTime;
        if (waveTimer >= delayBetweenWaves && wavesSpawned < waves.Count)
        {
            waveTimer = 0;
            //Spawning wave----
            waves[wavesSpawned].SetActive(true);
            wavesSpawned++;
        }
    }
}
