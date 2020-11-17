using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macaron_Break : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Cookie")
        {
            Destroy(this.gameObject);
        }
    }
}
