using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollControl : MonsterBasic
{
    public GameObject bullet;
    public WitchDoctorDollWeapon Weapon;
    public Transform launchPos;

    public Vector3 Attackplace;

    float bulletSpeed = 10f;

    public GameObject go;

    float getTime = 0.0f;

    protected override void Awake()
    {
        base.Awake();

        Weapon = bullet.GetComponent<WitchDoctorDollWeapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
        go.transform.SetParent(hpCanvas.GetAnchorRect());
        uiHpBar = go.GetComponent<UIHPBar>();
        uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
        uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

    }

    private void FixedUpdate()
    {
        InAttackRange(); // 공격범위 안에 드는 지 체크

        getTime += Time.deltaTime;

        if (IsProgressAttack && getTime >= 5f)
        {
            IsProgressAttack = false;
            getTime = 0.0f;
        }

    }

 

    public void InAttackRange()
    {
        Collider[] InRangeTarget = Physics.OverlapSphere(transform.position, viewRadius, viewTargetMask);

        if(InRangeTarget.Length == 0)
        {
            IsInSight = false;
            //IsProgressAttack = false;
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
            Attackplace = PlayerManager.Instance.playerControll.transform.position;
        }

        IsProgressAttack = true; // false 처리 해야함. LayerMask 활용
        IsAttackOneTouch = false;
        //go = Instantiate(bullet, launchPos);
        go = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollBullet", launchPos.position, Quaternion.identity);
        go.transform.SetParent(this.transform);
        //Launch(Attackplace);
        

    }

    public void Launch() // 투사체 발사
    {
        Vector3 getDirection = (Attackplace - launchPos.position).normalized;
        //go.transform.Translate(getDirection * Time.deltaTime * bulletSpeed);
        go.transform.position += (getDirection * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & HitLayerMask) != 0 && PlayerManager.Instance.playerControll.playerAnimationEvent.GetDamageCheck())
        {
            //PlayerManager.Instance.playerControll.playerAnimationEvent.SetDamageCheck(false);
            //StartCoroutine(DamageTime());
            //StartCoroutine(GameManager.Instance.cameraManager.camerashake.Shake(0.25f, 0.25f));
            //GameObject Effect = ObjectPooler.Instance.SpawnFromPool("HitEffect", hittarget.transform.position, hittarget.transform.rotation);
            //Effect.transform.parent = hittarget.transform;
            //StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));

            // 피격
            MonsterStatusValue.hp -= 1;

            if (MonsterStatusValue.hp <= 0) // 사망
            {
                ProcessDead();
            }
            //else
            //{
            //    animator.SetTrigger("BeAttacked");
            //}
        }
    }
}
