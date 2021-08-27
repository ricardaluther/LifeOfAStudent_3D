using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePoint : MonoBehaviour
{
    public CapsuleCollider col;
    public AudioClip ActivePointSound;
    
    [SerializeField] private GameObject _player;


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

        if (other.CompareTag("Character"))
        {
            Debug.LogWarning("Character activated ActivePoint:D...let the games begin");
            //Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(ActivePointSound, transform.position);
            _player.LocationChange(-2.0f, -8.0f, 0.0f);

        }



    }
}
        