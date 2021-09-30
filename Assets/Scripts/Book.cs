using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Vector3 desiredPos;
    
    private Vector3 playerPos;
    
    public AudioClip failure;
	public AudioClip good;
	public AudioClip blätter;
	public AudioClip danger;
	
	[Header("Book will attack:")]
	[SerializeField] private bool isAttacker = false;

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
	
	//variables for which floor it should be spawned on
	[SerializeField] private float floor = 7f;
	[SerializeField] private float randFloor; 
    


    void Start()
    {
		randFloor = Random.Range(0f, 3f);
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

        transform.position = new Vector3(Random.Range(-27f, 25f), floor, Random.Range(-21f, 110f));

        rewriteDesPos();
		
		//decide whether the book will have the attack function
		if (Random.Range(0.0f, 10.0f) > 7.0f)
		{
			isAttacker = true;
		}
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
            lastPlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            timer4 = 0.0f;
            closetoPlayer = new Vector3(lastPlayerPos.x + Random.Range(-10.0f, 10.0f), lastPlayerPos.y, lastPlayerPos.z + Random.Range(-10.0f, 10.0f));
        }
    }

    private void MovePlayer()
    {
        timer1 += Time.deltaTime * timerSpeed;
        timer2 += Time.deltaTime * timerSpeed;
        timer3 += Time.deltaTime * timerSpeed;
        
        //book will follow player until it hits the player
        if ((timer3 >= TimeToAttack) && isAttacker)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

			
            transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);
			if(Vector3.Distance(transform.position, playerPos) > 4.0f && Vector3.Distance(transform.position, playerPos) < 4.4f)
			{
				AudioSource.PlayClipAtPoint(danger, transform.position);
			}
            if (Vector3.Distance(transform.position, playerPos) <= 0.1f)
            {
                
                timer3 = 0.0f;
                timer2 = 0.0f; //if it attacked the player it doesn't need to seek right away after.... 
                timer1 = 0.0f;
				rewriteDesPos();
				this.gameObject.SetActive(false);
				
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

  /*  private void SeekPlayer()
    {
        timer2 += Time.deltaTime * timerSpeed;
        if (timer2 >= TimeToSeek)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Character").transform.position /* + range , Time.deltaTime * speed);
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
*/


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        //Add Stress:
        if (other.CompareTag("Player"))
        {
            if (Player.GetMoney() == 0)
            {
                Player.AddStress(10);
            }
            else
            {
                Player.AddStress(2);
            }
            AudioSource.PlayClipAtPoint(failure, transform.position);
            //Debug.LogWarning("Book hit Player:O");
             // maybe implement this later when we actually spawn more of them...
            
        }
        else if (other.CompareTag("Projectile"))
        {
            Player.AddStress(-1);
            Player.AddEcts(5);
			AudioSource.PlayClipAtPoint(good, transform.position);
 			AudioSource.PlayClipAtPoint(blätter, transform.position);
            this.gameObject.SetActive(false);
        }
    }
}
