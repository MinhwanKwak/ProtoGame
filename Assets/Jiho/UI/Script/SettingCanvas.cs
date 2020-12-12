using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingCanvas : MonoBehaviour
{   
    public Button ContinueButton;
    public Button EndGameButton;
    public Button SettingButton;

    public Image SelectSwordImg;

    Vector3 OriginContinueButtonLocalScale;
    Vector3 OriginEndButtonLocalScale;
    Vector3 OriginSettingButtonLocalScale;

    Vector3 ContinueButtonRaiseScale;
    Vector3 EndButtonRaiseScale;
    Vector3 SettingButtonRaiseScale;

    Vector3 ContinueButtonDecreaseScale;
    Vector3 EndButtonDecreaseScale;
    Vector3 SettingButtonDecreaseScale;

    Vector3 RaiseScale = new Vector3(0.2f, 0.2f, 0);
    Vector3 DecreaseScale = new Vector3(0.2f, 0.2f, 0);

    WaitForSeconds DelayTime = new WaitForSeconds(0.7f);
    WaitForSeconds DownShowTime = new WaitForSeconds(0.3f);

    void Start()
    {
        SelectSwordImg.transform.position = ContinueButton.transform.position;

        OriginContinueButtonLocalScale = ContinueButton.transform.localScale;
        OriginEndButtonLocalScale = EndGameButton.transform.localScale;
        OriginSettingButtonLocalScale = SettingButton.transform.localScale;
       
        ContinueButtonRaiseScale = OriginContinueButtonLocalScale + RaiseScale;
        EndButtonRaiseScale = OriginEndButtonLocalScale + RaiseScale;
        SettingButtonRaiseScale = OriginSettingButtonLocalScale + RaiseScale;
        

        ContinueButtonDecreaseScale = OriginContinueButtonLocalScale - DecreaseScale;
        EndButtonDecreaseScale = OriginEndButtonLocalScale - DecreaseScale;
        SettingButtonDecreaseScale = OriginSettingButtonLocalScale - DecreaseScale;   
    }

    public void ContinueButtonEnter()
    {
        SelectSwordImg.transform.DOMove(ContinueButton.transform.position, 0.4f);
        ContinueButton.transform.DOScale(ContinueButtonRaiseScale, 0.5f);
    }

    public void ContinueButtonDown()
    {
        StartCoroutine(ContinueButtonShowDown());
    }

    IEnumerator ContinueButtonShowDown()
    {
        ContinueButton.transform.DOScale(ContinueButtonDecreaseScale, 0.3f);
        yield return DownShowTime;
        ContinueButton.transform.DOScale(ContinueButtonRaiseScale, 0.3f);
        //}
    }

    public void ContinueButtonClick()
    {
        StartCoroutine(DelayContinue());
    }
    IEnumerator DelayContinue()
    {
        yield return DelayTime;
        // 계속하기 실행
    }

    public void ContinueButtonExit()
    {
        ContinueButton.transform.DOScale(OriginContinueButtonLocalScale, 0.5f);
    }
}
