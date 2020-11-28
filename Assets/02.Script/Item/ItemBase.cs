using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemStatus itemStatus;
    
    public LayerMask targetLayer;
    
    

    [Range(0, 10)]
    public float ItemRespawnTime;


  protected  WaitForSeconds RespawnTime;

  protected  MeshRenderer meshRenderer;

  protected  SphereCollider HitCollider;



    public virtual void Initialize(ItemStatus item)
    {
        itemStatus = item;

        RespawnTime = new WaitForSeconds(ItemRespawnTime);

        meshRenderer = GetComponent<MeshRenderer>();
        HitCollider = GetComponent<SphereCollider>();

    }

    public virtual IEnumerator ItemGet() { yield  break; }


}
