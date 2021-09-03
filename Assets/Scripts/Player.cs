using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")] [SerializeField]
    private int _lives = 3;

    public bool _drunk = false;
    private float _soberingTime = 4f;

    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpSpeed = 4f;
    [SerializeField] private float _gravity = 9.81f;
    private float _directionY;
    private bool _readyDoubleJump;
    private CharacterController _controller;
    private Animation anim;
    private BoxCollider col;



    void Start()
    {
        _drunk = false;
        _controller = GetComponent<CharacterController>();
        transform.position = new Vector3(0f, 0f, 0f);
        anim = gameObject.GetComponent<Animation>();
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
        Vector3 playerTranslate = transform.right * horizontalInput + transform.forward * verticalInput;
        //basic animations when moving
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.Play("Run");
        }
        else
        {
            anim.Play("Idle");
        }

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
        _controller.Move(playerTranslate * _speed * Time.deltaTime);
    }

    //gets called, when the player enters an ActivePoint
    public void LocationChange(float x, float y, float z)
    {
        Debug.LogWarning("LocationChange called");
        _controller.Move(new Vector3(x, y, z));
    }

    public void GetDrunk()
    {
        _drunk = true;
        //StartCoroutine(SoberUp());
    }

    //IEnumerator SoberUp()
    //{
     //   yield return WaitForSeconds(_soberingTime);
      //  _drunk = false;
    //}
    //getter for the lives variable
    public int GetLives()
    {
        return _lives;
    }
}

