using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : ItemBase
{
    public int Power = 1;

    private void Start()
    {
        Initialize(ItemStatus.ATTACKITEM);
    }


    public override IEnumerator ItemGet()
    {
        if (PlayerManager.Instance.AttackPower >= PlayerManager.Instance.maxAttackPower)
        {
            Debug.Log("최대치 입니다.");
            yield break;
        }
        PlayerManager.Instance.AttackPower += Power;
        PlayerManager.Instance.PlayerUI.AttackUIs[PlayerManager.Instance.AttackPower - 1].IsAttackBufToggle = true;
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
