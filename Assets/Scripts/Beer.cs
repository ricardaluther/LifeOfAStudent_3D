using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    private float _beerSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * _beerSpeed * Time.deltaTime);
        transform.Rotate(0f,3f,0f, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student, start drinking the wine bottle
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetDrunk();
            Destroy(this.gameObject);
            // some other form of punishment? withdraw points?
        }
        // if we collide with a weapon, the wine bottle gets destroyed since it is fairly small, your score gets +2
        else if (other.CompareTag("Weapon"))
        {
            Destroy(this.gameObject);
            // give points to player
        }
    }
}