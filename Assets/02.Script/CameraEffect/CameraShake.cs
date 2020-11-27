using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originpos;
    
 public IEnumerator Shake(float duration,float magnitude)
    {
        originpos = transform.localPosition;

        float elased = 0.0f;

        while(elased <  duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originpos.z);


            elased += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originpos;
    }

}
