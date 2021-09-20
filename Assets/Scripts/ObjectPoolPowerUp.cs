using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolPowerUp : MonoBehaviour
{
    public static ObjectPoolPowerUp SharedInstance;

    public List<GameObject> pooledObjectsPU;

    public List<GameObject> puToPool;

    public int amountPUToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjectsPU = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountPUToPool; i++)
        {
            tmp = Instantiate(puToPool[UnityEngine.Random.Range(0,6)]);
            tmp.SetActive(false);
            pooledObjectsPU.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        if (pooledObjectsPU.Count > 0)
        {
            for(int i = 0; i < amountPUToPool; i++)
            {
                if (!pooledObjectsPU[i].activeInHierarchy)
                {
                    return pooledObjectsPU[i];
                }
            }
        }


        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}