using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level5Manager : MonoBehaviour
{
    public int pCreditsReward = 270;
    public int pSuppliesReward = 65;

    public float endDelay = 2;
    public float delayBetweenWaves = 20;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    public GameObject canvas1;
    private bool canvas1done = false;
    public GameObject canvas2;
    private bool canvas2done = false;

    public List<GameObject> waves = new List<GameObject>();

    public GameObject player;
    public Transform playerTransform;
    public GameObject alliedCarrier;
    public UnitHP objectiveHp;
    private float percentage = 100;
    public Transform alliedCarrierTransform;
    public float distanceFromCarrier = 600;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    private bool gameOver = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float waveTimer = 0;
    private int wavesSpawned = 0;

    private List<GameObject> listOfEnemies = new List<GameObject>();


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
        if (Vector3.Distance(playerTransform.position, alliedCarrierTransform.position) < distanceFromCarrier && !canvas1done)
        {
            //canvas----
            canvas1done = true;
            canvas1.SetActive(true);
            Time.timeScale = 0;
            playerUi.SetActive(false);
            //objectives text----
            objectives.text = "Objectives:\n- Destroy all enemies.\n- Protect the allied aircraft carrier.\n- Hull integrity: 100%";
        }
        //carrier HP updates after it appears in objectives----
        if (canvas1done)
        {
            //Updating objective status in the objectives text----
            percentage = objectiveHp.health / objectiveHp.maximumHealth * 100;
            percentage = Mathf.Round(percentage);
            objectives.text = "Objectives:\n- Destroy all enemies.\n- Protect the allied aircraft carrier.\n- Hull integrety: " + percentage + "%";
        }

        listOfEnemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
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
        //If allied carrier is dead----
        else if (!alliedCarrier.activeSelf)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failText.text = "Your allied aircraft carrier was destroyed.";
                failedMenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
        //If all enemies are dead and the waves are done spawning----
        else if (listOfEnemies.Count == 0 && wavesSpawned == waves.Count)
        {
            //End of game dialogue canvas----
            if (!canvas2done)
            {
                canvas2done = true;
                canvas2.SetActive(true);
                Time.timeScale = 0;
                playerUi.SetActive(false);
            }
            //Ending game after delay----
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
                controller.level5Completed.VerifyBool(controller.pLevel5Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel5Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level5Completed.Reset(controller.pLevel5Completed);

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

