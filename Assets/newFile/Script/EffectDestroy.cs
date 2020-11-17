using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2.0f)
            Destroy(this.gameObject);
    }
}
