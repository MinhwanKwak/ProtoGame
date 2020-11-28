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



    [Range(0, 5)]
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
        maxHp = 5;
        maxArmor = 5;
        maxAttackPower = 5;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        bool IsAttackTime = other.gameObject.GetComponent<MonsterBasic>().IsProgressAttack;

        if (((1 << other.gameObject.layer) & playerControll.monsterWeaponLayer) != 0 && IsAttackTime)
        {
            Damage();
            //check player에 armor가 있는지 없는지 hp 가 있는지 
            //check후 ui에 표시 
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
            PlayerUI.HpUIs[Hp].isHpToggle = false;
        }

        PlayerUI.UpdateDisplayUI();

    }

    public void DeadCheck()
    {
        if(Hp <= 0)
        {
            Debug.Log("Dead");
           // Time.timeScale = 0;
        }
    }

}
