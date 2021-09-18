using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
	public AudioClip DrinkBeer;
	public AudioClip glassShatter;
    private float _beerSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    { 
 		transform.position = new Vector3(Random.Range(-50f, 50f), 0.88f, Random.Range(-50f, 50f));
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
			AudioSource.PlayClipAtPoint(DrinkBeer, transform.position);
            this.gameObject.SetActive(false);
            // some other form of punishment? withdraw points?
            Player.addStress(-5);
        }
        // if we collide with a weapon, the wine bottle gets destroyed since it is fairly small, your score gets +2
        else if (other.CompareTag("Projectile"))
        {
			AudioSource.PlayClipAtPoint(glassShatter, transform.position);
            this.gameObject.SetActive(false);
            // give points to player
        }
    }
}
