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
    public  AttackUIToggle[] AttackUIs;
    public HpUIToggle[] HpUIs;
    public HalfHPUIToggle[] halfHPUIs;
    public ArmorUIToggle[] ArmorUIs;


    public Texture2D MouseClick;
    public Texture2D MouseNonClick;

    public Image DashCoolTimeImage;
    public Image AttackCoolTimeImage;

    public Image[] DashChargeIcon;

    public bool IsDashCool = false;
    public bool IsAttackCool = false;

    public float SetCoolTime = 0.0f;


    private void Start()
    {
        Cursor.SetCursor(MouseClick, Vector2.zero, CursorMode.Auto);
        for (int i = 0; i < PlayerManager.Instance.Armor; ++i) { ArmorUIs[i].isArmorToggl = true; }
        for (int i = 0; i < PlayerManager.Instance.Hp/2; ++i) { HpUIs[i].isHpToggle = true; halfHPUIs[i].ishalfHpToggle = false; }
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
        for (int i = 0; i < PlayerManager.Instance.maxHp-5; ++i)
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
        for(int i = 0; i < PlayerManager.Instance.maxHp-5; i++)
        {
            if(halfHPUIs[i].ishalfHpToggle)
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

    public void StartCoolTime()
    {
        if(IsDashCool)
        {
            DashCoolTimeImage.GetComponent<GameObject>().SetActive(true);
            StartCoroutine(UpdateCoolTime(DashCoolTimeImage));
        }
        else if(!IsDashCool)
        {
            DashCoolTimeImage.GetComponent<GameObject>().SetActive(false);
        }

        if(IsAttackCool)
        {
            AttackCoolTimeImage.GetComponent<GameObject>().SetActive(true);
        }
        else if(!IsAttackCool)
        {
            AttackCoolTimeImage.GetComponent<GameObject>().SetActive(false);
        }
        
    }

    IEnumerator UpdateCoolTime(Image image)
    {
        while(image.fillAmount > 0)
        {
            image.fillAmount -= 1 * Time.deltaTime / SetCoolTime;
            yield return null;
        }
        yield break;
    }


}
