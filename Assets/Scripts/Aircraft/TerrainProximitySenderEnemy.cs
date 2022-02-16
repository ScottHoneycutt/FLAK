using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainProximitySenderEnemy : MonoBehaviour
{
    public EnemyFighterMovement fighterMovement;
    //When the hitbox intersects with terrain----
    private void OnTriggerEnter(Collider other)
    {
        fighterMovement.NotifyGroundClose(true);
    }
    private void OnTriggerExit(Collider other)
    {
        fighterMovement.NotifyGroundClose(false);
    }
}
