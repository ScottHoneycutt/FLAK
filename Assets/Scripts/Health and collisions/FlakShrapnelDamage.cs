using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakShrapnelDamage : MonoBehaviour {

    //Public variables----
    public float sShrapnelDamage = 1;
    public ParticleSystem shrapnel;

    //Anti-tampering measures----
    SecretFloat shrapnelDamage;

    //Sound files----
    public float volume = .5f;
    public AudioClip impact1;
    public AudioClip impact2;
    public AudioClip impact3;
    public AudioClip impact4;
    private List<AudioClip> audioList = new List<AudioClip>();

    //List of particle collision events----
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    //Void OnParticleCollision----
    void OnParticleCollision(GameObject impactTarget)
    {
        //Collision count and changing the list of particle collision events----
        int collisionCount = ParticlePhysicsExtensions.GetCollisionEvents(shrapnel, impactTarget, collisionEvents);


        //Running through the list of collision events----
        for (int i = 0; i < collisionCount; i++)
        {
            //If impacted object has health----
            if (impactTarget.GetComponent<UnitHP>() != null)
            {
                UnitHP healthScript = impactTarget.GetComponent<UnitHP>();
                //Reduce its health by shrapnelDamage----
                healthScript.health = healthScript.health - shrapnelDamage.GetFloat();
            }
            //Play random impact sound if bullet hits Player Fighter----
            if (impactTarget.name == "Player Fighter")
            {
                int selector = Random.Range(0, 4);
                //Referencing main camera for audiosource----
                AudioSource source = Camera.main.GetComponent<AudioSource>();
                source.PlayOneShot(audioList[selector], volume);
            }
        }
    }

    //Anti-tampering measures----
    private void Awake()
    {
        shrapnelDamage = new SecretFloat(sShrapnelDamage);
    }

    // Use this for initialization
    void Start()
    {
        //filling audiolist with audio----
        audioList.Add(impact1);
        audioList.Add(impact2);
        audioList.Add(impact3);
        audioList.Add(impact4);
    }
}
