using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Status
{
    IDLE,
    RUN,
    ATTACK,
}

public class MonsterBasic : MonoBehaviour
{
    public MonsterStatusValue MonsterStatusValue;

    public GameObject hpImage;

    protected UIHPBar uiHpBar;

    public TemporaryCam Camera;


    public Transform HpTransform;
    public HPCanvas hpCanvas;

    public Transform PlayerPos; // navigation으로 쫓아갈 플레이어 좌표
    public NavMeshAgent Nav;

    public Animator animator;

    public Status MonsterStatus;

    void Start()
    {
        
    }
    protected virtual void Update()
    {
        
    }
    public virtual void SetAnimation(string _anim) // 임시 애니메이션 설정
    {
        if (MonsterStatus == Status.IDLE)
            return;
        animator.Play(_anim);
    }

    public virtual void ApproachToPlayer() // 플레이어를 쫓아감
    {
        Nav.SetDestination(PlayerPos.position);
    }

    public virtual void Attack() // 공격
    {
        MonsterStatus = Status.ATTACK;
        animator.SetBool("IsAttack", true); // 애니메이션 테스트
    }
}
