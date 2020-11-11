using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//근거리 원거리
public enum Type {Almost, Distance};

public class Weapon : MonoBehaviour
{
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider MeleeArea;
    public TrailRenderer trailRenderer;

    public LayerMask targetMask;
    
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Damage:" + damage);
        }
    }


}
