using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;
    
    [Header("Player Settings")]
    [SerializeField]
 
    private int _lives = 3;
    public static Vector3 playerPos;
    public Rigidbody rb;
    public LayerMask groundLayers;
    public float jumpForce = 7;
    public BoxCollider col;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        transform.position = new Vector3(0f, 0f, 0f);
    }
   
    void Update()
    {
        PlayerMovement();
        Jump();
    }
    void PlayerMovement()
    {
     float horizontalInput = Input.GetAxis("Horizontal");
     float verticalInput = Input.GetAxis("Vertical");
     Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime,
               0f,
               1f * verticalInput * _speed * Time.deltaTime);
               transform.Translate(playerTranslate);
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckBox(col.bounds.center,
            new Vector3(col.size.x, col.size.y, col.size.z),Quaternion.identity,groundLayers);
     

    }
}
