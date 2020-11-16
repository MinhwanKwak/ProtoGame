using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaek : MonoBehaviour
{
    public Transform ShakeCameraA;
    public bool shakeRotate = false;

    private Vector3 originpos;
    private Quaternion originRot;

    private void Start()
    {
        originpos = ShakeCameraA.localPosition;
        originRot = ShakeCameraA.localRotation;
    }

    private void Update()
    {
        
    }

    public IEnumerator ShakeCamera(float duration = 0.2f ,float manitudePos = 0.6f, float magnitudeRot = 0.3f)
    {
        float passTime =  0.0f;

        while(passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            ShakeCameraA.localPosition = shakePos * manitudePos;

            if(shakeRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));

                ShakeCameraA.localRotation = Quaternion.Euler(shakeRot);


            }

            passTime += Time.deltaTime;

            yield return null;

        }

        ShakeCameraA.localPosition = originpos;
        ShakeCameraA.localRotation = originRot;
    }
    
}
