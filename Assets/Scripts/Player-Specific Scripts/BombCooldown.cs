using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCooldown : MonoBehaviour
{
    //Public vaiables----
    public Transform playerFighterTransform;
    public Rigidbody playerFighterRigidbody;
    //Normal bombs----
    public float sCooldown = 5;
    //Carpet bombs----
    public float sCarpetBombCooldown = 3;
    public int sMaxBombCount = 5;
    public float delayBetweenBombs = .5f;
    //Audio----
    public AudioSource source;
    public AudioClip fireClip;
    public AudioClip fireFailedClip;
    public float volume = .7f;

    //Anti-tampering variables----
    private SecretFloat cooldown;
    private SecretFloat carpetBombCooldown;
    private SecretInt maxBombCount;

    //private variables----
    //normal timer---
    private float timer1;
    //carpet bomb stuff----
    private float timer2;
    private int bombCount;
    private float delayTimer;

    //GameControl----
    private GameControl controller;

    private void Awake()
    {
        controller = GameControl.control;

        cooldown = new SecretFloat(sCooldown);
        carpetBombCooldown = new SecretFloat(sCarpetBombCooldown);
        maxBombCount = new SecretInt(sMaxBombCount);
    }

    private void Start()
    {
        timer1 = cooldown.GetFloat();
        timer2 = 0;
        bombCount = maxBombCount.GetInt();
        delayTimer = delayBetweenBombs;
    }

    // Update is called once per frame
    void Update()
    {
        //Bombs but not carpet bombs----
        if (controller.specialDeliveryOwned.VerifyBool(controller.pSpecialDeliveryOwned) && !controller.carpetBombingEquipped.VerifyBool(controller.pCarpetBombingEquipped))
        {
            timer1 += Time.deltaTime;
            //If cooldown is over and the player uses the ability by hitting either shift key----
            if (timer1 >= cooldown.GetFloat() && Input.GetKey(KeyCode.Space))
            {
                //Reset timer and drop bomb, have bomb inherit aircraft transform----
                ObjectPoolManager.manager.SpawnFromPool("Bombs", playerFighterTransform.position, playerFighterTransform.rotation);
                timer1 = 0;
                //Play sound from sound source----
                source.PlayOneShot(fireClip, volume);
            }
            //Playing sound if player tries to drop a bomb while it's on cooldown----
            else if (timer1 < cooldown.GetFloat() && (Input.GetKeyDown(KeyCode.Space)))
            {
                source.PlayOneShot(fireFailedClip, volume);
            }
        }

        //Carpet bombs----
        else if(controller.specialDeliveryOwned.VerifyBool(controller.pSpecialDeliveryOwned) && controller.carpetBombingEquipped.VerifyBool(controller.pCarpetBombingEquipped))
        {
            //Incrementing delay timer for between bombs----
            delayTimer += Time.deltaTime;

            //Incrementing timer for bomb replenishment----
            if (bombCount < maxBombCount.GetInt())
            {
                timer2 += Time.deltaTime;
            }
            //Adding one bomb to the stock with each cooldown reset----
            if (timer2 >= carpetBombCooldown.GetFloat())
            {
                timer2 = 0;
                bombCount++;
            }
            //Dropping bombs if they are in stock and if the delay is over----
            if(bombCount > 0 && delayTimer >= delayBetweenBombs && Input.GetKey(KeyCode.Space))
            {
                //Resetting delay and decreasing bomb stock by 1----
                delayTimer = 0;
                bombCount--;
                //Dropping bomb----
                ObjectPoolManager.manager.SpawnFromPool("Bombs", playerFighterTransform.position, playerFighterTransform.rotation);
                //Play sound from sound source----
                source.PlayOneShot(fireClip, volume);
            }
            //Playing sound if player tries to drop a bomb while it's on delay or while the stock is empty----
            else if ((bombCount == 0 || delayTimer < delayBetweenBombs) && (Input.GetKeyDown(KeyCode.Space)))
            {
                source.PlayOneShot(fireFailedClip, volume);
            }
        }
    }
}
