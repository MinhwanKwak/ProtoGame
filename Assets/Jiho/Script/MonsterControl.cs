using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterStatus
{
    IDLE,
    RUN,
    ATTACK,
}

public class MonsterControl : MonoBehaviour
{
    public int HP;
    
    public Transform PlayerPos; // navigation으로 쫓아갈 플레이어 좌표



    public NavMeshAgent Nav;

    public Animator animator;

    public MonsterStatus MonsterStatus;

    public const string IDLE = "Idle";

    private void Awake()
    {
        HP = 1;
    }

    void Start()
    {
        PlayerPos.GetComponent<Transform>();
        this.MonsterStatus = MonsterStatus.IDLE;
        this.animator = animator.GetComponent<Animator>();
    }

    void Update()
    {
        //SetAnimation();
        //animator.Play(IDLE);
        //ApproachToPlayer();
        //Attack();
        StartCoroutine(DelayAttack());
    }

    public void SetAnimation(string _anim) // 임시 애니메이션 설정
    {
        if (MonsterStatus == MonsterStatus.IDLE)
            return;
        animator.Play(_anim);
       
    }

    public void ApproachToPlayer() // 플레이어를 쫓아감
    {
        Nav.SetDestination(PlayerPos.position);
    }

    public void Attack() // 공격
    {
        MonsterStatus = MonsterStatus.ATTACK;
        animator.SetBool("IsAttack", true); // 애니메이션 테스트
    }

    IEnumerator DelayAttack() // 테스트용
    {
        yield return new WaitForSeconds(3f);
        Attack();
    }
}
