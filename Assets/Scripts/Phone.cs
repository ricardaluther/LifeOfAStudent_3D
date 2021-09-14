using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
	public AudioClip phoneShatter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetDistracted();
            Destroy(this.gameObject); 
            
        }
        else if (other.CompareTag("Projectile"))
        {
			AudioSource.PlayClipAtPoint(phoneShatter, transform.position);
            Destroy((this.gameObject));
        }
    }
}
