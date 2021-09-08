using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


//using Random = System.Random;

//spawn manager blueprint:
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemiesPrefabs;

    [SerializeField] 
    private List<GameObject> _powerUpPrefabs;


   //Spawn delay
   [SerializeField] 
   private float _delay = 2f;

   [SerializeField] 
   private float _delayPowUp = 3f;
    
   //Controls whether prefab instances are spawned
   //Question: what controls this? Player class? some central control class?
   public Boolean _spawnStuff;
   
    // Start is called before the first frame update
    void Start()
    {
        //to be replaced later...
        //_spawnStuff = false;
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowUpSystem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnSystem()
    {
        //Spawn prefab in an area:
        while(_spawnStuff)
        {
            Instantiate(_enemiesPrefabs[UnityEngine.Random.Range(0, 6)], new Vector3(
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(0f, 2f),
                    UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            yield return new WaitForSeconds(_delay);
        }
        Destroy(this.gameObject);
    }
    
    IEnumerator SpawnPowUpSystem()
    {
        //Spawn prefab in an area:
        while(_spawnStuff)
        {
            Instantiate(_powerUpPrefabs[UnityEngine.Random.Range(0, 6)]);
            yield return new WaitForSeconds(_delayPowUp);
        }
        Destroy(this.gameObject);
    }
}
