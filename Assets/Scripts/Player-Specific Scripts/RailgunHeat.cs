using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailgunHeat : MonoBehaviour
{
    //Public variables----
    public float cooldownRate = 10;
    public float heatThreshold = 30;
    public GameObject uiHeatMeter;
    public GameObject uiHeat;
    public Image uiHeatMeterImg;
    public Image uiHeatImg;
    public ParticleSystem cannonFlash;
    public ParticleSystem cannonSmoke;
    public Transform railgunSpawn;

    //Private variables----
    private float heatUpRate = 15;
    private bool isOverHeated = false;
    private bool isCold = true;
    private bool firing = false;
    private bool overheatBool = true;
    private float fireTimer = 0;
    private ParticleSystem.MainModule cannonEmission;
    private ParticleSystem.MainModule cannonSmokeEmission;

    //fire rate is determined by rate of muzzle flash----
    private float fireperiod;

    //Sound Variables----
    public AudioSource cannonSoundRegulator;
    public AudioClip overheatSignal;

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
        cooldownRate = cooldownRate * Mathf.Pow(1.03f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));
        heatThreshold = heatThreshold * Mathf.Pow(1.05f, controller.cannonLevel.VerifyInt(controller.pCannonLevel));

        //Applying tech upgrades----
        if (controller.tungstenBarrelsOwned.VerifyBool(controller.pTungstenBarrelsOwned))
        {
            heatThreshold = heatThreshold * 1.12f;
        }
        if (controller.copperHeatSinksOwned.VerifyBool(controller.pCopperHeatSinksOwned))
        {
            cooldownRate = cooldownRate * 1.1f;
        }
        if (controller.markIVFeederOwned.VerifyBool(controller.pMarkIVFeederOwned))
        {
            //Heat threshhold is not altered for the railgun by M4 Feeder----
            //Fire rate----
            cannonEmission = cannonFlash.main;
            cannonEmission.duration = cannonEmission.duration * .83333f;
            cannonSmokeEmission = cannonSmoke.main;
            cannonSmokeEmission.duration = cannonSmokeEmission.duration * .83333f;

            fireperiod = cannonEmission.duration;
            fireTimer = fireperiod;
            //Audio play rate----
            cannonSoundRegulator.pitch = cannonSoundRegulator.pitch * 1.2f;
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
    void Update()
    {
        //incrementing fire timer----
        fireTimer += Time.deltaTime;


        //Workable variables----
        float realCooldown = cooldownRate * 154 / heatThreshold;
        float realHeatUpRate = heatUpRate * 154 / heatThreshold;

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

        //Cooling----
        if (!isCold && Time.timeScale == 1)
        {
            heatRectTransform.anchoredPosition = new Vector2(heatX - (realCooldown * Time.deltaTime), 0);
        }

        //Toggling firing variable and turning on/off sound and particle effects----
        if (isLeftMousePressed && !isOverHeated && Time.timeScale == 1)
        {
            //Start firing----
            firing = true;
        }

        //Turning off camera shake between shots----
        if (controller.railgunShootingBool && fireTimer > .15f)
        {
            controller.railgunShootingBool = false;
        }

        //Actually firing----
        if (firing)
        {
            if(fireTimer >= fireperiod)
            {
                //Restting timer----
                fireTimer = 0;

                //Firing projectile----
                ObjectPoolManager.manager.SpawnFromPool("Player Railgun Munitions", railgunSpawn.position, railgunSpawn.rotation);
               
                //Spiking Heat (UI)----
                heatRectTransform.anchoredPosition = new Vector2(heatX + realHeatUpRate, 0);

                if (!cannonFlash.isPlaying)
                {
                    //Particles----
                    cannonFlash.Play();
                    //Start sound----
                    cannonSoundRegulator.loop = true;
                    cannonSoundRegulator.Play();
                }
            
                //Start camera shake----
                controller.railgunShootingBool = true;
            }
        }

        //Beginning of one-time overheat bool----
        if (!isOverHeated)
        {
            overheatBool = true;
        }

        //Stop firing when mouse is not pressed----
        if ((firing && !isLeftMouseDown) || (overheatBool && isOverHeated))
        {
            cannonFlash.Stop();
            //Stop firing----
            firing = false;
            //Stop sound----
            cannonSoundRegulator.loop = false;
        }

        //End of one-time overheat----
        if (overheatBool && isOverHeated)
        {
            //Overheat sound----
            cannonSoundRegulator.PlayOneShot(overheatSignal, 1);
            overheatBool = false;
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
