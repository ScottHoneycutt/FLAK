using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //Public variables----
    public GameObject radarSphere;
    public GameObject uiPanel;

    //Dictionary for storing pool queues----
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public Dictionary<string, Queue<RectTransform>> uiPoolDictionary;

    //ObjectPoolManager instance----
    public static ObjectPoolManager manager;

    //Making a pool class to contain object queues by type----
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //Making a uipool class to contain UI elements----
    [System.Serializable]
    public class UIPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //List of pools for easy pool construction in the inspector----
    public List<Pool> pools;
    public List<UIPool> uiPools;

    //Awake for making the instance of ObjectpoolManager----
    void Start()
    {
        manager = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        uiPoolDictionary = new Dictionary<string, Queue<RectTransform>>();

        //For each pool (sorted by object type)----
        foreach(Pool pool in pools)
        {
            //Making a queue for each object created by the pool based upon size----
            Queue<GameObject> objectPool = new Queue<GameObject>();
            //Creating reference for removing UIs----

            //Set each element inactive usisng .SetActive----
            for (int i = 0; i < pool.size; i++)
            {
                //Creating each object and setting it inactive----
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                //Adding object to queue----
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        foreach (UIPool uipool in uiPools)
        {
            //Making a queue for each object created by the pool based upon size----
            Queue<RectTransform> elementPool = new Queue<RectTransform>();
            //Creating reference for removing UIs----
            Vector2 removalcoordinates = new Vector2(1500, 1500);

            //Set each element inactive by changing its position values----
            for (int i = 0; i < uipool.size; i++)
            {
                //Creating each object and setting it inactive----
                GameObject obj = Instantiate(uipool.prefab);
                RectTransform rect = obj.transform.GetChild(0).GetComponent<RectTransform>();
                rect.anchoredPosition = removalcoordinates;

                //Adding RectTransform to queue----
                elementPool.Enqueue(rect);
            }
            uiPoolDictionary.Add(uipool.tag, elementPool);
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        //Grabbing, enabling, and reconfiguring an object from the queue (go through dictionary to find pool, dequeue from there)----
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        //Re-queueing to allow for later reuse----
        poolDictionary[tag].Enqueue(objectToSpawn);

        //Returning objectToSpawn to allow for further modifications----
        return objectToSpawn;
    }

    //For UI elements that don't use standard position/rotation vectors----
    public RectTransform SpawnForUI(string tag)
    {
        //Resetting queue location----
        RectTransform objectRect = uiPoolDictionary[tag].Dequeue();

        //Re-queueing to allow for later reuse----
        uiPoolDictionary[tag].Enqueue(objectRect);
        //Setting parent of canvas part of prefab----
        if (objectRect.parent.parent != uiPanel.transform)
        {
            objectRect.parent.transform.SetParent(uiPanel.transform);
        }

        //Returning objectRect to allow for further modifications----
        return objectRect;
    }

    //For UI elements that don't use standard position/rotation vectors----
    public RectTransform SpawnForRadar(string tag)
    {
        //Resetting queue location----
        RectTransform objectRect = uiPoolDictionary[tag].Dequeue();

        //Re-queueing to allow for later reuse----
        uiPoolDictionary[tag].Enqueue(objectRect);
        //Setting parent of canvas part of prefab----
        if (objectRect.parent.parent != radarSphere.transform)
        {
            objectRect.parent.transform.SetParent(radarSphere.transform);
        }

        //Returning objectRect to allow for further modifications----
        return objectRect;
    }
}
