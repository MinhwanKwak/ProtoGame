using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : ItemBase
{
    public int Armor = 1;

    private void Start()
    {
        Initialize(ItemStatus.ARMORITEM);
    }


    public override IEnumerator ItemGet()
    {
        if(PlayerManager.Instance.Armor >= PlayerManager.Instance.maxArmor)
        {
            Debug.Log("최대치 입니다.");
            yield break;
        }
        PlayerManager.Instance.Armor += Armor;
        PlayerManager.Instance.PlayerUI.ArmorUIs[PlayerManager.Instance.Armor - 1].isArmorToggl = true;
        meshRenderer.enabled = false;
        HitCollider.enabled = false;
        yield return RespawnTime;

        meshRenderer.enabled = true;
        HitCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << targetLayer) & other.gameObject.layer) != 0)
        {
            StartCoroutine(ItemGet());
            PlayerManager.Instance.PlayerUI.UpdateDisplayUI();

        }
    }
}
