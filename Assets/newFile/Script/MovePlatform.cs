using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    float turn = 1;
    private void Update()
    {
        transform.Translate((Vector3.right*turn)*Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            turn *= -1;
        }
    }
}
