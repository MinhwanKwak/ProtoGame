using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ItemBase
{
    [Range(0,20)]
    public float SpeedIncrease = 0f;

    [Range(0, 10)]
    public float BuffTime;


    private void Start()
    {
        Initialize(ItemStatus.SPEEDITEM);

      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << targetLayer)  & other.gameObject.layer) != 0)
        {
            StartCoroutine(ItemGet());
        }
    }

  

    
   private void BuffpersistTime()
    {
        PlayerManager.Instance.playerControll.speed = PlayerManager.Instance.playerControll.CurrentSpeed;
    }


    public override IEnumerator ItemGet()
    {
        PlayerManager.Instance.playerControll.speed += SpeedIncrease;
        meshRenderer.enabled = false;
        HitCollider.enabled = false;
        Invoke("BuffpersistTime", BuffTime);
        yield return RespawnTime;

        meshRenderer.enabled = true;
        HitCollider.enabled = true;

    }
}
