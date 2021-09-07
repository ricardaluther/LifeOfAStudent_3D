using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public AudioClip MoneyABBA;

    private Vector3 moneyPos;
    private float _moneySpeed = 4f;
    // Start is called before the first frame update
    void Start()
    { 
        moneyPos = new Vector3(Random.Range(-50f, 50f), 1.4f, Random.Range(-50f, 50f));
        transform.position = moneyPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _moneySpeed * Time.deltaTime);
        transform.Rotate(0f,0.5f,0f, Space.World);
        moneyPos = transform.position;
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        //is necessary because I have rotated the object and if it moves forward it actually goes downward
        //moneyPos.y = 1.7f;
    
        //check x Boundary
        if (transform.position.x <= -49.5f)
        {
            moneyPos.x = -49.5f;
            transform.position = moneyPos;
        }
        else if (transform.position.x >= 49.5f)
        {
            moneyPos.x = 49.5f;
            transform.position = moneyPos;
        }
        
        //check z Boundary 
        if (transform.position.z <= -49.5f)
        {
            moneyPos.z = -49.5f;
            transform.position = moneyPos;
        }
        else if (transform.position.z >= 49.5f)
        {
            moneyPos.z = 49.5f;
            transform.position = moneyPos;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "got the money:D");
        // if the object we collide with is the student he gets the money
        if(other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(MoneyABBA, transform.position);
            Destroy(this.gameObject);
            // TODO add points to money bar
            
            
        }
       
        
    }
}
