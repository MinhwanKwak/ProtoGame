using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//근거리 원거리

public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public int damage;
    public float rate;
    public BoxCollider MeleeArea;
    public TrailRenderer trailRenderer;

    public LayerMask targetMask;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetMask) != 0)
        {
            // GameObject newProjectile = GameObject.Instantiate(GameManager.Instance.playercontroller.Effects[3]) as GameObject;
            //  StartCoroutine(GameManager.Instance.cameraManager.camerashake.ShakeCamera());
        }
    }
    
    
    



}
