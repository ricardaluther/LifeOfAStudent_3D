using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

//A class which allows the player to throw stuff.
public class ThrowingStuff : MonoBehaviour
{
    //Note: to add another prefab to the list, add a prefab variable below,
    //increase the amount variable by one and add the prefab variable
    //to the officeSupplies Array in the start function.
    //Then assign a prefab to the prefab variable using the unity editor.
    //Note: The amount of prefabs in the three tiers has to be kept the same.
    //Amount variable specifies the amount of prefabs in each of the three tiers.
    
    //different office supply prefabs in tier one:
    public GameObject t1officeSupply1;
    public GameObject t1officeSupply2;
    public GameObject t1officeSupply3;
    public GameObject t1officeSupply4;
    
    //different office supply prefabs in tier two:
    public GameObject t2officeSupply1;
    public GameObject t2officeSupply2;
    public GameObject t2officeSupply3;
    public GameObject t2officeSupply4;

    //different office supply prefabs in tier three:
    public GameObject t3officeSupply1;
    public GameObject t3officeSupply2;
    public GameObject t3officeSupply3;
    public GameObject t3officeSupply4;

    //amount of prefabs used for easy modification:
    private int amount = 4;
    
    //Point at which throwables are spawned:
    public Transform spawnPoint;
    
    //Throw speed:
    public float speed = 5f;
    
    //Max amount of throwables instantiated:
    public int limit;
    
    //Queues for storing them:
    private Queue<GameObject> _throwablesTierOne = new Queue<GameObject>();
    private Queue<GameObject> _throwablesTierTwo = new Queue<GameObject>();
    private Queue<GameObject> _throwablesTierThree = new Queue<GameObject>();
    
    private bool _launched;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] officeSuppliesTierOne = {t1officeSupply1, t1officeSupply2, t1officeSupply3, t1officeSupply4};
        GameObject[] officeSuppliesTierTwo = {t2officeSupply1, t2officeSupply2, t2officeSupply3, t2officeSupply4};
        GameObject[] officeSuppliesTierThree = {t3officeSupply1, t3officeSupply2, t3officeSupply3, t3officeSupply4};
        
        for (int i = 0; i < limit; i++)
        {
            int f = UnityEngine.Random.Range(0, amount - 1);
            GameObject anInstanceTone = Instantiate(officeSuppliesTierOne[f], spawnPoint.position, officeSuppliesTierOne[f].transform.rotation);
            GameObject anInstanceTtwo = Instantiate(officeSuppliesTierTwo[f], spawnPoint.position, officeSuppliesTierTwo[f].transform.rotation);
            GameObject anInstanceTthree = Instantiate(officeSuppliesTierThree[f], spawnPoint.position, officeSuppliesTierThree[f].transform.rotation);
            _throwablesTierOne.Enqueue(anInstanceTone);
            _throwablesTierTwo.Enqueue(anInstanceTtwo);
            _throwablesTierThree.Enqueue(anInstanceTthree);
            anInstanceTone.SetActive(false);
            anInstanceTtwo.SetActive(false);
            anInstanceTthree.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _launched = true;
        }
    }

    private void FixedUpdate()
    {
        if (_launched)
        {
            InitiateThrow();
        }
    }

    private void InitiateThrow()
    {
        if (Player.GetMoney() < Player.Tier1Border)
        {
            //Get the next instance from the queue:
            GameObject anInstance = _throwablesTierOne.Dequeue();
            Throw(anInstance);
            //recycling the object:
            _throwablesTierOne.Enqueue(anInstance);
        } else if (Player.GetMoney() < Player.Tier2Border)
        {
            //Get the next instance from the queue:
            GameObject anInstance = _throwablesTierTwo.Dequeue();
            Throw(anInstance);
            //recycling the object:
            _throwablesTierTwo.Enqueue(anInstance);
        }
        else
        {
            //Get the next instance from the queue:
            GameObject anInstance = _throwablesTierThree.Dequeue();
            Throw(anInstance);
            //recycling the object:
            _throwablesTierThree.Enqueue(anInstance);
        }

    }
    

    private void Throw(GameObject anInstance)
    {
                
        //position and rotation:
        anInstance.transform.position = new Vector3(spawnPoint.position.x,spawnPoint.position.y,spawnPoint.position.z);
        anInstance.transform.rotation = Quaternion.LookRotation(-spawnPoint.forward);
        anInstance.SetActive(true);
        
        //Movement
        Rigidbody aRigidBody = anInstance.GetComponent<Rigidbody>();
        aRigidBody.velocity = Vector3.zero;
        aRigidBody.angularVelocity = Vector3.zero;
        aRigidBody.AddForce(spawnPoint.forward * speed, ForceMode.Impulse);
        
        //Rotation stuff: (Maybe randomize this).
        aRigidBody.AddTorque(transform.right, ForceMode.Impulse);
        aRigidBody.AddTorque(transform.forward, ForceMode.Impulse);
        
        _launched = false;
    }
}
