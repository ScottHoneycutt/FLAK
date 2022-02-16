using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonHeat : MonoBehaviour {

    //Public variables----
    public float cooldownRate = 10;
    public float heatThreshold = 30;
    public GameObject uiHeatMeter;
    public GameObject uiHeat;
    public Image uiHeatMeterImg;
    public Image uiHeatImg;
    public ParticleSystem cannonFiring;

    //Private variables----
    private float heatUpRate = 10;
    private bool isOverHeated = false;
    private bool isCold = true;
    private bool firing = false;
    private bool overheatBool = true;
    private float preventExploitTimer = 0;
    private ParticleSystem.EmissionModule cannonEmission;

    //Sound Variables----
    public AudioSource cannonSoundRegulator;
    public AudioClip overheatSignal;
    public AudioClip cannonWindDown;

    //Dump reserve variables----
    private float dumpHeatThreshhold;

    //GameControl----
    GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;
    }

    private void Start()
    {
        //Applying incremental upgrades----
        cooldownRate = cooldownRate* Mathf.Pow(1.03f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));
        heatThreshold = heatThreshold* Mathf.Pow(1.05f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));
        cannonEmission = cannonFiring.emission;

        //Applying tech upgrades----
        if (controller.tungstenBarrelsOwned.VerifyBool(controller.pTungstenBarrelsOwned))
        {
            heatThreshold *= 1.12f;
        }
        if (controller.copperHeatSinksOwned.VerifyBool(controller.pCopperHeatSinksOwned))
        {
            cooldownRate *= 1.1f;
        }
        if (controller.markIVFeederOwned.VerifyBool(controller.pMarkIVFeederOwned))
        {
            heatThreshold *= .95f;
            //Fire rate----
            cannonEmission.rateOverTime = cannonEmission.rateOverTime.constant * 1.2f;
            //Audio play rate----
            cannonSoundRegulator.pitch *= 1.2f;
        }

        //Reserving/dumping variables----
        dumpHeatThreshhold = heatThreshold;

        if (controller.desperationModuleOwned.VerifyBool(controller.pDesperationModuleOwned))
        {
            StartCoroutine("RefreshDesperationModule");
        }
    }

    IEnumerable RefreshDesperationModule()
    {
        while (true)
        {
            if (controller.criticalHealth)
            {
                heatThreshold = dumpHeatThreshhold * 1.4f;
            }
            else
            {
                heatThreshold = dumpHeatThreshhold;
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    // Update is called once per frame
    void Update () {
        //incrementing exploit timer----
        preventExploitTimer += Time.deltaTime;


        //Workable variables----
        float realCooldown = cooldownRate * 154 / heatThreshold;
        float realHeatUpRate = heatUpRate *154 / heatThreshold;

        //LMB Held down? (UI)----
        bool isLeftMouseDown = Input.GetMouseButton(0);

        //LMB initial press? (Particles)----
        bool isLeftMousePressed = Input.GetMouseButtonDown(0);
        
        //UI heat condition----
        RectTransform heatRectTransform = uiHeat.GetComponent<RectTransform>();
        Vector2 heatPosition = heatRectTransform.anchoredPosition;
        float heatX = heatPosition.x;

        //Is/is not cold----
        if (heatX <= -154)
        {
            isCold = true;
        }
        else if (heatX > -154)
        {
            isCold = false;
        }

        //Is overheated----
        if (heatX >= 0)
        {
            isOverHeated = true;

        }

        //Cold clears overheated status----
        if (isCold)
        {
            isOverHeated = false;
            if (isLeftMouseDown)
            {
                isLeftMousePressed = true;
            }
        }
        //Actual firing/not firing (Particles and effects)----
        if ((isLeftMousePressed && !isOverHeated && Time.timeScale == 1 && preventExploitTimer >= 1/cannonEmission.rateOverTime.constant) || (isLeftMouseDown && !isOverHeated && Time.timeScale == 1 && preventExploitTimer >= 1 / cannonEmission.rateOverTime.constant && !firing))
        {
            cannonFiring.Play();
            //Start firing----
            firing = true;
            //Start sound----
            cannonSoundRegulator.loop = true;
            cannonSoundRegulator.Play();
            //Start camera shake----
            controller.cannonShootingBool = true;
        }

        if (firing)
        {
            //Heating (UI)----
            heatRectTransform.anchoredPosition = new Vector2(heatX + (realHeatUpRate * Time.deltaTime), 0);
        }

        //Beginning of one-time overheat bool----
        if (!isOverHeated)
        {
            overheatBool = true;
        }

        if ((firing && !isLeftMouseDown) || (overheatBool && isOverHeated))
        {
            cannonFiring.Stop();
            //Stop firing----
            firing = false;
            //Stop sound----
            cannonSoundRegulator.loop = false;
            //Resetting exploit timer----
            preventExploitTimer = 0;

            if (cannonWindDown)
            {
                cannonSoundRegulator.PlayOneShot(cannonWindDown);
            }
            //Stop camera shake----
            controller.cannonShootingBool = false;
        }
        //End of one-time overheat----
        if (overheatBool && isOverHeated)
        {
            //Overheat sound----
            cannonSoundRegulator.PlayOneShot(overheatSignal, 1);
            overheatBool = false;
        }

        //Cooling----
        if ((!isLeftMouseDown || isOverHeated) && !isCold && Time.timeScale == 1)
        {
            heatRectTransform.anchoredPosition = new Vector2(heatX - (realCooldown * Time.deltaTime), 0);
        }

        if (overheatBool && isOverHeated)
        {
            cannonSoundRegulator.PlayOneShot(overheatSignal, 1);
            overheatBool = false;
        }

        //Color----
        float green = (float)3.227848101 * heatX;
        green = 0 - green;

        float red = (float)3.227848101 * heatX + 510;

        if (red > 255)
        {
            red = 255;
        }
        else if (red < 0)
        {
            red = 0;
        }

        if (green > 255)
        {
            green = 255;
        }
        else if (green < 0)
        {
            green = 0;
        }

        int redness = (int)red;
        int greenness = (int)green;

        uiHeatImg.color = new Color32((byte)redness, (byte)greenness, 0, 109);
        uiHeatMeterImg.color = new Color32((byte)redness, (byte)greenness, 0, 109);

    }
}
