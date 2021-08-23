using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private int _speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0.5f, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * _speed,
                                       0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _speed);
        }

        else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * _speed,
                                       0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _speed);
        }
    }
}
