﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class MonsterBasic : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask viewTargetMask;
    public LayerMask viewObstacleMask;

    public bool IsInSight; // 시야에 들어와있을 때
    public bool wasInSight; // 시야에 들어온 적이 있는지

    public MonsterStatusValue MonsterStatusValue;

    public GameObject hpImage;

    protected UIHPBar uiHpBar;

    //public DummyCamera Camera;
    public CameraManager Camera;


    public Transform HpTransform;
    public HPCanvas hpCanvas;

    public Transform playerPos; // navigation으로 쫓아갈 플레이어 좌표
    protected Transform tr;
    public NavMeshAgent Nav;

    public Animator animator;

    public MonsterStatus monsterStatus;

    public bool IsProgressAttack = false;

    private void Awake()
    {
        IsInSight = false;
    }
    protected virtual void Update()
    {
        FindTarget();
        if (IsDestination() && !IsInSight)
        {
            monsterStatus = MonsterStatus.IDLE;
            animator.SetTrigger("Idle");
        }
        if (monsterStatus == MonsterStatus.RECEIVEDATTACK)
        {

        }
    }

    public virtual void Idle() // 평소상태
    {

    }

    public virtual void Attack() // 공격
    {

    }

    public virtual void ReceivedAttack() // 피격
    {
      
    }

    public virtual void ProcessDead()
    {

    }

    public virtual void Dead() // 죽음
    {
    
    }
    public virtual void ApproachToPlayer() // 플레이어를 쫓아감
    {
        if (monsterStatus == MonsterStatus.ATTACK)
        {
            return;
        }
        else
            Nav.SetDestination(playerPos.position);
    }

    public virtual void FindTarget()
    {
        Quaternion RotateviewAngle = Quaternion.Euler(0, viewAngle / 2, 0);
        Vector3 forwardVector = transform.forward;
        Vector3 viewAngleVector = RotateviewAngle * forwardVector;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, viewTargetMask);
        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) > Vector3.Dot(transform.forward, viewAngleVector)) // 타겟벡터와의 내적값이 시야벡터와의 내적값보다 크면 시야 안에 들어옴
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //Debug.DrawRay(transform.position, dirToTarget,Color.blue);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, viewObstacleMask)) // 레이캐스트를 쏘았는데 obstacleMask가 아닐 때 참
                {                    
                    IsInSight = true;
                    wasInSight = true;
                    ApproachToPlayer();
                    return;
                }
            }
            else if(wasInSight)
            {
                //IsInSight = false;

                StartCoroutine(OutSight());
                DOTween.Kill(this.gameObject);
            }
        }
    }

    IEnumerator OutSight()
    {
        //wasInSight = true;
        ApproachToPlayer();
        yield return new WaitForSeconds(5f);
        wasInSight = false;
        IsInSight = false;
    }

    public bool IsDestination() // 네비게이션 도착했는지 안했는지
    {
        if (!Nav.pathPending)
        {
            if (Nav.remainingDistance <= Nav.stoppingDistance)
            {
                if (!Nav.hasPath || Nav.velocity.sqrMagnitude == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
