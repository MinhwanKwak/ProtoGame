using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class MonsterBasic : MonoBehaviour
{
    public string thisname;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask viewTargetMask;
    public LayerMask viewObstacleMask;

    public LayerMask HitLayerMask;

    public LayerMask PatrolPointMask;

    public bool IsInSight; // 시야에 들어와있을 때
    public bool wasInSight; // 시야에 들어온 적이 있는지

    public MonsterStatusValue MonsterStatusValue;

    public GameObject hpImage;

    protected UIHPBar uiHpBar;
    public UIHPBar[] uiHpBarArray;
    
    public Transform HpTransform;
    public Transform[] HpTransformArray;

    protected HPCanvas hpCanvas;
    protected Vector2 hpUIInterval = new Vector2(40, 0);
    protected Vector2 hpUIVerticalInterval = new Vector2(0, -30);

    protected Transform tr;
    public NavMeshAgent Nav;

    public Animator animator;

    public MonsterStatus monsterStatus;

    public bool IsProgressAttack = false;
    public bool IsAttackOneTouch = false;

    public bool IsDead = false;

    public float MonsterAttackDelayTime = 0.0f;

    Vector3 OriginSpawnPoint;
    GameObject[] PatrolPoint = new GameObject[4];

    int PatrolNum = 0;

    protected bool IsPatrol = true;
    protected bool IsTrace = false;

    protected virtual void Awake()
    {
        IsInSight = false;
    }

    protected virtual void Start()
    {
        hpCanvas = FindObjectOfType<HPCanvas>();
        OriginSpawnPoint = transform.position;


        PatrolPoint[0] = ObjectPooler.Instance.SpawnFromPool("PatrolPoint", OriginSpawnPoint + new Vector3(10, 0, 5), Quaternion.identity);
        PatrolPoint[1] = ObjectPooler.Instance.SpawnFromPool("PatrolPoint", OriginSpawnPoint + new Vector3(5, 0, 10), Quaternion.identity);
        PatrolPoint[2] = ObjectPooler.Instance.SpawnFromPool("PatrolPoint", OriginSpawnPoint + new Vector3(-10, 0, 5), Quaternion.identity);
        PatrolPoint[3] = ObjectPooler.Instance.SpawnFromPool("PatrolPoint", OriginSpawnPoint + new Vector3(5, 0, -10), Quaternion.identity);
    }
    protected virtual void Update()
    {
        if (IsDead || monsterStatus == MonsterStatus.DEAD)
            return;

        MonsterAttackDelayTime += Time.deltaTime;

        //if(IsPatrol)
        //{
        //    Patrol();
        //}
        //else if(IsTrace)
        //{
        //    FindTarget();
        //}
        if(!IsInSight)
        {
            Patrol();
        }
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
        animator.StopPlayback();
        animator.SetBool("Dead", true);

        for (int i = 0; i < GameManager.Instance.maps.Length; ++i)
        {
            if (gameObject.tag == GameManager.Instance.maps[i].tag)
            {
                --GameManager.Instance.maps[i].MapMonsterCount;
                if (GameManager.Instance.maps[i].MapMonsterCount <= 0)
                {
                    GameManager.Instance.maps[i].DoorAnim[0].SetTrigger("DoorOpen");
                    return;
                }
            }
        }
    }

    public virtual void Dead() // 죽음
    {
        monsterStatus = MonsterStatus.DEAD;
        animator.SetBool("Dead", true);
    }
    public virtual void ApproachToPlayer() // 플레이어를 쫓아감
    {
        if (monsterStatus == MonsterStatus.ATTACK)
        {
            return;
        }
        else
            Nav.SetDestination(PlayerManager.Instance.playerControll.transform.position);
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
                // 이후 다시 순찰지역으로 돌아가야 한다
                DOTween.Kill(this.gameObject);
            }
        }
    }

    public virtual void Patrol()
    {
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, 30, PatrolPointMask);
        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            float dstToTarget = Vector3.Distance(transform.position, target.position);

            Debug.DrawRay(transform.position, dirToTarget,Color.blue, dstToTarget);
             if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, viewObstacleMask)) // 레이캐스트를 쏘았는데 obstacleMask가 아닐 때 참
             {
                int Randnumber = Random.Range(0, 4);
 
                while(PatrolNum == Randnumber)
                {
                    Randnumber = Random.Range(0, 4);
                }
                PatrolNum = Randnumber;

                if(IsDestination())
                {
                    StartCoroutine(PatrolDelay());
                    return;
                }
             }
            
              else if (wasInSight)
              {
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

    IEnumerator PatrolDelay()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Run");
        Nav.SetDestination(PatrolPoint[PatrolNum].transform.position);
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
