using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Vector3 desiredPos;
    
    private Vector3 playerPos;
    
    public AudioClip failure;

    [Header("Timers")]
    [SerializeField] public float timer1 = 0.0f; //this timer decides the time of the general random movement

    [SerializeField] public float timer2 = 0.0f; // decides when it is time to seek the closeness of the player
    [SerializeField] public float timer3 = 0.0f; // decides when time to attack

    [SerializeField] private float timer4 = 0.0f; // decides when to update the current position(Vector3) of the player
    [SerializeField] private float TimeToSeek = 10f;

    [SerializeField] private float TimeToAttack = 30f;

    private float TimeToLocatePlayer = 2.0f;
    
    public float timerSpeed = 1f;
    public float timeToMove = 2f;

    [SerializeField] private float speed = 2f;

    [Header("Koordinaten")]
    [SerializeField] private float xPos;
    [SerializeField] private float yPos = 0.88f;
    [SerializeField] private float zPos;

    [SerializeField] private Vector3 lastPlayerPos;
    [SerializeField] private Vector3 closetoPlayer;
    
// TODO: spawn more books, destroy book if it hits player + sound, check the wierd movement in seek(vll weil man sich ständig bewegt und so die Position die nahe des Players kalkuliert wurde nie erreicht wird...)/ attack, 

    void Start()
    {
        transform.position = new Vector3(Random.Range(-50f, 50f), 0.88f, Random.Range(-50f, 50f));

        rewriteDesPos();
    }

    void Update()
    {
        MovePlayer();
       // SeekPlayer();
        // AttackPlayer();
        LocatePlayerCloseArea();
    }

    private void LocatePlayerCloseArea()
    {
        timer4 += Time.deltaTime * timerSpeed;
        if (timer4 >= TimeToLocatePlayer)
        {
            lastPlayerPos = GameObject.FindGameObjectWithTag("Character").transform.position;
            timer4 = 0.0f;
            closetoPlayer = new Vector3(lastPlayerPos.x + Random.Range(-10.0f, 10.0f), yPos, lastPlayerPos.z + Random.Range(-10.0f, 10.0f));
        }
    }

    private void MovePlayer()
    {
        timer1 += Time.deltaTime * timerSpeed;
        timer2 += Time.deltaTime * timerSpeed;
        timer3 += Time.deltaTime * timerSpeed;
        
        //book will follow player until it hit the player
        if (timer3 >= TimeToAttack)
        {
            playerPos = GameObject.FindGameObjectWithTag("Character").transform.position;
            transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, playerPos) <= 0.1f)
            {
                rewriteDesPos();
                timer3 = 0.0f;
                timer2 = 0.0f; //if it attacked the player it doesn't need to seek right away after.... 
                timer1 = 0.0f;
            }
        }
        //book will go close to the player
        else if (timer2 >= TimeToSeek)
        {
            timer1 = 0.0f; // if it seeks it doesn't have to move normally at the same time...
            // is being done in LocatePlayerCloseArea()...
            //playerPos = GameObject.FindGameObjectWithTag("Character").transform.position;
            //closetoPlayer = new Vector3(playerPos.x + Random.Range(-15.0f, 15.0f), yPos, playerPos.z + Random.Range(-10.0f, 10.0f));
            transform.position = Vector3.Lerp(transform.position, closetoPlayer, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, closetoPlayer) <= 2.0f)
            {
                rewriteDesPos();
                timer2 = 0.0f;
            }
        } 
        else if (timer1 >= timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                rewriteDesPos();
                timer1 = 0.0f;
            }
        }
    }

    private void rewriteDesPos()
    {
        //check for boundaries and assign a new x and z coordinate

        //new x
        xPos = transform.position.x + Random.Range(-20f, 20f);
        if (xPos < -50f)
        {
            xPos = -50f;
        }
        else if (xPos > 50f)
        {
            xPos = 50f;
        }
        
        //new z
        zPos = transform.position.z + Random.Range(-10f, 10f);
        if (zPos < -50f)
        {
            zPos = -50f;
        }
        else if (zPos > 50f)
        {
            zPos = 50f;
        }
    
        //new Position
        desiredPos = new Vector3(xPos, yPos, zPos);
    }

    private void SeekPlayer()
    {
        timer2 += Time.deltaTime * timerSpeed;
        if (timer2 >= TimeToSeek)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Character").transform.position /* + range */, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                rewriteDesPos();
                timer2 = 0.0f;
            }
        }
    }
    
    private void AttackPlayer()
    {
        timer3 += Time.deltaTime * timerSpeed;
        if (timer3 >= TimeToAttack)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Character").transform.position, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                rewriteDesPos();
                timer3 = 0.0f;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("Character"))
        {
            Debug.LogWarning("Book hit Player:O");
            //Destroy(this.gameObject); // maybe implement this later when we actually spawn more of them...
            //reduce the points or something of player
            AudioSource.PlayClipAtPoint(failure, transform.position);


        }
    }
}
