using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollWeapon : MonoBehaviour
{
    WitchDoctorDollControl owner;

    float bulletSpeed = 10f;
    float getTime = 0.0f;

    GameObject TrailEffect;

    private void Start()
    {
        owner = GetComponentInParent<WitchDoctorDollControl>();
        TrailEffect = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollTrailEffect", transform.position, Quaternion.identity);
        TrailEffect.transform.SetParent(this.transform);
        TrailEffect.transform.localPosition = Vector3.one;
    }

    private void Update()
    {
        TrailEffect.transform.position = transform.position;

        getTime += Time.deltaTime;
        if (getTime >= 2f)
        {
            
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Launch();
    }

    public void Launch()
    {
        Vector3 getDirection = (owner.Attackplace - owner.launchPos.position).normalized;
        transform.position += (getDirection * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (((1 << other.gameObject.layer) & hitLayer) != 0)
        //{

        //}
        owner.IsAttackOneTouch = false;

        StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollTrailEffect", TrailEffect, 0f));

        GameObject hitEffectGo =  ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollHitEffect", transform.position, Quaternion.identity);
        hitEffectGo.transform.SetParent(this.transform);
        StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollHitEffect", hitEffectGo, 0.5f));

        //Destroy(this.gameObject);
    }

}
