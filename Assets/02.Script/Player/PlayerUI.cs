﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArmorUIToggle
{
    public GameObject ArmorUI;
    public bool isArmorToggl;
};

[System.Serializable]
public struct HpUIToggle
{
    public GameObject HpUI;
    public bool isHpToggle;
};


[System.Serializable]
public struct AttackUIToggle
{
    public GameObject AttackBuffUI;
    public bool IsAttackBufToggle;
};


public class PlayerUI : UIBase
{
    public  AttackUIToggle[] AttackUIs;
    public HpUIToggle[] HpUIs;
    public ArmorUIToggle[] ArmorUIs;



    private void Start()
    {
        for (int i = 0; i < PlayerManager.Instance.Armor; ++i) { ArmorUIs[i].isArmorToggl = true; }
        for (int i = 0; i < PlayerManager.Instance.Hp; ++i) { HpUIs[i].isHpToggle = true; }
        for (int i = 0; i < PlayerManager.Instance.AttackPower; ++i) { AttackUIs[i].IsAttackBufToggle = true; }
        UpdateDisplayUI();
    }


    public void UpdateDisplayUI()
    {
        for(int i = 0;  i <  PlayerManager.Instance.maxArmor; ++i)
        {
           if(ArmorUIs[i].isArmorToggl)
            {
                ArmorUIs[i].ArmorUI.SetActive(true);
            }
            else
            {
                ArmorUIs[i].ArmorUI.SetActive(false);
            }
        }
        for (int i = 0; i < PlayerManager.Instance.maxHp; ++i)
        {
            if (HpUIs[i].isHpToggle)
            {
                HpUIs[i].HpUI.SetActive(true);
            }
            else
            {
                HpUIs[i].HpUI.SetActive(false);
            }
        }
        for (int i = 0; i < PlayerManager.Instance.maxAttackPower; ++i)
        {
            if (AttackUIs[i].IsAttackBufToggle)
            {
                AttackUIs[i].AttackBuffUI.SetActive(true);
            }
            else
            {
                AttackUIs[i].AttackBuffUI.SetActive(false);
            }
        }
    }


}