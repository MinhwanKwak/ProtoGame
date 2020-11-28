using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollControl : MonsterBasic
{
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
        InAttackRange();

        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        if (MonsterStatusValue.hp <= 0)
        {
            Dead();
        }
    }

    public void InAttackRange()
    {
        Collider[] InRangeTarget = Physics.OverlapSphere(transform.position, viewRadius);

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
                
            }
        }

    }
}
