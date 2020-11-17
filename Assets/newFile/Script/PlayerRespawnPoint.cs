using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnPoint : MonoBehaviour
{
    bool RespawnSave;
    public Transform resPawn;
    Transform RespawnPoint = null;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&& !RespawnSave)
        {
            RespawnSave = true;
            RespawnPoint = resPawn;
        }
    }
    public Transform ResPawunGet()
    {
        return RespawnPoint;
    }
}
