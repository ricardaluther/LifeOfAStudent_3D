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
    
    //different office supply prefabs:
    public GameObject officeSupply1;
    public GameObject officeSupply2;
    public GameObject officeSupply3;
    public GameObject officeSupply4;
    //amount of prefabs used for easy modification:
    private int amount = 4;
    
    //Point at which throwables are spawned:
    public Transform spawnPoint;
    
    //Throw speed:
    public float speed = 5f;
    
    //Max amount of throwables instantiated:
    public int limit;
    
    //Queue for storing them:
    private Queue<GameObject> _throwables = new Queue<GameObject>();
    
    private bool _launched;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] officeSupplies = {officeSupply1, officeSupply2, officeSupply3, officeSupply4};
        
        for (int i = 0; i < limit; i++)
        {
            int f = UnityEngine.Random.Range(0, amount - 1);
            GameObject anInstance = Instantiate(officeSupplies[f], spawnPoint.position, officeSupplies[f].transform.rotation);
            _throwables.Enqueue(anInstance);
            anInstance.SetActive(false);
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
            Throw();
        }
    }

    private void Throw()
    {

        //Get the next instance from the queue:
        GameObject anInstance = _throwables.Dequeue();
        
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
        
        //recycling the object:
        _throwables.Enqueue(anInstance);
    }
}
