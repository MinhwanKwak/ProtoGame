using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnotherMonsterControl : MonsterBasic
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(hpImage, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(hpCanvas.GetAnchorRect());
        go.transform.localScale = Vector3.one;
        uiHpBar = go.GetComponent<UIHPBar>();
        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
        //uiHpBar.UpdatePositionFromWorldPosition(HpTransform.position);
    }

    protected override void Update()
    {
        base.Update();
        //ApproachToPlayer();
        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
    }
}
