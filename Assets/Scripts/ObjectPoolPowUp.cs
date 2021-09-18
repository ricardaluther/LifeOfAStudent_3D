using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolPowUp : MonoBehaviour
{

    public static ObjectPoolPowUp SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> objectsToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectsToPool[UnityEngine.Random.Range(0, 6)]);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject()
     {
         for(int i = 0; i < amountToPool; i++)
         {
             if(!pooledObjects[i].activeInHierarchy)
             {
                 return pooledObjects[i];
             }
         }
         return null;
     }
}
