using DG.Tweening;
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
    protected override void Awake()
    {
        MonsterStatusValue.Initialize();
        tr = GetComponent<Transform>();
    }

    void Start()
    {

        timestop = new WaitForSecondsRealtime(TimeStop);

        //GameObject go = Instantiate(hpImage);
        //go.transform.SetParent(hpCanvas.GetAnchorRect());
        //go.transform.localScale = Vector3.one;
        //uiHpBar = go.GetComponent<UIHPBar>();
        //uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
        //uiHpBar.image.rectTransform.anchoredPosition = CameraManager.MainCamera.WorldToScreenPoint(HpTransform.position);
        //uiHpBar.UpdatePositionFromWorldPosition(HpTransform.position);

        //uiHpBargo = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
        //uiHpBargo.transform.SetParent(hpCanvas.GetAnchorRect());
        //uiHpBar = uiHpBargo.GetComponent<UIHPBar>();



        for (int i = 0; i < uiHpBargoArray.Length; i++)
        {
            uiHpBargoArray[i] = ObjectPooler.Instance.SpawnFromPool("MonsterHPUI", transform.position, Quaternion.identity);
            uiHpBargoArray[i].transform.SetParent(hpCanvas.GetAnchorRect());
            uiHpBarArray[i] = uiHpBargoArray[i].GetComponent<UIHPBar>();

            //uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[i].position);

        }

        uiHpBarArray[0].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);
        uiHpBarArray[1].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position + new Vector3(1, 0, 0));
        uiHpBarArray[2].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position - new Vector3(1, 0, 0));

        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[0].position);
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[1].position);
        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[2].position);

        this.monsterStatus = MonsterStatus.IDLE;
    }

    protected override void Update()
    {
        base.Update();

        //uiHpBar.image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransform.position);

        for (int i = 0; i < uiHpBargoArray.Length; i++)
        {
            uiHpBarArray[i].image.rectTransform.anchoredPosition = GameManager.Instance.cameraManager.GetMainCamera().WorldToScreenPoint(HpTransformArray[i].position);
        }

        if (monsterStatus == MonsterStatus.DEAD)
        {
            StartCoroutine(ObjectPooler.Instance.SpawnBack("MonsterHPUI", uiHpBargo, 0));
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
        --GameManager.Instance.maps[0].MapMonsterCount;
        GameManager.Instance.maps[0].CheckClearMonster();
        animator.SetBool("Dead", true);
    }

    public override void Dead()
    {
        base.Dead();
        
        StartCoroutine(DeadDelay());
        
    }

    IEnumerator DeadDelay()
    {
        Destroy(this.hpCanvas.GetComponentInChildren<UIHPBar>().gameObject);
     
        yield break;
    }

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
            StartCoroutine(DamageTime());
            StartCoroutine(GameManager.Instance.cameraManager.camerashake.Shake(0.25f, 0.25f));
            GameObject Effect = ObjectPooler.Instance.SpawnFromPool("HitEffect", hittarget.transform.position, hittarget.transform.rotation);
            Effect.transform.parent = hittarget.transform;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("HitEffect", Effect, 0.7f));

            // 피격
            AudioManager.Instance.PlaySoundSfx("ZombieDamage");

            MonsterStatusValue.hp -= 1;
            StartCoroutine(ObjectPooler.Instance.SpawnBack("MonsterHPUI", uiHpBargoArray[2], 0));

            if (MonsterStatusValue.hp <= 0 && !IsDead) // 사망
            {
                ProcessDead();
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
