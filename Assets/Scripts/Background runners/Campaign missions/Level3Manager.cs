using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    public int pCreditsReward = 120;
    public int pSuppliesReward = 20;

    public float endDelay = 2;
    public float delayBetweenWaves = 15;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    public GameObject canvas;
    private bool canvasdone = false;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    //Waves----
    public GameObject alliedReinforcements;
    public List<GameObject> waves = new List<GameObject>();

    public GameObject player;
    public GameObject alliedDestroyer;
    public UnitHP objectiveHp;
    private float percentage = 100;

    private bool gameOver = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float waveTimer = 0;
    private int wavesSpawned = 0;

    //Objectives text----
    private float reinforcementTimer = 132;
    private int reinforcementMinutes = 0;
    private int reinforcementSeconds = 0;

    private List<GameObject> listOfEnemies = new List<GameObject>();

    //Anti-tampering measures----
    private void Awake()
    {
        suppliesReward = new ProtectedInt(pSuppliesReward);
        creditsReward = new ProtectedInt(pCreditsReward);
    }

    private void Start()
    {
        //prompting first wave immediately----
        waveTimer = delayBetweenWaves;
    }


    // Update is called once per frame
    void Update()
    {
        //Updating timer for objectives text----
        if (reinforcementTimer > 0)
        {
            reinforcementTimer -= Time.deltaTime;
            reinforcementMinutes = (int)reinforcementTimer / 60;
            reinforcementSeconds = (int)reinforcementTimer % 60;

        }
        //Dialogue once reinforcements arrive----
        else if(!canvasdone)
        {
            canvasdone = true;
            canvas.SetActive(true);
            playerUi.SetActive(false);
            Time.timeScale = 0;
        }

        //Updating objective status in the objectives text----
        percentage = objectiveHp.health / objectiveHp.maximumHealth * 100;
        percentage = Mathf.Round(percentage);
        //Adding the zero to the timer display when needed and adding in objective hp----
        if (reinforcementSeconds < 10)
        {
            objectives.text = "Objectives:\n- Protect the allied destroyer.\n- Hull integrety: " + percentage + "%\n- Reinforcements arrive in: " + reinforcementMinutes + ":0" + reinforcementSeconds;
        }
        else
        {
            objectives.text = "Objectives:\n- Protect the allied destroyer.\n- Hull integrety: " + percentage + "%\n- Reinforcements arrive in: " + reinforcementMinutes + ":" + reinforcementSeconds;
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
        //If all enemies are dead and the waves are done spawning----
        else if (listOfEnemies.Count == 0 && wavesSpawned == waves.Count)
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
                controller.level3Completed.VerifyBool(controller.pLevel3Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel3Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level3Completed.Reset(controller.pLevel3Completed);

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
            //Spawning allied reinforcments----
            if(wavesSpawned == 6)
            {
                alliedReinforcements.SetActive(true);
            }
            wavesSpawned++;
        }
    }
}
