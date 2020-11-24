using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ItemBase
{
    [Range(0,20)]
    public float SpeedIncrease = 0f;

    WaitForSeconds RespawnTime;
    
    [Range(0, 10)]
    public float ItemRespawnTime;

    MeshRenderer meshRenderer;
    SphereCollider HitCollider;
    private void Start()
    {
        Initialize(ItemStatus.SPEEDITEM);

        RespawnTime = new WaitForSeconds(ItemRespawnTime);

        meshRenderer =  GetComponent<MeshRenderer>();
        HitCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << targetLayer)  & other.gameObject.layer) != 0)
        {
            StartCoroutine(ItemGet());
        }
    }

    IEnumerator ItemGet()
    {
        player.speed += SpeedIncrease;
        meshRenderer.enabled = false;
        HitCollider.enabled = false;
        Invoke("BuffpersistTime", BuffTime);
        yield return RespawnTime;
        
        meshRenderer.enabled = true;
        HitCollider.enabled = true;
    }

    
   private void BuffpersistTime()
    {
        player.speed = player.CurrentSpeed;
    }

}
