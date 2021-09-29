using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public AudioClip MoneyABBA;

    private Vector3 moneyPos;
    private float _moneySpeed = 4f;
    
    //variables for which floor it should be spawned on
    [SerializeField] private float floor = 7f;
    [SerializeField] private float randFloor; 
    
    // Start is called before the first frame update
    void Start()
    { 
        randFloor = Random.Range(0f, 3f);
        
        //decide which floor the thing should be spawned on...floor is going to be the y-coordinate
        if (randFloor < 1)
        {
            floor = 0.88f;
        }
        else if (randFloor < 2)
        {
            floor = 7.01f;
        }
        else
        {
            floor = 10f;
        }
        
        moneyPos = new Vector3(Random.Range(-27f, 25f), floor, Random.Range(-21f, 110f));
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
        if (transform.position.x <= -26.5f)
        {
            moneyPos.x = -26.5f;
            transform.position = moneyPos;
        }
        else if (transform.position.x >= 24.5f)
        {
            moneyPos.x = 24.5f;
            transform.position = moneyPos;
        }
        
        //check z Boundary 
        if (transform.position.z <= -21.0f)
        {
            moneyPos.z = -21.0f;
            transform.position = moneyPos;
        }
        else if (transform.position.z >= 109.5f)
        {
            moneyPos.z = 109.5f;
            transform.position = moneyPos;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "got the money:D");
        // if the object we collide with is the student he gets the money
        if(other.CompareTag("Player"))
        {
            Player.AddMoney(1000);
            AudioSource.PlayClipAtPoint(MoneyABBA, transform.position);
			//before object pooling
            //Destroy(this.gameObject);

			//after object pooling
			gameObject.SetActive(false);
            // TODO add points to money bar
            
            
        }
       
        
    }
}
