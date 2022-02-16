using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour
{
    public GameObject bomb;
    public Rigidbody bombRigidBody;

    public float maxFallSpeed = 200;
    public float gravitySpeed = 50;
    public float airResistenceSpeed = 20;

    // Update is called once per frame
    void Start()
    {
        //Make the bomb look in the direction it is flying----
        bomb.transform.LookAt(bomb.transform.position + bombRigidBody.velocity);
        //Setting bomb velocity vectors, inherited from player fighter----
        bombRigidBody.velocity = Identifiers.identifier.GetPlayerVelocity();
    }
    private void OnEnable()
    {
        //Make the bomb look in the direction it is flying----
        bomb.transform.LookAt(bomb.transform.position + bombRigidBody.velocity);

        //Setting bomb velocity vectors, inherited from player fighter----
        //verifying that the identifiers have been instantiated before trying to reference them----
        if (Identifiers.identifier)
        {
            bombRigidBody.velocity = Identifiers.identifier.GetPlayerVelocity();
        }
    }

    private void FixedUpdate()
    {
        //Gravity----
        if (bombRigidBody.velocity.y <= -maxFallSpeed)
        {
            bombRigidBody.velocity = new Vector3(bombRigidBody.velocity.x, -maxFallSpeed, 0);
        }
        else
        {
            bombRigidBody.velocity = new Vector3(bombRigidBody.velocity.x, bombRigidBody.velocity.y - gravitySpeed*Time.deltaTime, 0);
        }

        //Air Resistence----
        if (!(bombRigidBody.velocity.x > Time.deltaTime * airResistenceSpeed || bombRigidBody.velocity.x < -Time.deltaTime * airResistenceSpeed))
        {
            //No change---
        }
        else if (bombRigidBody.velocity.x > 0)
        {
            bombRigidBody.velocity = new Vector3(bombRigidBody.velocity.x - airResistenceSpeed * Time.deltaTime, bombRigidBody.velocity.y, 0);
        }
        else if (bombRigidBody.velocity.x < 0)
        {
            bombRigidBody.velocity = new Vector3(bombRigidBody.velocity.x + airResistenceSpeed * Time.deltaTime, bombRigidBody.velocity.y, 0);
        }

        //Make the bomb look in the direction it is flying----
        bomb.transform.LookAt(bomb.transform.position + bombRigidBody.velocity);
    }
}
