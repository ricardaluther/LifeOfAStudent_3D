using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Vector3 desiredPos;

    private Vector3 closetoPlayer;

    private Vector3 playerPos;
    
    [Header("Timers")]
    [SerializeField] public float timer1 = 1f;

    [SerializeField] public float timer2 = 1f;
    [SerializeField] public float timer3 = 1f;
    [SerializeField] private float TimeToSeek = 10f;

    [SerializeField] private float TimeToAttack = 30f; 
    
    public float timerSpeed = 1f;
    public float timeToMove = 2f;

    [SerializeField] private float speed = 2f;

    [Header("Koordinaten")]
    [SerializeField] private float xPos;
    [SerializeField] private float yPos = 0.88f;
    [SerializeField] private float zPos;
    
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
            }
        }
        //book will go close to the player
        else if (timer2 >= TimeToSeek)
        {
            playerPos = GameObject.FindGameObjectWithTag("Character").transform.position;
            closetoPlayer = new Vector3(playerPos.x + Random.Range(-5f, 5f), yPos, playerPos.z + Random.Range(-5f, 5f));
            transform.position = Vector3.Lerp(transform.position, closetoPlayer, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, closetoPlayer) <= 0.1f)
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
        xPos = transform.position.x + Random.Range(-10f, 10f);
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


    /*
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    //returns a random Vector3
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.88f, UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    */
}
