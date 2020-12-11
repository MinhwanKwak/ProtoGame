using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BossControl : MonsterBasic
{
    public GameObject hittarget;

    WaitForSecondsRealtime timestop;
    public float TimeStop = 0f;


    public GameObject uiHpBargo;
    public GameObject[] uiHpBargoArray;
    protected override void Awake()
    {
        base.Awake();
        tr = GetComponent<Transform>();
    }

    protected override void Start()
    {
        base.Start();
        uiHpBargoArray = new GameObject[(int)MonsterStatusValue.maxHp];
        uiHpBarArray = new UIHPBar[(int)MonsterStatusValue.maxHp];
        timestop = new WaitForSecondsRealtime(TimeStop);

        for (int i = 0; i < MonsterStatusValue.maxHp; i++)
        {
            uiHpBargoArray[i] = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
            uiHpBargoArray[i].transform.SetParent(hpCanvas.GetAnchorRect());
            uiHpBarArray[i] = uiHpBargoArray[i].GetComponent<UIHPBar>();
        }
        //for (int i = 0; i < MonsterStatusValue.maxHp; i += 3)
        //{
        //    uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        //    uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        //    uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
        //}

        for (int i = 0; i < MonsterStatusValue.maxHp; i += 5)
        {
            if (i >= 4)
            {
                uiHpBarArray[i].image.rectTransform.anchoredPosition = uiHpBarArray[0].image.rectTransform.anchoredPosition + new Vector2(0, 25f);
                uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
                uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
                uiHpBarArray[i + 3].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 3);
                uiHpBarArray[i + 4].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 4);
            }
            else
            {
                uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
                uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
                uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
                uiHpBarArray[i + 3].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 3);
                uiHpBarArray[i + 4].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 4);
            }

        }



        this.monsterStatus = MonsterStatus.IDLE;
        
    }

    protected override void Update()
    {
       // uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        MonsterAttackDelayTime += Time.deltaTime;

        if(!IsDead)
        {
            TracePlayer();
        }

        for (int i = 0; i < MonsterStatusValue.maxHp; i += 5)
        {
            if(i >= 4)
            {
                uiHpBarArray[i].image.rectTransform.anchoredPosition = uiHpBarArray[0].image.rectTransform.anchoredPosition + new Vector2(0, 25f);
                uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
                uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
                uiHpBarArray[i + 3].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 3);
                uiHpBarArray[i + 4].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 4);
            }
            else
            {
                uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
                uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
                uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
                uiHpBarArray[i + 3].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 3);
                uiHpBarArray[i + 4].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 4);
            }

        }

        //if (IsDestination() && !IsInSight)
        //{

        //}
    }

    public override void Attack()
    {
        base.Attack();
        IsProgressAttack = true;
        tr.DOLookAt(PlayerManager.Instance.playerControll.transform.position, 0.4f);
    }

    private void TracePlayer()
    {
        //Nav.SetDestination(PlayerManager.Instance.playerControll.transform.position);
        IsInSight = true;
        if (Vector3.Distance(tr.position, PlayerManager.Instance.playerControll.transform.position) <= MonsterStatusValue.range && IsInSight && !IsProgressAttack)
        {
            MonsterAttackDelayTime = 0.0f;
            animator.SetTrigger("Attack");
            monsterStatus = MonsterStatus.ATTACK;
            Attack();
        }
        else if (Vector3.Distance(tr.position, PlayerManager.Instance.playerControll.transform.position) > MonsterStatusValue.range && IsInSight)
        {

            animator.SetTrigger("Walk");
            monsterStatus = MonsterStatus.RUN;
            //Nav.isStopped = false;
            Nav.SetDestination(PlayerManager.Instance.playerControll.transform.position);
        }
    }

    IEnumerator DamageTime()
    {
        Time.timeScale = 0f;

        yield return timestop;

        Time.timeScale = 1f;

    }
    public override void ProcessDead()
    {
        base.ProcessDead();
        monsterStatus = MonsterStatus.DEAD;
        Nav.isStopped = true;
        IsDead = true;
        //StartCoroutine(ObjectPooler.Instance.SpawnBack(thisname, gameObject, 0f)); //test 지워두됨
        //--GameManager.Instance.maps[0].MapMonsterCount;
        //GameManager.Instance.maps[0].CheckClearMonster();
        //animator.SetBool("Dead", true);
    }

    public override void Dead()
    {
        base.Dead();
        StartCoroutine(ObjectPooler.Instance.SpawnBack("Boss", gameObject, 0));
    }



    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & HitLayerMask) != 0 && PlayerManager.Instance.playerControll.playerAnimationEvent.GetDamageCheck())
        {

            //AudioManager.Instance.PlaySoundSfx("ZombieCollider");
            PlayerManager.Instance.playerControll.playerAnimationEvent.SetDamageCheck(false);
            StartCoroutine(DamageTime());
            StartCoroutine(GameManager.Instance.cameraManager.camerashake.Shake(0.25f, 0.25f));
            GameObject Effect = ObjectPooler.Instance.SpawnFromPool("HitEffect", hittarget.transform.position, hittarget.transform.rotation);
            Effect.transform.parent = hittarget.transform;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));

            // 피격
            //AudioManager.Instance.PlaySoundSfx("ZombieDamage");

            MonsterStatusValue.hp -= 1;

            if (monsterStatus != MonsterStatus.DEAD)
            {
                StartCoroutine(ObjectPooler.Instance.SpawnBack("MonsterHPUI", uiHpBargoArray[(int)MonsterStatusValue.hp], 0));
            }

            if (MonsterStatusValue.hp <= 0 && monsterStatus != MonsterStatus.DEAD) // 사망
            {
                ProcessDead();
            }
            else if (monsterStatus != MonsterStatus.DEAD && !IsProgressAttack)
            {
                animator.SetTrigger("BeAttacked");
                if (!IsInSight)
                {
                    tr.DOLookAt(PlayerManager.Instance.playerControll.transform.position, 0.2f);
                }

            }
        }
    }
}
