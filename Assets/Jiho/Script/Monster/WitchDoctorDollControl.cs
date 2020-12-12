using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctorDollControl : MonsterBasic
{
    public GameObject bullet;
    public Transform launchPos;

    public Vector3 Attackplace;

    public GameObject go;

    float getTime = 0.0f;

    public GameObject uiHpBargo;
    public GameObject[] uiHpBargoArray;

    public GameObject hittarget;

    //public GameObject AttackRangeImage;

    public float TimeStop = 0f;

    WaitForSecondsRealtime timestop;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //GameObject go = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
        //go.transform.SetParent(hpCanvas.GetAnchorRect());
        //uiHpBar = go.GetComponent<UIHPBar>();
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        base.Start();
        timestop = new WaitForSecondsRealtime(TimeStop);

        uiHpBargoArray = new GameObject[(int)MonsterStatusValue.maxHp];
        uiHpBarArray = new UIHPBar[(int)MonsterStatusValue.maxHp];

        for (int i = 0; i < MonsterStatusValue.maxHp; i++)
        {
            uiHpBargoArray[i] = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
            uiHpBargoArray[i].transform.SetParent(hpCanvas.GetAnchorRect());
            uiHpBarArray[i] = uiHpBargoArray[i].GetComponent<UIHPBar>();
        }

        int Center = (int)MonsterStatusValue.maxHp / 2;

        for (int i = 0; i < MonsterStatusValue.maxHp; i++)
        {
            uiHpBargoArray[i] = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
            uiHpBargoArray[i].transform.SetParent(hpCanvas.GetAnchorRect());
            uiHpBarArray[i] = uiHpBargoArray[i].GetComponent<UIHPBar>();
        }

        uiHpBarArray[Center].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        for (int i = Center; i < (int)MonsterStatusValue.maxHp - 1; i++)
        {
            uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        }

        for (int i = Center; i < 0; i--)
        {
            uiHpBarArray[i - 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition - hpUIInterval;
        }

        //for (int i = 0; i < MonsterStatusValue.maxHp; i += 3)
        //{
        //    uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        //    uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        //    uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
        //}

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        if (IsProgressAttack && !go.activeSelf)
        {
            go.GetComponent<WitchDoctorDollWeapon>().isPool = true;

        }

        int Center = (int)MonsterStatusValue.maxHp / 2;
        uiHpBarArray[Center].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        for (int i = Center; i < (int)MonsterStatusValue.maxHp - 1; i++)
        {
            uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        }

        for (int i = Center; i > 0; i--)
        {
            uiHpBarArray[i - 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition - hpUIInterval;
        }

        //for (int i = 0; i < MonsterStatusValue.maxHp; i += 3)
        //{
        //    uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        //    uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        //    uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
        //}
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

    void OnDrawGizmos() // 공격 범위 gizmos
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, viewRadius);
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
                if (!IsProgressAttack && monsterStatus != MonsterStatus.DEAD)
                {
                    Attack();
                    Debug.Log("Attack");
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
            GameObject go = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollRange", transform.position, Quaternion.Euler(90,0,0));
            go.transform.position = Attackplace + new Vector3(0,0.1f,0);
            StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollRange", go, 2.0f));
        }

        IsProgressAttack = true; // false 처리 해야함. LayerMask 활용
        IsAttackOneTouch = false;
        //go = Instantiate(bullet, launchPos);
        go = ObjectPooler.Instance.SpawnFromPool("WitchDoctorDollBullet", launchPos.position, Quaternion.identity);
        go.transform.SetParent(this.transform);
        //Launch(Attackplace);
        StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDollBullet", go, 2.0f));
    }

    public override void ProcessDead()
    {
        monsterStatus = MonsterStatus.DEAD;

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

        StartCoroutine(WitchDoctorDollDead());
    }

    IEnumerator WitchDoctorDollDead()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ObjectPooler.Instance.SpawnBack("WitchDoctorDoll", gameObject, 0));
    }

    IEnumerator DamageTime()
    {
        Time.timeScale = 0f;

        yield return timestop;

        Time.timeScale = 1f;

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

            PlayerManager.Instance.playerControll.playerAnimationEvent.SetDamageCheck(false);
            StartCoroutine(DamageTime());
            StartCoroutine(GameManager.Instance.cameraManager.camerashake.Shake(0.25f, 0.25f));
            GameObject Effect = ObjectPooler.Instance.SpawnFromPool("HitEffect", hittarget.transform.position, hittarget.transform.rotation);
            Effect.transform.parent = hittarget.transform;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));

            // 피격
            MonsterStatusValue.hp -= 1;

            if(monsterStatus != MonsterStatus.DEAD)
            {
                StartCoroutine(ObjectPooler.Instance.SpawnBack("MonsterHPUI", uiHpBargoArray[(int)MonsterStatusValue.hp], 0));
            }

            if (MonsterStatusValue.hp <= 0 && monsterStatus != MonsterStatus.DEAD) // 사망
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
