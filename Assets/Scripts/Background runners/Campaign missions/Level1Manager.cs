using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    //Public variables----
    public int pCreditsReward = 100;
    public int pSuppliesReward = 15;

    public float endDelay = 2;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text Objectives;

    public GameObject player;
    public Transform playerTransform;
    public Transform destroyer;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    //Storyline canvases----
    public float canvas1Distance = 1200;
    public GameObject canvas1;
    private bool canvas1done = false;

    public float canvas2Distance = 800;
    public GameObject canvas2;
    private bool canvas2done = false;

    public float delayBeforeCanvas3 = 3;
    private float canvas3Timer = 0;
    public GameObject canvas3;
    private bool canvas3done = false;

    public float delayBeforeCanvas4 = 5f;
    public float canvas4Distance = 400;
    private float canvas4Timer = 0;
    public GameObject canvas4;
    private bool canvas4done = false;

    //Neutral/enemy management for destroyer----
    private bool converted = false;
    private bool flipFlag = false;
    public List<GameObject> neutralTargeters = new List<GameObject>();
    public List<GameObject> enemyTargeters = new List<GameObject>();
    public List<UnitHP> neutralHps = new List<UnitHP>();
    private List<GameObject> listOfEnemies = new List<GameObject>();

    //Prevent Alpine and Javelin from firing until destroyer is hostile----
    public GameObject alpineFireControl;
    public GameObject javelinFireControl;

    //private variables----
    private float timer1 = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private bool gameOver = false;
    private bool gameEnding = false;
    private bool supposedToFlip = false;

    //Anti-tampering measures----
    private void Awake()
    {
        creditsReward = new ProtectedInt(pCreditsReward);
        suppliesReward = new ProtectedInt(pSuppliesReward);
    }

    private void Start()
    {
        //Setting up neutral targeters on the destroyer----
        foreach(GameObject targeter in neutralTargeters)
        {
            Identifiers.identifier.ReportNeutralTargeter(targeter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If ship is still neutral----
        if (!converted)
        {
            //Checking to see if the neutrality of the single destroyer should flip----
            foreach (UnitHP hpStat in neutralHps)
            {
                if (hpStat.health != hpStat.maximumHealth)
                {
                    flipFlag = true;
                }
                //Enforcing EMP status while neutral----
                hpStat.empStart = true;
            }
            //Disabling neutralTargeters and enabling enemyTargeters when flipflag has been triggered----
            if (flipFlag || supposedToFlip)
            {
                foreach (GameObject targeter in neutralTargeters)
                {
                    Identifiers.identifier.RemoveNeutralTargeter(targeter);
                }
                foreach (GameObject targeter in enemyTargeters)
                {
                    Identifiers.identifier.ReportEnemyFlakMissileTargeter(targeter);
                }
                //Ending EMP status when neutrality ends----
                foreach (UnitHP hpStat in neutralHps)
                {
                    hpStat.isEmped = false;
                    hpStat.empStart = false;
                }
                //Ending checks by changing converted to true----
                converted = true;
            }
        }

        //If player is dead and the mission hasn't already ended----
        if (!player.activeSelf && !gameOver)
        {
            gameEnding = true;
            //Start endDelay countdown----
            timer3 += Time.deltaTime;
            if (timer3 >= endDelay)
            {
                //End game with mission failed screen----
                gameOver = true;
                Time.timeScale = 0;
                playerUi.SetActive(false);
                failedMenu.SetActive(true);
                //Set explanation text----
                failText.text = "Your aircraft was destroyed.";
            }
        }

        //Once ship has become hostile, check if...----
        if (converted)
        {
            //Re-enabling fire control system for Alpine and Javelin----
            alpineFireControl.SetActive(true);
            javelinFireControl.SetActive(true);

            //Refilling list of enemies----
            listOfEnemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
            //...orders were disobeyed (mission fail if game has not already ended)----
            if (!supposedToFlip && !gameOver)
            {
                gameEnding = true;
                //Start endDelay countdown----
                timer2 += Time.deltaTime;
                if (timer2 >= endDelay)
                {
                    //End game with mission failed screen----
                    gameOver = true;
                    Time.timeScale = 0;
                    playerUi.SetActive(false);
                    failedMenu.SetActive(true);
                    //Set explanation text----
                    failText.text = "You disobeyed orders.";
                }
            }
            //...all enemies are destroyed (mission accomplished if game has not already ended)----
            else if (listOfEnemies.Count == 0 && !gameOver)
            {
                //Start endDelay countdown----
                timer1 += Time.deltaTime;
                if (timer1 >= endDelay)
                {
                    //End mission with a mission accomplished menu----
                    gameOver = true;
                    Time.timeScale = 0;
                    playerUi.SetActive(false);
                    victoryMenu.SetActive(true);
                    //Updating GameControl----
                    GameControl controller = GameControl.control;

                    //Verifying that values have not been tampered with----
                    controller.supplies.VerifyInt(controller.pSupplies);
                    controller.credits.VerifyInt(controller.pCredits);
                    controller.level1Completed.VerifyBool(controller.pLevel1Completed);

                    //Altering protected values----
                    controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                    controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                    controller.pLevel1Completed = true;

                    //Resetting protectors----
                    controller.supplies.Reset(controller.pSupplies);
                    controller.credits.Reset(controller.pCredits);
                    controller.level1Completed.Reset(controller.pLevel1Completed);

                    controller.Save();

                }
            }
            //...enemy targeters are still active when they should not be----
            for (int i = 0; i < listOfEnemies.Count; i ++)
            {
                if (!listOfEnemies[i].activeInHierarchy)
                {
                    //Turning targeter off by removing it from identifier lists----
                    Identifiers.identifier.RemoveEnemyFlakMissileTargeter(listOfEnemies[i]);
                }
            }
        }

        //Story stuff----
        //Canvas1 once within certain range----
        if (!canvas1done && Vector3.Distance(playerTransform.position, destroyer.position) <= canvas1Distance && !gameEnding)
        {
            canvas1done = true;
            canvas1.SetActive(true);
            Time.timeScale = 0;
        }
        //Canvas2 once within certain range----
        if(!canvas2done && Vector3.Distance(playerTransform.position, destroyer.position) <= canvas2Distance && !gameEnding)
        {
            canvas2done = true;
            canvas2.SetActive(true);
            Time.timeScale = 0;
        }
        //Canvas3 once delay ends----
        if (!canvas3done && canvas2done && !gameEnding)
        {
            canvas3Timer += Time.deltaTime;
            if(canvas3Timer >= delayBeforeCanvas3)
            {
                canvas3done = true;
                canvas3.SetActive(true);
                Time.timeScale = 0;
            }
        }
        //Canvas4 once delay ends----
        if (!canvas4done && canvas3done && !gameEnding)
        {
            canvas4Timer += Time.deltaTime;
            if (canvas4Timer >= delayBeforeCanvas4 && Vector3.Distance(playerTransform.position, destroyer.position) <= canvas4Distance)
            {
                canvas4done = true;
                canvas4.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    //Method to end neutrality for the story (not prematurely triggered by the player----
    public void EndDestroyerNeutrality()
    {
        supposedToFlip = true;
        Objectives.text = "Objectives: \n- Destroy all enemies.";
    }
}
