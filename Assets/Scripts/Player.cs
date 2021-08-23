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



    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
   
    void Update()
    {
        PlayerMovement();
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
}
