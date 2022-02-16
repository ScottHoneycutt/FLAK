using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedMissileHit : MonoBehaviour {
    //Public variables----
    public float sDamage = 25;

    //Anti-tampering measures----
    private SecretFloat damage;

    //Private variables---- 
    private bool expended = false;
    private UnitHP healthStat;

    private void Awake()
    {
        damage = new SecretFloat(sDamage);
    }

    private void Start()
    {
        healthStat = this.GetComponent<UnitHP>();
    }

    private void OnEnable()
    {
        expended = false;
    }

    private void OnTriggerEnter(Collider objectCollision)
    {
        //If the missile collides with an object on the "Enemy"/"Terrain" layer and it hasn't already collided...----
        if ((objectCollision.gameObject.layer == 8 || objectCollision.gameObject.layer == 18 || objectCollision.gameObject.layer == 19) && !expended)
        {
            //If it has a health stat----
            if (objectCollision.gameObject.GetComponent<UnitHP>())
            {
                //Deal damage----
                objectCollision.gameObject.GetComponent<UnitHP>().health -= damage.GetFloat();
            }
            //Die----
            healthStat.health = 0;
            expended = true;
        }
    }
}
