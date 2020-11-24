using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemStatus itemStatus;
    
    public LayerMask targetLayer;

    public PlayerControll player;

    [Range(0, 10)]
    public float BuffTime;
    

    public virtual void Initialize(ItemStatus item)
    {
        itemStatus = item;
    }

    
    



}
