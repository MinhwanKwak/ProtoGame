using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class MonsterControl : MonsterBasic
{

    public const string IDLE = "Idle";

    private void Awake()
    {
        MonsterStatusValue.Initialize();
    }

    void Start()
    {
        GameObject go = Instantiate(hpImage);
        go.transform.SetParent(hpCanvas.GetAnchorRect());
        go.transform.localScale = Vector3.one;
        uiHpBar = go.GetComponent<UIHPBar>();
        uiHpBar.image.rectTransform.anchoredPosition = Camera.GetAnotherCamera().WorldToScreenPoint(HpTransform.position);
        //uiHpBar.image.rectTransform.anchoredPosition = CameraManager.MainCamera.WorldToScreenPoint(HpTransform.position);
        //uiHpBar.UpdatePositionFromWorldPosition(HpTransform.position);

        this.MonsterStatus = Status.IDLE;
        this.animator = animator.GetComponent<Animator>();
    }

    protected override void Update()
    {
        //SetAnimation();
        //animator.Play(IDLE);
        //ApproachToPlayer();
        //Attack();
        StartCoroutine(DelayAttack());
    }



    IEnumerator DelayAttack() // 테스트용
    {
        yield return new WaitForSeconds(3f);
        Attack();
    }
}
