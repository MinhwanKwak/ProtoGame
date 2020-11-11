using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public Transform PlayerBodyTransform;
    public Transform PlayerTransform;

    public Vector3 pointTolook;
    public Vector3 Playerlook;

    public GameObject playercam;

    public Camera MainCamera;
    

    public float CameraSmoothspeed;

    public float Cameraoffsetpos;


    public Vector3 CameraMousepos;
 
    public Vector3 CameraFinal;
   
    public Vector3 offsetpos;
    
    private Vector3 Velocity;
    
    private Ray CameraRay;

    private Ray PlayerportRay;

    private Plane GroupPlane;

    private float rayLength;
    private float playerlength;



    private void Start()
    {
         GroupPlane = new Plane(Vector3.up, Vector3.zero);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (GroupPlane.Raycast(CameraRay, out rayLength))
        {
            pointTolook = CameraRay.GetPoint(rayLength);

            Playerlook = pointTolook - PlayerTransform.position;

            CameraMousepos = new Vector3(Mathf.Clamp(Playerlook.x, -Cameraoffsetpos, Cameraoffsetpos), 0f, Mathf.Clamp(Playerlook.z, -Cameraoffsetpos, Cameraoffsetpos));

            CameraFinal = Vector3.SmoothDamp(CameraFinal, CameraMousepos, ref Velocity, CameraSmoothspeed);
            
            transform.position = CameraFinal + PlayerBodyTransform.transform.position;

            transform.eulerAngles = Vector3.zero;
        
            GameManager.Instance.playercontroller.SetMousePointLook(Playerlook);

            PlayerBodyTransform.LookAt(new Vector3(pointTolook.x, PlayerBodyTransform.position.y, pointTolook.z));


        }
    }
    
}
