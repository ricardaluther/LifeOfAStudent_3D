using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 300f;
    [SerializeField]
    private Transform _player;

    private float _xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -60f, 60f);
        _player.Rotate(Vector3.up * mouseX);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);


    }
}