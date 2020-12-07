using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WitchDoctorDollWeapon : MonoBehaviour
{
    public WitchDoctorDollControl owner;

    PlayerControll player;
    

    float bulletSpeed = 10f;
    float getTime = 0.0f;

    GameObject TrailEffect;

    public LayerMask PlayerLayer;

    bool isHit = false;

    bool isPool = false;

    private void Start()
    {
        owner = GetComponentInParent<WitchDoctorDollControl>();
        TrailEffect = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollTrailEffect", transform.position, Quaternion.identity);
        TrailEffect.transform.SetParent(this.transform);
        TrailEffect.transform.localPosition = Vector3.one;

        player = FindObjectOfType<PlayerControll>();
    }

    private void Update()
    {
        //if (isPool)
        //{
        //    rePooling();
        //}

        TrailEffect.transform.position = transform.position;

        getTime += Time.deltaTime;
        if (getTime >= 2f)
        {

            //Destroy(this.gameObject);
            getTime = 0f;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollBullet", owner.go, 0f));
            //isHit = false;
        }
    }

    private void FixedUpdate()
    {
        if(!isHit)
        {
            Launch();
        }
        
    }

    public void rePooling()
    {
        isHit = false;
        TrailEffect = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollTrailEffect", transform.position, Quaternion.identity);
    }

    public void Launch()
    {
        Vector3 getDirection = (owner.Attackplace - owner.launchPos.position).normalized;
        transform.position += (getDirection * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & PlayerLayer) != 0 && !owner.IsAttackOneTouch)
        {
            //owner.IsAttackOneTouch = true;
            isHit = true;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollTrailEffect", TrailEffect, 0f));

            GameObject hitEffectGo = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollHitEffect", player.Hittransform.position, Quaternion.identity);
            hitEffectGo.transform.SetParent(owner.transform);
            hitEffectGo.transform.localScale = Vector3.one;
            //hitEffectGo.transform.position = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(player.Hittransform.position);
            //hitEffectGo.GetComponent<Image>().rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(player.Hittransform.position);



            StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollHitEffect", hitEffectGo, 0.5f));
//            isPool = true;
            //StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollBullet", owner.go, 0f));

        }

        
    }

}
