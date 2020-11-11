using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class MonsterControl : MonsterBasic
{
    private void Awake()
    {
        MonsterStatusValue.Initialize();
        tr = GetComponent<Transform>();
    }

    void Start()
    {
        GameObject go = Instantiate(hpImage);
        go.transform.SetParent(hpCanvas.GetAnchorRect());
        go.transform.localScale = Vector3.one;
        uiHpBar = go.GetComponent<UIHPBar>();
        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
        //uiHpBar.image.rectTransform.anchoredPosition = CameraManager.MainCamera.WorldToScreenPoint(HpTransform.position);
        //uiHpBar.UpdatePositionFromWorldPosition(HpTransform.position);

        this.MonsterStatus = Status.IDLE;
        this.animator = animator.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);

        if (MonsterStatusValue.hp <= 0)
        {
            Dead();
        }
    }

   

    public override void Idle()
    {
        base.Idle();
        MonsterStatus = Status.IDLE;
        Nav.isStopped = false;
        animator.SetTrigger("Idle");

    }

    public override void Attack()
    {
        base.Attack();
        MonsterStatus = Status.ATTACK;
        Nav.isStopped = true;
        animator.SetTrigger("Attack");
        Vector3 transform = new Vector3(playerPos.position.x, 0, playerPos.position.z);
        tr.DOLookAt(transform, 0.2f);
    }

    public override void ReceivedAttack()
    {
        base.ReceivedAttack();
        MonsterStatus = Status.RECEIVEDATTACK;
        Nav.isStopped = true;
        animator.SetTrigger("ReceivedAttack");
        MonsterStatusValue.hp -= 5;

        uiHpBar.SetHPUIFill();

    }

    public override void Dead()
    {
        base.Dead();
        MonsterStatus = Status.DEAD;
        Nav.isStopped = true;
        animator.SetTrigger("Dead");
    }

    public override void ApproachToPlayer()
    {
        base.ApproachToPlayer();
        if (Vector3.Distance(tr.position, playerPos.position) < MonsterStatusValue.range && !IsInSight)
        {
            animator.SetTrigger("Idle");
            Nav.isStopped = false;
            MonsterStatus = Status.IDLE;
        }
        else if (Vector3.Distance(tr.position, playerPos.position) > MonsterStatusValue.range && IsInSight)
        {
            animator.SetTrigger("Run");
            MonsterStatus = Status.RUN;
            Nav.isStopped = false;
            Nav.SetDestination(playerPos.position);
        }
        else
        {
            //animator.SetTrigger("Attack");
            //MonsterStatus = Status.ATTACK;
        }
            
    }

    //IEnumerator AI()
    //{
    //    while(true)
    //    {
    //        if (Vector3.Distance(tr.position, playerPos.position) < MonsterStatusValue.range && IsInSight)
    //        {
    //            Attack();
                
    //        }
    //        yield return new WaitForSeconds(MonsterStatusValue.tickRate);
    //        //Nav.isStopped = false;
    //    }
    //}
}
