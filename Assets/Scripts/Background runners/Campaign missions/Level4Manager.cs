using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Manager : MonoBehaviour
{
    public int pCreditsReward = 120;
    public int pSuppliesReward = 20;

    public float endDelay = 2;
    public float missionEndX = 2000;
    public GameObject failedMenu;
    public GameObject victoryMenu;
    public GameObject playerUi;
    public Text failText;
    public Text objectives;

    //Canvas variables----
    public float canvas1Delay = 2f;
    public GameObject canvas1;
    private bool canvas1Done = false;

    public float canvas2Distance = 700;
    public GameObject canvas2;
    private bool canvas2Done = false;

    public float canvas3Distance = 800;
    public GameObject canvas3;
    private bool canvas3Done = false;

    public float canvas4Distance = 800;
    public GameObject canvas4;
    private bool canvas4Done = false;

    public float canvas5Delay = 4;
    public GameObject canvas5;
    private bool canvas5Done = false;
    private float canvas5Timer = 0;

    public float canvas6Delay = 4;
    public GameObject canvas6;
    private bool canvas6Done = false;
    private float canvas6Timer = 0;

    //Anti-tampering variables----
    private ProtectedInt creditsReward;
    private ProtectedInt suppliesReward;

    //Player and objective units----
    public GameObject player;
    public Transform playerTransform;
    public GameObject enemyFighter;
    public Transform enemyFighterTransform;
    public GameObject enemyPatrolBoat1;
    public Transform enemyPatrolBoatTransform;
    public GameObject enemyPatrolBoat2;
    public GameObject enemyDestroyer;
    public Transform enemyDestroyerTransform;
    public GameObject enemyfleet;

    public AlliedFighterMovement alpine;
    public AlliedFighterMovement javelin;
    public GameObject endPatrol;

    private bool gameOver = false;
    private float timer = 0;
    private float timer2 = 0;
    private float timer3 = 0;
    private float timer4 = 0;
    private bool fighterDestroyed = false;
    private bool patrolBoatsDestroyed = false;

    //Objectives text----
    private float countdownTimer = 0;
    private int countdownMinutes = 0;
    private int countdownSeconds = 0;

    //Anti-tampering measures----
    private void Awake()
    {
        suppliesReward = new ProtectedInt(pSuppliesReward);
        creditsReward = new ProtectedInt(pCreditsReward);
    }


    // Update is called once per frame
    void Update()
    {
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

        //Storyline----

        //Trigger dialogue canvas after a few seconds of start----
        if (timer2 >= canvas1Delay && !canvas1Done)
        {
            canvas1Done = true;
            Time.timeScale = 0;
            canvas1.SetActive(true);
            playerUi.SetActive(false);
        }
        else
        {
            timer2 += Time.deltaTime;
        }

        //Trigger second canvas once player is within range of enemy fighter----
        if(canvas1Done && !canvas2Done && Vector3.Distance(playerTransform.position, enemyFighterTransform.position) < canvas2Distance)
        {
            canvas2Done = true;
            Time.timeScale = 0;
            canvas2.SetActive(true);
            playerUi.SetActive(false);
            countdownTimer = 12;
        }
        //Detection countdown before mission fail (fighter) (only activate after second canvas)----
        if (countdownTimer > 0 && enemyFighter.activeSelf && canvas2Done)
        {
            countdownTimer -= Time.deltaTime;
            countdownMinutes = (int)countdownTimer / 60;
            countdownSeconds = (int)countdownTimer % 60;
            if (countdownSeconds < 10)
            {
                objectives.text = "Objectives:\n- Destroy the enemy fighter: " + countdownMinutes + ":0" + countdownSeconds;
            }
            else
            {
                objectives.text = "Objectives:\n- Destroy the enemy fighter: " + countdownMinutes + ":" + countdownSeconds;
            }
        }
        //End the game with a loss if the enemy fighter is still alive after countdown ends----
        else if (countdownTimer <= 0 && enemyFighter.activeSelf && canvas2Done)
        {
            timer4 += Time.deltaTime;
            if (timer4 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failedMenu.SetActive(true);
                failText.text = "The enemy fleet was alerted of your presence.";
                playerUi.SetActive(false);
            }
        }
        //If the fighter is destroyed, move on (activate only once)----
        else if (!enemyFighter.activeSelf && !fighterDestroyed)
        {
            fighterDestroyed = true;
            objectives.text = "Objectives:\n- Scout the area.";
        }

        //Trigger third canvas once player is within range of patrol boats and enemy fighter is destroyed----
        if (!canvas3Done && fighterDestroyed && Vector3.Distance(playerTransform.position, enemyPatrolBoatTransform.position) <= canvas3Distance)
        {
            canvas3Done = true;
            Time.timeScale = 0;
            canvas3.SetActive(true);
            playerUi.SetActive(false);
            countdownTimer = 19;
        }
        //Detection countdown before mission fail (patrol boats) (only activate after third canvas)----
        if (countdownTimer > 0 && (enemyPatrolBoat1.activeSelf || enemyPatrolBoat2.activeSelf) && canvas3Done)
        {
            countdownTimer -= Time.deltaTime;
            countdownMinutes = (int)countdownTimer / 60;
            countdownSeconds = (int)countdownTimer % 60;
            if (countdownSeconds < 10)
            {
                objectives.text = "Objectives:\n- Destroy the enemy patrol boats: " + countdownMinutes + ":0" + countdownSeconds;
            }
            else
            {
                objectives.text = "Objectives:\n- Destroy the enemy patrol boats: " + countdownMinutes + ":" + countdownSeconds;
            }
        }
        //End the game with a loss if the enemy patrol boats are still alive after countdown ends----
        else if (countdownTimer <= 0 && (enemyPatrolBoat1.activeSelf || enemyPatrolBoat2.activeSelf) && canvas2Done)
        {
            timer4 += Time.deltaTime;
            if (timer4 >= endDelay && !gameOver)
            {
                Time.timeScale = 0;
                gameOver = true;
                failedMenu.SetActive(true);
                failText.text = "The enemy fleet was alerted of your presence.";
                playerUi.SetActive(false);
            }
        }
        //If the patrol boats are destroyed, move on (activate only once)----
        else if (!enemyPatrolBoat1.activeSelf && !enemyPatrolBoat2.activeSelf && !patrolBoatsDestroyed)
        {
            patrolBoatsDestroyed = true;
            objectives.text = "Objectives:\n- Scout the area.";
        }

        //Trigger fourth canvas once player is within range of destroyer and patrol boats are destroyed----
        if (!canvas4Done && patrolBoatsDestroyed && Vector3.Distance(playerTransform.position, enemyDestroyerTransform.position) <= canvas3Distance)
        {
            canvas4Done = true;
            Time.timeScale = 0;
            canvas4.SetActive(true);
            playerUi.SetActive(false);
            objectives.text = "Objectives:\n- Destroy the isolated frigate.";
            //Setting the fleet active to start spawning fighters----
            enemyfleet.SetActive(true);
        }

        //Delay before canvas 5----
        if(canvas4Done && !canvas5Done && canvas5Timer >= canvas5Delay)
        {
            //Setting canvas 5 active----
            canvas5Done = true;
            Time.timeScale = 0;
            canvas5.SetActive(true);
            playerUi.SetActive(false);
        }
        //Incrementing canvas5Timer after canvas 4 is done (delay)----
        else if (canvas4Done && !canvas5Done)
        {
            canvas5Timer += Time.deltaTime;
        }

        //Canvas 6 after destroyer is dead (with delay)----
        if(!canvas6Done && !enemyDestroyer.activeSelf && canvas6Timer >= canvas6Delay)
        {
            //Setting canvas 6 active----
            canvas6Done = true;
            Time.timeScale = 0;
            canvas6.SetActive(true);
            playerUi.SetActive(false);
            objectives.text = "Objectives:\n- Leave the area.";
            //Changing patrol points for campaign allies----
            alpine.patrolPoint1 = endPatrol;
            alpine.patrolPoint2 = endPatrol;
            javelin.patrolPoint1 = endPatrol;
            javelin.patrolPoint2 = endPatrol;
        }
        //Incrementing canvas5Timer after canvas 4 is done (delay)----
        else if (!enemyDestroyer.activeSelf && !canvas6Done)
        {
            canvas6Timer += Time.deltaTime;
        }



        //If all objectives are destroyed and the player has fled, end the game----
        if (!enemyFighter.activeSelf && !enemyPatrolBoat1.activeSelf && !enemyPatrolBoat2.activeSelf && !enemyDestroyer.activeSelf && playerTransform.position.x <= missionEndX)
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
                controller.level4Completed.VerifyBool(controller.pLevel4Completed);

                //Altering protected values----
                controller.pSupplies += suppliesReward.VerifyInt(pSuppliesReward);
                controller.pCredits += creditsReward.VerifyInt(pCreditsReward);
                controller.pLevel4Completed = true;

                //Resetting protectors----
                controller.supplies.Reset(controller.pSupplies);
                controller.credits.Reset(controller.pCredits);
                controller.level4Completed.Reset(controller.pLevel4Completed);

                controller.Save();
            }
        }
    }
}
