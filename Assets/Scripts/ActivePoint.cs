using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePoint : MonoBehaviour
{
    public CapsuleCollider col;
    public AudioClip ActivePointSound;
    
    [SerializeField] private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();

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
            Debug.LogWarning("Character activated ActivePoint:D...let the games begin");
            
			_player.LocationChange(-1.0f, 0.0f, -15.0f); //calls the Player Script and relocates the player
            AudioSource.PlayClipAtPoint(ActivePointSound, transform.position);
            //Destroy(this.gameObject);
            
        }

    }
}
        