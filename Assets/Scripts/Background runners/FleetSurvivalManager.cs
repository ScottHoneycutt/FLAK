using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FleetSurvivalManager : MonoBehaviour
{
    //Public variables----
    public float refreshDelay = 1;
    public float endDelay = 1;
    public Text objectives;
    public GameObject resultsMenu;
    public GameObject playerUi;
    public Text resultsText;

    public GameObject player;
    public GameObject destroyer1;
    public GameObject destroyer2;
    public GameObject carrier;

    //Waves----
    public List<GameObject> waves = new List<GameObject>();
    private GameObject waveToSpawn;

    //Private variables----
    private int pCurrentWave = 1;
    private bool spawningWave = false;
    private float timer = 0;
    private float endTimer = 0;
    private bool endOverride = false;
    private List<GameObject> enemies = new List<GameObject>();

    //Secret and protected values (anti-tampering measures)----
    private ProtectedInt currentWave;

    //Gamecontrol----
    GameControl controller;

    //Anti-tampering measures----
    private void Awake()
    {
        currentWave = new ProtectedInt(pCurrentWave);

        controller = GameControl.control;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Refreshing enemies----
        enemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();
        //instant refresh----
        timer = refreshDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //Refreshing enemies every refreshdelay----
        timer += Time.deltaTime;
        if (timer >= refreshDelay)
        {
            timer = 0;
            enemies = Identifiers.identifier.ReturnAlliedFlakMissileTargets();

            //Sending new waves of enemies if old wave is defeated and manager is no longer spawning waves----
            if (enemies.Count == 0 && !spawningWave)
            {
                //Verifying that the original and protected values match----
                pCurrentWave = currentWave.VerifyInt(pCurrentWave);
                //Incrementing both values simultaneously----
                pCurrentWave++;
                currentWave.Reset(pCurrentWave);
                spawningWave = true;
            }
        }

        //Actually managing what is and isn't sent with each wave and spawning the units in----
        if (spawningWave)
        {
            //Getting wave from list----
            if(waves.Count >= currentWave.VerifyInt(pCurrentWave))
            {
                waveToSpawn = waves[pCurrentWave - 1];
                //Setting it active----
                waveToSpawn.SetActive(true);
                spawningWave = false;
                //Updating Objectives to indicate wave----
                objectives.text = "Objectives:\n- Survive.\n- Protect the allied fleet.\nWave " + pCurrentWave;
            }
            //If all waves have been defeated, end the game----
            else
            {
                endOverride = true;
            }
        }

        //Mision fail (or end, rather)----
        if ((!carrier.activeSelf && !destroyer1.activeSelf && !destroyer2.activeSelf) || !player.activeSelf || endOverride)
        {
            //Delay for a second or two before finishing----
            endTimer += Time.deltaTime;
            if (endTimer >= endDelay)
            {
                endTimer = 0;

                //Adding resource rewards to GameControl proportional to the number of waves survived squared (after the third wave)----
                int creditsEarned = 4 * (int)Mathf.Pow(currentWave.VerifyInt(pCurrentWave) - 4, 2);
                int suppliesEarned = (int)Mathf.Pow(pCurrentWave - 4, 2);
                if (pCurrentWave <= 4)
                {
                    creditsEarned = 0;
                    suppliesEarned = 0;
                }

                //Checking for tampering----
                controller.credits.VerifyInt(controller.pCredits);
                controller.supplies.VerifyInt(controller.pSupplies);

                //Adding values----
                controller.pCredits += creditsEarned;
                controller.credits.Reset(controller.pCredits);
                controller.pSupplies += suppliesEarned;
                controller.supplies.Reset(controller.pSupplies);

                //Saving----
                controller.Save();

                //Timescale----
                Time.timeScale = 0;
                //UI updates and results display----
                playerUi.SetActive(false);
                resultsText.text = "Waves Survived: " + (pCurrentWave - 1) + "\nCredits Earned: " + creditsEarned + "\nSupplies Earned: " + suppliesEarned;
                resultsMenu.SetActive(true);
            }
        }
    }
}
