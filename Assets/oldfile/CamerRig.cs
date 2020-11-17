using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRig : MonoBehaviour {
    public Transform target;
    public float targetY;

    public float xRotMax;
    public float rotSpeed;
    public float scrollSpeed;

    public float distance;
    public float minDistance;
    public float maxDistance;

    private float xRot;
    private float yRot;
    private Vector3 targetPos;
    private Vector3 dir;


    public float shakes = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    bool CameraShaking;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        originalPos = transform.position;
        CameraShaking = true;
    }

    private void Update()
    {
        xRot += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, -xRotMax, xRotMax);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        targetPos = target.position + Vector3.up * targetY;

        dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
        if (!CameraShaking)
            transform.position = targetPos + dir * -distance;

        //플레이어가 바라보는 방향
       Vector3 moveDir = transform.localRotation * Vector3.forward;
        target.localRotation = transform.localRotation;
        target.localRotation = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
       
    }

    private void LateUpdate()
    {
        transform.LookAt(targetPos);
    }
   
    void FixedUpdate()
    {
        if (CameraShaking)
        {
            if (shakes > 0)
            {
                Debug.Log(Random.insideUnitSphere);
               // Vector3 _shake = new Vector3(0,0, Random.insideUnitSphere.z);
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakes -= Time.deltaTime * decreaseFactor;
            }

            else
            {
                shakes = 0f;
                gameObject.transform.localPosition = originalPos;
                CameraShaking = false;
            }
        }
    }

    public void ShakeCamera(float shaking)
    {
        shakes = shaking;
        originalPos = transform.position;
        CameraShaking = true;
    }

}
