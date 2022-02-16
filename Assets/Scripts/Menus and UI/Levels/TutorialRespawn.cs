using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRespawn : MonoBehaviour
{
    public GameObject playerFighter;
    public Transform playerTransform;
    public UnitHP playerHp;
    public TutorialObjectives control;

    public void Respawn()
    {
        //Reset fighter----
        playerTransform.Translate(new Vector3(0, 250, 0), Space.World);
        playerHp.health = playerHp.maximumHealth;
        playerHp.deathRegister = false;
        playerFighter.SetActive(true);
        //Make the tutorial "failable" again----
        control.RespawnReset();
    }
}
