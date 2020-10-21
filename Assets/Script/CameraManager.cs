using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public GameObject target;

    public MouseLook playerlook;
    
    [Range(-100,100)]
    public float offsetX;

    [Range(-100, 100)]
    public float offsetY;

    [Range(-100, 100)]
    public float offsetZ;
    
    private Vector3 CameraVec;

    public MouseLook lookmouse;

    Vector3 velocity;
    Ray CameraRay;

    Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

    float rayLength;


    public Transform PlayerBody;

    private Vector3 pointTolook;



    Vector3 Velocity;

    public Vector3 CameraMousepos;


    public float CameraSmoothspeed;

    public float Cameraoffsetpos;

    private void Start()
    {
        CameraVec = new Vector3();

        



    }
    private void Update()
    {
        CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (GroupPlane.Raycast(CameraRay, out rayLength))
        {
            pointTolook = CameraRay.GetPoint(rayLength);


            CameraMousepos = new Vector3(Mathf.Clamp(pointTolook.x, -Cameraoffsetpos, Cameraoffsetpos), 0f, Mathf.Clamp(pointTolook.z, -Cameraoffsetpos, Cameraoffsetpos));

            transform.position = Vector3.SmoothDamp(transform.position, CameraMousepos, ref Velocity, CameraSmoothspeed);
            
            PlayerBody.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }

        //움직임이 있을때만으로 변경하는게 좋음 
        Vector3 te1 = CameraVec;
        
        
     
        
    }
}
