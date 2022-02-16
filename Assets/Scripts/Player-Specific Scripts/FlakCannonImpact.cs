using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakCannonImpact : MonoBehaviour
{
    public ParticleSystem cannonFire;

    //List of particle collision events----
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    //Void OnParticleCollision----
    void OnParticleCollision(GameObject impactTarget)
    {
        //Getting rotation quaternion for prefab instantiation----
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        //Collision count and changing the list of particle collision events----
        int collisionCount = ParticlePhysicsExtensions.GetCollisionEvents(cannonFire, impactTarget, collisionEvents);


        //Running through the list of collision events----
        for (int i = 0; i < collisionCount; i++)
        {
            //Impact explosion instantiation----
            Vector3 impactLocation = collisionEvents[i].intersection;
            ObjectPoolManager.manager.SpawnFromPool("Player Flak Explosions", impactLocation, rotation);
        }
    }
}
