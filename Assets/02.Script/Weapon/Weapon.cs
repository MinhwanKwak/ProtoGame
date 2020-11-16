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

    WaitForSecondsRealtime timestop;
    public float TimeStop = 0f;
    
    
    
    private void Start()
    {
        timestop = new WaitForSecondsRealtime(TimeStop);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetMask) != 0)
        {
            StartCoroutine(DamageTime());
            GameObject Effect =   ObjectPooler.Instance.SpawnFromPool("HitEffect", other.gameObject.transform.position, Quaternion.identity);
            // GameObject newProjectile = GameObject.Instantiate(GameManager.Instance.playercontroller.Effects[3]) as GameObject;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));


            //  StartCoroutine(GameManager.Instance.cameraManager.camerashake.ShakeCamera());

        }
    }
    
    
    
    IEnumerator DamageTime()
    {
        Time.timeScale = 0f;

        yield return timestop;

        Time.timeScale = 1f;

    }



}
