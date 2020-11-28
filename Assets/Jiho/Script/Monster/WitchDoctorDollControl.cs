using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollControl : MonsterBasic
{
    public GameObject bullet;
    public Transform launchPos;

    Vector3 Attackplace;

    float bulletSpeed = 10f;

    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(hpImage);
        go.transform.SetParent(hpCanvas.GetAnchorRect());
        go.transform.localScale = Vector3.one;
        uiHpBar = go.GetComponent<UIHPBar>();

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
       
        
        //uiHpBar.image.rectTransform.anchoredPosition = Camera.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        if (MonsterStatusValue.hp <= 0)
        {
            Dead();
        }
    }

    private void FixedUpdate()
    {
        InAttackRange(); // 공격범위 안에 드는 지 체크

        if(IsProgressAttack)
        {
            Launch();
        }
        
    }

    public void InAttackRange()
    {
        Collider[] InRangeTarget = Physics.OverlapSphere(transform.position, viewRadius, viewTargetMask);

        if(InRangeTarget.Length == 0)
        {
            IsInSight = false;
            return;
        }

        for(int i = 0; i < InRangeTarget.Length; i++)
        {
            Transform target = InRangeTarget[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, viewObstacleMask))
            {
                IsInSight = true;
                // 공격
                if (!IsProgressAttack)
                {
                    Attack();
                }
            }
        }

    }

    public override void Attack()
    {
        base.Attack();
        // 플레이어 포지션을 받아와서 그 위치로 공격 투사체 발사, 공격 중일 때 위치 받아오지않기
        
        if(!IsProgressAttack)
        {
            Attackplace = playerPos.position;
        }

        IsProgressAttack = true; // false 처리 해야함. LayerMask 활용

        go = Instantiate(bullet, launchPos);
        go.transform.position = launchPos.position;
        //Launch(Attackplace);

    }

    public void Launch() // 투사체 발사
    {
        Vector3 getDirection = (Attackplace - launchPos.position).normalized;
        go.transform.Translate(getDirection * Time.deltaTime * bulletSpeed);        
    }
}
