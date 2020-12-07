﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BossControl : MonsterBasic
{
    GameObject uiHpBargo;
    GameObject hittarget;

    WaitForSecondsRealtime timestop;
    public float TimeStop = 0f;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        timestop = new WaitForSecondsRealtime(TimeStop);

        uiHpBargo = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
        uiHpBargo.transform.SetParent(hpCanvas.GetAnchorRect());
        uiHpBar = uiHpBargo.GetComponent<UIHPBar>();
        uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
        uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        MonsterAttackDelayTime += Time.deltaTime;

        TracePlayer();

        if (IsDestination() && !IsInSight)
        {

        }
    }

    public override void Attack()
    {
        base.Attack();
        tr.DOLookAt(PlayerManager.Instance.playerControll.transform.position, 0.4f);
    }

    private void TracePlayer()
    {
        //Nav.SetDestination(PlayerManager.Instance.playerControll.transform.position);

        if (Vector3.Distance(tr.position, PlayerManager.Instance.playerControll.transform.position) <= MonsterStatusValue.range && IsInSight && !IsProgressAttack && MonsterAttackDelayTime >= 2f)
        {
            MonsterAttackDelayTime = 0.0f;
            animator.SetTrigger("Attack");
            monsterStatus = MonsterStatus.ATTACK;
            Attack();
        }
        else if (Vector3.Distance(tr.position, PlayerManager.Instance.playerControll.transform.position) > MonsterStatusValue.range && IsInSight)
        {

            animator.SetTrigger("Run");
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

            if (MonsterStatusValue.hp <= 0 && !IsDead) // 사망
            {
                ProcessDead();
            }
            else if (!IsDead && !IsProgressAttack)
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
