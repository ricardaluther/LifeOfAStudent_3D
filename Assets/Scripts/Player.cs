using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField] 
    private float _jumpSpeed = 4f;
    [SerializeField]
    private float _gravity = 9.81f;
    private float _directionY;
    private bool _readyDoubleJump;
    private CharacterController _controller;
    public BoxCollider col;
   
    


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        col = GetComponent<BoxCollider>();
        transform.position = new Vector3(0f, 0f, 0f);
    }
   
    void Update()
    {
        PlayerMovement();
    }
    void PlayerMovement()
    {
        // pass basic movements to playertranslate
     float horizontalInput = Input.GetAxis("Horizontal");
     float verticalInput = Input.GetAxis("Vertical");
     Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime,
               0f,
               1f * verticalInput * _speed * Time.deltaTime);
     // is the player is grounded and we press space, allow the player to doublejump and apply _jumpspeed to the direction Y of the playertranslate
     if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
     {
         _readyDoubleJump = true;
         _directionY = _jumpSpeed;
     }
     // if the player is not grounded but doublejump is activated, enable another jump of have the speed
     else
     {
         if (Input.GetKeyDown(KeyCode.Space) && _readyDoubleJump)
         {
             _directionY = _jumpSpeed * 0.5f;
             _readyDoubleJump = false;
             
         }
     }
     // apply gravity to the y-direction, pass the y-direction to the playertranslate and pass the playertranslate to the controller
     _directionY -= _gravity * Time.deltaTime;
     playerTranslate.y = _directionY;
     _controller.Move(playerTranslate);
    }

    //gets called, when the player enters an ActivePoint
    public void LocationChange(float x, float y, float z)
    {
       // rb = GetComponent<Rigidbody>();
       // col = GetComponent<BoxCollider>();
        this.transform.position = new Vector3(x, y, z);
    }
}
