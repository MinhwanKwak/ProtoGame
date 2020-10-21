using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class MouseLook : MonoBehaviour
{
    Ray CameraRay;

    Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

    float rayLength;
    

    public Transform PlayerBody;

    private Vector3 pointTolook;
    
    public GameObject cam;

    Vector3 Velocity;
    
    public Vector3 CameraMousepos;

    public Vector3 temp;

    [Range(0, 100)]
    public float offsetX;

    [Range(0, 100)]
    public float offsetY;

    [Range(0, 100)]
    public float offsetZ;

    public float CameraSmoothspeed;

    public float Cameraoffsetpos;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(GroupPlane.Raycast(CameraRay, out rayLength))
        {
            pointTolook = CameraRay.GetPoint(rayLength);
            
            CameraMousepos = new Vector3(Mathf.Clamp(pointTolook.x, -Cameraoffsetpos, Cameraoffsetpos), 0f, Mathf.Clamp(pointTolook.z, -Cameraoffsetpos, Cameraoffsetpos));

            temp = Vector3.SmoothDamp(temp, CameraMousepos, ref Velocity, CameraSmoothspeed);

            
            cam.transform.position = temp + PlayerBody.transform.position;
            cam.transform.LookAt(PlayerBody.transform.position);
            cam.transform.eulerAngles = Vector3.zero;

            PlayerBody.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }
    }


}
