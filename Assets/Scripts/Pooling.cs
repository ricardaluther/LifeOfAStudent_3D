using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pooling : MonoBehaviour
{
    public static Pooling SharedInstance;
    public List<GameObject> pooledObjects;
    [SerializeField]
    public List<GameObject> objectsToPool;
    public int amountToPool;
    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
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
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
