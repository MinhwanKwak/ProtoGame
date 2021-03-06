﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControl : MonsterBasic
{
   

   public float TimeStop = 0f;

   WaitForSecondsRealtime timestop;

    public GameObject uiHpBargo;
    public GameObject[] uiHpBargoArray;

    public GameObject hittarget;

    Collider[] colliders;
    protected override void Awake()
    {
        MonsterStatusValue.Initialize();
        tr = GetComponent<Transform>();
    }

    protected override void Start()
    {
        base.Start();
        timestop = new WaitForSecondsRealtime(TimeStop);

        //for(int i = 0; i < MonsterStatusValue.maxHp; i++)
        //{
        //    uiHpBarArray[i] = new UIHPBar();
        //}

        uiHpBargoArray = new GameObject[(int)MonsterStatusValue.maxHp];
        uiHpBarArray = new UIHPBar[(int)MonsterStatusValue.maxHp];


        //GameObject go = Instantiate(hpImage);
        //go.transform.SetParent(hpCanvas.GetAnchorRect());
        //go.transform.localScale = Vector3.one;
        //uiHpBar = go.GetComponent<UIHPBar>();
        //uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
        //uiHpBar.image.rectTransform.anchoredPosition = CameraManager.MainCamera.WorldToScreenPoint(HpTransform.position);

        //uiHpBargo = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
        //uiHpBargo.transform.SetParent(hpCanvas.GetAnchorRect());
        //uiHpBar = uiHpBargo.GetComponent<UIHPBar>();

        int Center = (int)MonsterStatusValue.maxHp / 2;


        for (int i = 0; i < MonsterStatusValue.maxHp; i++)
        {
            uiHpBargoArray[i] = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
            uiHpBargoArray[i].transform.SetParent(hpCanvas.GetAnchorRect());
            uiHpBarArray[i] = uiHpBargoArray[i].GetComponent<UIHPBar>();
        }



        uiHpBarArray[Center].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        for(int i = Center; i<(int)MonsterStatusValue.maxHp-1;i++)
        {
            uiHpBarArray[i+1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        }

        for(int i = Center; i>0; i--)
        {
            uiHpBarArray[i - 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition - hpUIInterval;
        }

        //for (int i = 0; i < MonsterStatusValue.maxHp; i += 3)
        //{
        //    uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        //    uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        //    uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
        //}

       

        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[0].position);
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[1].position);
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[2].position);

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
        base.Update();
        if (IsDead || monsterStatus == MonsterStatus.DEAD)
            return;

        //for (int i = 0; i < MonsterStatusValue.maxHp; i += 3)
        //{
        //    uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        //    uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        //    uiHpBarArray[i + 2].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + (hpUIInterval * 2);
        //}
        int Center = (int)MonsterStatusValue.maxHp / 2;
        uiHpBarArray[Center].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        for (int i = Center; i < (int)MonsterStatusValue.maxHp-1; i++)
        {
            uiHpBarArray[i + 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition + hpUIInterval;
        }

        for (int i = Center; i > 0; i--)
        {
            uiHpBarArray[i - 1].image.rectTransform.anchoredPosition = uiHpBarArray[i].image.rectTransform.anchoredPosition - hpUIInterval;
        }

    }

   

    public override void Idle()
    {
        base.Idle();
        monsterStatus = MonsterStatus.IDLE;
        Nav.isStopped = false;
        animator.SetTrigger("Idle");

    }

    public override void Attack()
    {
        base.Attack();
        IsProgressAttack = true;

        //monsterStatus = MonsterStatus.ATTACK;
        //Nav.isStopped = true;
        //animator.SetTrigger("Attack");
         
        //Vector3 transform = new Vector3(playerPos.position.x, 0, playerPos.position.z);
        //tr.LookAt(playerPos);
        tr.DOLookAt(PlayerManager.Instance.playerControll.transform.position, 0.2f);
        //tr.DOLookAt(transform, 0.2f);
    }

    public override void ReceivedAttack()
    {
        base.ReceivedAttack();
        monsterStatus = MonsterStatus.RECEIVEDATTACK;
        //Nav.isStopped = true;
        animator.SetTrigger("ReceivedAttack");
        MonsterStatusValue.hp -= 5;

        uiHpBar.SetHPUIFill();

    }

    public override void ProcessDead()
    {
        base.ProcessDead();
        monsterStatus = MonsterStatus.DEAD;
        Nav.isStopped = true;
        IsDead = true;


        
        //StartCoroutine(ObjectPooler.Instance.SpawnBack(thisname, gameObject, 0f)); //test 지워두됨
        //for (int i  = 0; i  < GameManager.Instance.maps.Length; ++i)
        //{
        //    if (gameObject.tag == GameManager.Instance.maps[i].tag)
        //    {
        //        --GameManager.Instance.maps[i].MapMonsterCount;
        //        if(GameManager.Instance.maps[i].MapMonsterCount <= 0)
        //        {
        //            GameManager.Instance.maps[i].DoorAnim[0].SetTrigger("DoorOpen");
        //            return;
        //        }
        //    }
        //}
    }

    public override void Dead()
    {
        base.Dead();

        StartCoroutine(ObjectPooler.Instance.SpawnBack("Zombie", gameObject, 0));
        //StartCoroutine(DeadDelay());
        
    }

    //IEnumerator DeadDelay()
    //{
        //Destroy(this.hpCanvas.GetComponentInChildren<UIHPBar>().gameObject);
     
        //yield break;
    //}

    public override void ApproachToPlayer()
    {
        base.ApproachToPlayer();
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
        else if(!IsProgressAttack)
        {
            animator.SetTrigger("Idle");
            monsterStatus = MonsterStatus.IDLE;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & HitLayerMask) != 0 && PlayerManager.Instance.playerControll.playerAnimationEvent.GetDamageCheck())
        {

            AudioManager.Instance.PlaySoundSfx("ZombieCollider");
            PlayerManager.Instance.playerControll.playerAnimationEvent.SetDamageCheck(false);
           // StartCoroutine(DamageTime());
            StartCoroutine(GameManager.Instance.cameraManager.camerashake.Shake(0.25f, 0.25f));
            GameObject Effect = ObjectPooler.Instance.SpawnFromPool("HitEffect", hittarget.transform.position, hittarget.transform.rotation);
            Effect.transform.parent = hittarget.transform;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));

            // 피격
            AudioManager.Instance.PlaySoundSfx("ZombieDamage");

            MonsterStatusValue.hp -= 1;

            if (!IsDead)
            {
                StartCoroutine(ObjectPooler.Instance.SpawnBack("MonsterHPUI", uiHpBargoArray[(int)MonsterStatusValue.hp], 0));
            }

            if (MonsterStatusValue.hp <= 0 && !IsDead) // 사망
            {
                ProcessDead();
                colliders = this.gameObject.GetComponentsInChildren<Collider>();
                for(int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }
            }
            else if(!IsDead && !IsProgressAttack)
            {
                animator.SetTrigger("BeAttacked");
                
                if (!IsInSight)
                {
                    tr.DOLookAt(PlayerManager.Instance.playerControll.transform.position, 0.2f);
                }
                
            }
  
        }
    }

    IEnumerator DamageTime()
    {
        Time.timeScale = 0f;

        yield return timestop;

        Time.timeScale = 1f;

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
