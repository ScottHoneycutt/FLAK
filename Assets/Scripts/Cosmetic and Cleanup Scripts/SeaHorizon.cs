using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaHorizon : MonoBehaviour
{
    public GameObject playerFighter;

    //Caching transforms----
    private Transform fighterTransform;
    private Transform thisTransform;
        
    // Start is called before the first frame update
    void Awake()
    {
        //Caching transforms----
        fighterTransform = playerFighter.transform;
        thisTransform = transform;

        thisTransform.position = new Vector3(fighterTransform.position.x, fighterTransform.position.y - 1000, 1400);
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.position = new Vector3(fighterTransform.position.x, fighterTransform.position.y - 1050, 1400);
    }
}
