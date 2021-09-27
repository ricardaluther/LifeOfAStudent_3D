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
    private List<GameObject> _powerUpPrefabs;

    [SerializeField]
    private List<GameObject> _enemiesPrefabs;

   //Spawn delay
   [SerializeField] 
   private float _delay = 2f;

   [SerializeField] 
   private float _delayPowUp = 3f;

   [SerializeField] 
   public ThrowingStuff ThrowingStuff;

   
    
   //Controls whether prefab instances are spawned
   //Question: what controls this? Player class? some central control class?
   public Boolean _spawnStuff;
   
    // Start is called before the first frame update
    void Start()
    {
        //to be replaced later...
        _spawnStuff = true;
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowUpSystem());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 spawnLocation()
    {
        return new Vector3(
            UnityEngine.Random.Range(-10f, 8f),
            UnityEngine.Random.Range(1f, 2f),
            UnityEngine.Random.Range(-9f, 24f));
        
    }

    IEnumerator SpawnSystem()
    {
        //Spawn prefab in an area:
        while(_spawnStuff)
        {
            //Instantiate(_enemiesPrefabs[UnityEngine.Random.Range(0, 6)], spawnLocation(),Quaternion.identity);
            GameObject _enemies = ObjectPool.SharedInstance.GetPooledObject();
            if (_enemies != null)
            {
                _enemies.transform.position = spawnLocation();
                _enemies.transform.rotation = Quaternion.identity;
                _enemies.SetActive(true);
            }

            yield return new WaitForSeconds(_delay);
        }

        gameObject.SetActive(false);

    }

    IEnumerator SpawnPowUpSystem()
    {
        //Spawn prefab in an area:
        while(_spawnStuff)
        {
            //Instantiate(_powerUpPrefabs[UnityEngine.Random.Range(0, 6)], spawnLocation(),Quaternion.identity);
            GameObject _powerUps = ObjectPoolPowerUp.SharedInstance.GetPooledObject();
            if (_powerUps != null)
            {
                _powerUps.transform.position = spawnLocation();
                _powerUps.transform.rotation = Quaternion.identity;
                _powerUps.SetActive(true);
            }

            yield return new WaitForSeconds(_delayPowUp);
        }

        gameObject.SetActive(false);
        //Spawn prefab in an area:
       
    }

    public void onPlayerDeath()
    {
        _spawnStuff = false;
        ThrowingStuff.GetComponent<ThrowingStuff>().enabled = false;
        
    }
    
    
}
