using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Vector3 desiredPos;
    [SerializeField]
    public float timer = 1f;
    
    public float timerSpeed = 1f;
    public float timeToMove = 2f;

    private float speed = 2f;

    [Header("Koordinaten")]
    [SerializeField] private float xPos;
    [SerializeField] private float yPos = 0.88f;
    [SerializeField] private float zPos;
    


    void Start()
    {
        transform.position = new Vector3(Random.Range(-50f, 50f), 0.88f, Random.Range(-50f, 50f));

        rewriteDesPos();
    }

    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        if (timer >= timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                rewriteDesPos();
                timer = 0.0f;
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
