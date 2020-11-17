using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterArea : MonoBehaviour
{
    bool player;
    private void Start()
    {
        player = false;
    }
    public bool playerCatch()
    {
        return player;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = true;
        }
    }
}
