 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
     public PlayerControll playerControll;
     public PlayerAnimationEvent PlayerAnimationEvent;
     public PlayerPrefs PlayerPrefs;
     public PlayerUI PlayerUI;
     public static PlayerManager Instance;

    [Range(0, 10)]
    public int Hp;
    [Range(0, 5)]
    public int Armor;
    [Range(0,10)]
    public int AttackPower;

    public int maxHp;
    public int maxArmor;
    public int maxAttackPower;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        maxHp = 10;
        maxArmor = 5;
        maxAttackPower = 5;
    }

    private void Update()
    {
        CheckDashCount();
        if(transform.position.y <= -10)
        {
            playerControll.animator.SetTrigger("Dead");
            GameManager.Instance.LoseUI.SetActive(true);
            Destroy(gameObject, 1.5f);
            playerControll.playerStatu = PlayerStatus.DEAD;
            Debug.Log("Dead");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        bool ProgressAttackCheck = false;

        if (((1 << other.gameObject.layer) & playerControll.monsterWeaponLayer) != 0)
        {
            ProgressAttackCheck = other.gameObject.GetComponentInParent<MonsterBasic>().IsProgressAttack;
        }

        if (((1 << other.gameObject.layer) & playerControll.monsterWeaponLayer) != 0 && ProgressAttackCheck)
        {
            if(!other.gameObject.GetComponentInParent<MonsterBasic>().IsAttackOneTouch)
            {
                Damage();
            }

            if (other.gameObject.GetComponentInParent<BossControl>() != null && other.gameObject.GetComponentInParent<BossControl>().name == "Boss")
            {
                GameObject go = ObjectPooler.Instance.SpawnFromPool("BossHitEffect", playerControll.Hittransform.position, Quaternion.identity);
                go.transform.SetParent(playerControll.Hittransform);
                go.transform.localScale = Vector3.one;

                StartCoroutine(ObjectPooler.Instance.SpawnBack("BossHitEffect", go, 0.5f));
            }

            other.gameObject.GetComponentInParent<MonsterBasic>().IsAttackOneTouch = true;

        }

    }

    public void Damage()
    {
        DeadCheck();
        if(Armor > 0)
        {
            --Armor;
            PlayerUI.ArmorUIs[Armor].isArmorToggl = false;
        }
        else
        {
            --Hp;
            //PlayerUI.HpUIs[Hp].isHpToggle = false;

            switch(Hp)
            {
                case 10:
                    break;
                case 9:
                    PlayerUI.HpUIs[4].isHpToggle = false;
                    PlayerUI.halfHPUIs[4].ishalfHpToggle = true;
                    break;

                case 8:
                    PlayerUI.halfHPUIs[4].ishalfHpToggle = false;
                    break;

                case 7:
                    PlayerUI.HpUIs[3].isHpToggle = false;
                    PlayerUI.halfHPUIs[3].ishalfHpToggle = true;
                    break;

                case 6:
                    PlayerUI.halfHPUIs[3].ishalfHpToggle = false;
                    break;

                case 5:
                    PlayerUI.HpUIs[2].isHpToggle = false;
                    PlayerUI.halfHPUIs[2].ishalfHpToggle = true;
                    break;

                case 4:
                    PlayerUI.halfHPUIs[2].ishalfHpToggle = false;
                    break;

                case 3:
                    PlayerUI.HpUIs[1].isHpToggle = false;
                    PlayerUI.halfHPUIs[1].ishalfHpToggle = true;
                    break;

                case 2:
                    PlayerUI.halfHPUIs[1].ishalfHpToggle = false;
                    break;

                case 1:
                    PlayerUI.HpUIs[0].isHpToggle = false;
                    PlayerUI.halfHPUIs[0].ishalfHpToggle = true;
                    break;

                case 0:
                    PlayerUI.halfHPUIs[0].ishalfHpToggle = false;
                    break;
            }
        }

        PlayerUI.UpdateDisplayUI();

    }

    public void CheckDashCount()
    {
        PlayerUI.UpdateDashUI(playerControll.Dashcount);
        PlayerUI.UpdateAttackUI(playerControll.playerStatu);
    }

    public void DeadCheck()
    {
        if(Hp <= 0)
        {
            playerControll.animator.SetTrigger("Dead");
            GameManager.Instance.LoseUI.SetActive(true);
            Destroy(gameObject, 1.5f);
            playerControll.playerStatu = PlayerStatus.DEAD;
            Debug.Log("Dead");
        }
    }

    public void Vicitory()
    {
        playerControll.playerStatu = PlayerStatus.DEAD;
        GameManager.Instance.VictoryUI.SetActive(true);
        Destroy(gameObject, 1.5f);
        Debug.Log("Victory");
    }

}
