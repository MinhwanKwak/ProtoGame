using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
public struct HalfHPUIToggle
{
    public GameObject halfHpUI;
    public bool ishalfHpToggle;
};


[System.Serializable]
public struct AttackUIToggle
{
    public GameObject AttackBuffUI;
    public bool IsAttackBufToggle;
};


public class PlayerUI : UIBase
{
    public AttackUIToggle[] AttackUIs;
    public HpUIToggle[] HpUIs;
    public HalfHPUIToggle[] halfHPUIs;
    public ArmorUIToggle[] ArmorUIs;


    public Texture2D MouseClick;
    public Texture2D MouseNonClick;

    public GameObject DashCoolTimeImage;
    public GameObject DashCoolTimeSpacebarImage;
    public GameObject AttackCoolTimeImage;
    public GameObject AttackCoolTimeMouseImage;

    public Image[] DashChargeIconUI;
    public Sprite DashChargeIcon;
    public Sprite DashDechargeIcon;

    public bool IsDashCool = false;
    public bool IsAttackCool = false;

    public float SetCoolTime = 0.0f;


    private void Start()
    {
        Cursor.SetCursor(MouseClick, Vector2.zero, CursorMode.Auto);
        for (int i = 0; i < PlayerManager.Instance.Armor; ++i) { ArmorUIs[i].isArmorToggl = true; }
        for (int i = 0; i < PlayerManager.Instance.Hp / 2; ++i) { HpUIs[i].isHpToggle = true; halfHPUIs[i].ishalfHpToggle = false; }
        for (int i = 0; i < PlayerManager.Instance.AttackPower; ++i) { AttackUIs[i].IsAttackBufToggle = true; }
        UpdateDisplayUI();

        DashCoolTimeImage.SetActive(false);
        DashCoolTimeSpacebarImage.SetActive(false);
        AttackCoolTimeImage.SetActive(false);
        AttackCoolTimeMouseImage.SetActive(false);
    }

    public void UpdateDashUI(int value)
    {
        switch(value)
        {
            case 3:
                //DashChargeIconUI[value].sprite = DashDechargeIcon;
                DashChargeIconUI[value - 1].sprite = DashChargeIcon;
                break;
            case 2:
                   DashChargeIconUI[value].sprite = DashDechargeIcon;
                DashChargeIconUI[value - 1].sprite = DashChargeIcon;
                break;
            case 1:
                DashChargeIconUI[value].sprite = DashDechargeIcon;
                DashChargeIconUI[value - 1].sprite = DashChargeIcon;
                DashCoolTimeImage.SetActive(false);
                DashCoolTimeSpacebarImage.SetActive(false);
                break;
            case 0:
                DashChargeIconUI[value].sprite = DashDechargeIcon;
                DashCoolTimeImage.SetActive(true);
                DashCoolTimeSpacebarImage.SetActive(true);
                break;
        }
    }

    public void UpdateAttackUI(PlayerStatus status)
    {
        if(PlayerManager.Instance.playerControll.GetAttack())
        {
            AttackCoolTimeImage.SetActive(true);
            AttackCoolTimeMouseImage.SetActive(true);
        }
        else
        {
            AttackCoolTimeImage.SetActive(false);
            AttackCoolTimeMouseImage.SetActive(false);
        }
    }

    public void UpdateDisplayUI()
    {
        for (int i = 0; i < PlayerManager.Instance.maxArmor; ++i)
        {
            if (ArmorUIs[i].isArmorToggl)
            {
                ArmorUIs[i].ArmorUI.SetActive(true);
            }
            else
            {
                ArmorUIs[i].ArmorUI.SetActive(false);
            }
        }
        for (int i = 0; i < PlayerManager.Instance.maxHp - 5; ++i)
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
        for (int i = 0; i < PlayerManager.Instance.maxHp - 5; i++)
        {
            if (halfHPUIs[i].ishalfHpToggle)
            {
                halfHPUIs[i].halfHpUI.SetActive(true);
            }
            else
            {
                halfHPUIs[i].halfHpUI.SetActive(false);
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

    //IEnumerator UpdateCoolTime(GameObject image)
    //{
    //    Image CoolImage = image.GetComponent<Image>();
    //    CoolImage.fillAmount = 1;
    //    while (CoolImage.fillAmount > 0)
    //    {
    //        CoolImage.fillAmount -= 1 * Time.deltaTime / SetCoolTime;
    //        yield return null;
    //    }
    //    yield break;

    //}
}
