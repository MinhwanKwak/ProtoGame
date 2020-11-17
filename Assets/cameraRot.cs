using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRot : MonoBehaviour
{
    float xRot;
    float yRot;
    Vector3 dir;
    float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 250.0f;
    }

    // Update is called once per frame
    void Update()
    {
        xRot += Input.GetAxis("Mouse Y");// * rotSpeed * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X"); //* rotSpeed * Time.deltaTime;

        dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
        transform.position +=dir;
    }
}
