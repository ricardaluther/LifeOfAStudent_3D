using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public AudioClip DrinkCoffee;

    public bool _caffeinated;

    public float _coffeetime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        _caffeinated = false;
        //die position wird ja at random festgelegt beim instantiaten
        //transform.position = new Vector3(Random.Range(-50f, 50f), 0.88f, Random.Range(-50f, 50f));
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0f,2f,0f, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student, start drinking the coffee
        if(other.CompareTag("Player"))
        {
            //TODO get energy points
            // sollen wir dein kaffe auch abschießen können?

            other.GetComponent<Player>().GetCaffeinated();
            AudioSource.PlayClipAtPoint(DrinkCoffee, transform.position);
			//before object pooling:
            //Destroy(this.gameObject);

			//after object pooling
			gameObject.SetActive(false);
        }
       
        
    }


}
