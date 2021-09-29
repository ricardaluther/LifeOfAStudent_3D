using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public AudioClip Eat;
    
    // Start is called before the first frame update
    void Start()
    { 
        transform.position = new Vector3(Random.Range(-50f, 50f), 0.88f, Random.Range(-50f, 50f));
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0f,5f,0f, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student, start drinking the wine bottle
        if(other.CompareTag("Player"))
        {
            //TODO get food points
            Player.AddStress(-5);
            AudioSource.PlayClipAtPoint(Eat, transform.position);
			//before op
            //Destroy(this.gameObject);

			//after op
			gameObject.SetActive(false);
        }
        // if we collide with a weapon, the wine bottle gets destroyed since it is fairly small, your score gets +2
        
    }
}
