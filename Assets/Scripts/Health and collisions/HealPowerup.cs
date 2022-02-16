using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerup : MonoBehaviour
{
    //Public variables----
    public float cooldown = 20;
    public BoxCollider colliderBox;
    public SpriteRenderer spriteRenderer;
    public Behaviour halo;
    public AudioSource sound;

    //private varibales---
    private float timer = 0;
    UnitHP healthStat;

    private void Update()
    {
        //Enable the powerup every cooldown----
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            halo.enabled = true;
            spriteRenderer.enabled = true;
            colliderBox.enabled = true;
        }
    }

    //Upon collision with the player----
    private void OnTriggerEnter(Collider objectCollision)
    {
        GameObject objectThatCollided = objectCollision.gameObject;
        if (objectThatCollided.name == "Player Fighter")
        {
            //Only set healthstat if it hasn't been assigned yet (increases efficiency)----
            if (!healthStat)
            {
                healthStat = objectThatCollided.GetComponent<UnitHP>();
            }
            //Only consume if player is below full health----
            if (healthStat.health < healthStat.maximumHealth)
            {
                //Setting health to full----
                healthStat.health = healthStat.maximumHealth;
                //Disabling powerup and resetting timer----
                timer = 0;
                halo.enabled = false;
                spriteRenderer.enabled = false;
                colliderBox.enabled = false;
                //Playing sound----
                sound.Play();
            }
        }
    }
}
