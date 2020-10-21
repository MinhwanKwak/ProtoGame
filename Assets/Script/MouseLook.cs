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
    

    public CinemachineCameraOffset cam;

    Vector3 Velocity;

    Vector3 CameraMousepos;
    

    public float CameraSmoothspeed;

    public float Cameraoffsetpos;
    // Update is called once per frame
    void Update()
    {
      
        
        CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(GroupPlane.Raycast(CameraRay, out rayLength))
        {
            pointTolook = CameraRay.GetPoint(rayLength);


            CameraMousepos = new Vector3(Mathf.Clamp(pointTolook.x, -Cameraoffsetpos, Cameraoffsetpos), Mathf.Clamp(pointTolook.z, -Cameraoffsetpos, Cameraoffsetpos), 0f);

            cam.m_Offset  = Vector3.SmoothDamp(cam.m_Offset, CameraMousepos, ref Velocity, CameraSmoothspeed);

            PlayerBody.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }
}
