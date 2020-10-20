using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public GameObject target;

    public MouseLook playerlook;
    
    [Range(0,100)]
    public float offsetX;

    [Range(0, 100)]
    public float offsetY;

    [Range(0, 100)]
    public float offsetZ;

    public CinemachineCameraOffset cam;

    private Vector3 CameraVec;
    
    private void Start()
    {
        CameraVec = new Vector3();

        float a = cam.m_Offset.x;

        int b = 4;

    }
    private void Update()
    {
    //    //움직임이 있을때만으로 변경하는게 좋음 
    //    CameraVec.x = target.transform.position.x + offsetX;
    //    CameraVec.y = target.transform.position.y + offsetY;
    //    CameraVec.z = target.transform.position.z + offsetZ;
        

        transform.position = CameraVec;
        
    }
}
