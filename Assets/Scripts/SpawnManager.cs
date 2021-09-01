using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = System.Random;

//spawn manager blueprint:
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _somePrefab;

   //Spawn delay
   [SerializeField] 
   private float _delay = 2f;
    
   //Controls whether prefab instances are spawned
   //Question: what controls this? Player class? some central control class?
   public Boolean _spawnStuff;
   
    // Start is called before the first frame update
    void Start()
    {
        //to be replaced later...
        _spawnStuff = false;
        StartCoroutine(SpawnSystem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnSystem()
    {
        while (true)
        {
            //Spawn prefab in an area:
            if (_spawnStuff)
            {
                Instantiate(_somePrefab, new Vector3(UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(0f, 20f),
                    UnityEngine.Random.Range(-10f,10f)), Quaternion.identity);
            }
            //Delay:
           yield return new WaitForSeconds(_delay);
        }
    }
}
