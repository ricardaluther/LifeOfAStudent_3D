using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//spawn manager blueprint:
public class SpawnManager : MonoBehaviour
{
   // [SerializeField]
   // private GameObject _somePrefab;

   //Spawn delay
   [SerializeField] 
   private float _delay = 2f;
    
   //Controls whether prefab instances are be spawned
   //Question: what controls this? Player class? some central control class?
   private Boolean _spawnStuff;
   
    // Start is called before the first frame update
    void Start()
    {
        //to be replaced later...
        _spawnStuff = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnSystem()
    {
        while (_spawnStuff)
        {
            //Spawn prefab:
           // Instantiate(_somePrefab, new Vector3(Random.Range(xRange),
           // Random.Range(yRange),
           // Random.Range(zRange), Quaternion.identity);
           //Delay:
           yield return new WaitForSeconds(_delay);
        }
    }
}
