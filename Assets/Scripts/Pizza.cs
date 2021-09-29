using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public AudioClip Eat;

	//variables for which floor it should be spawned on
	[SerializeField] private float floor = 7f;
	[SerializeField] private float randFloor; 
	private float randX;
	private float randZ;
    
    // Start is called before the first frame update
    void Start()
    { 
		randFloor = Random.Range(0f, 3f);
		randX = Random.Range(-27f, 25f);
		randZ = Random.Range(-21f, 110f);
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

		//rewrite x and z if it would be in the garden section
		while((randX > -13f && randX < 13f) && (randZ < 100f && randZ > 36f))
		{
			randX = Random.Range(-27f, 25f);
			randZ = Random.Range(-21f, 110f);
		}
		//put in on ground floor if its in the hall
        if((randX > -27f && randX < 25f) && (randZ < 24.5f && randZ > -3.8f))
        {
            floor = 0.88f;
        }

        transform.position = new Vector3(randX, floor, randZ);
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
