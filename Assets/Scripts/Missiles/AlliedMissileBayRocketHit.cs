using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedMissileBayRocketHit : MonoBehaviour
{
    //Public variables----
    public float sDamage = 20;

    //Sound variables----
    public AudioClip playerImpactSound;
    public float volume = .3f;
    public float collisionAllowDelay = .8f;

    //Anti-tampering measures----
    private SecretFloat damage;

    //Private variables----
    private bool expended = false;
    private UnitHP healthStat;
    private AudioSource source;
    private float timer = 0;
    private float afterburnDamage;
    private UnitHP targetHealth;

    private void Awake()
    {
        damage = new SecretFloat(sDamage);
    }

    private void Start()
    {
        //Referencing main camera for audiosource----
        source = Camera.main.GetComponent<AudioSource>();
        healthStat = this.GetComponent<UnitHP>();

        //Tech upgrades and damage reductions for player fighter----
        afterburnDamage = damage.GetFloat() * .7f;
    }

    //Resetting----
    private void OnEnable()
    {
        expended = false;
        timer = 0;
    }
    //Incrementing timer----
    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider objectCollision)
    {
        //If the missile collides with an object on the "Allied"/"Terrain" layer and it hasn't already collided...----
        if ((objectCollision.gameObject.layer == 8 || objectCollision.gameObject.layer == 18 || objectCollision.gameObject.layer == 19) && !expended)
        {
            //Only can be destroyed/detonated by collision if starting timer has completed----
            if (timer > collisionAllowDelay)
            {
                //If it has a health stat and didn't collide with the player----
                if (objectCollision.gameObject.GetComponent<UnitHP>())
                {
                    targetHealth = objectCollision.gameObject.GetComponent<UnitHP>();
                    //Deal damage----
                    targetHealth.health -= damage.GetFloat();
                }
                //Die----
                healthStat.health = 0;
                expended = true;
            }
        }
    }
}
