using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollWeapon : MonoBehaviour
{
    WitchDoctorDollControl owner;

    float bulletSpeed = 10f;
    float getTime = 0.0f;

    private void Start()
    {
        owner = GetComponentInParent<WitchDoctorDollControl>();
    }

    private void Update()
    {
       
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
        Destroy(this.gameObject);
    }

}
