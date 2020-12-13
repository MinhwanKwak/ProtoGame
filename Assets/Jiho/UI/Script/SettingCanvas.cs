using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SettingCanvas : MonoBehaviour
{
    public GameObject catHead;
    public GameObject catChin;

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

    public Vector3 catHeadOriginPosition;
    public Vector3 catChinOriginPosition;

    Vector3 OptionUIPanelOriginPosition;

    Vector3 RaiseScale = new Vector3(0.2f, 0.2f, 0);
    Vector3 DecreaseScale = new Vector3(0.2f, 0.2f, 0);

    WaitForSeconds DelayTime = new WaitForSeconds(0.7f);
    WaitForSeconds DownShowTime = new WaitForSeconds(0.3f);

    public GameObject BackPanel;
    public GameObject Panel;
    public GameObject SoundUIPanel;
    public GameObject OptionUIPanel;


    void Start()
    {
        SelectSwordImg.transform.position = ContinueButton.transform.position;

        catHeadOriginPosition = catHead.transform.position;
        catChinOriginPosition = catChin.transform.position;

        OptionUIPanelOriginPosition = OptionUIPanel.transform.position;

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
        SelectSwordImg.transform.DOMove(ContinueButton.transform.position, 0.3f);
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

    public void SettingButtonEnter()
    {
        SelectSwordImg.transform.DOMove(SettingButton.transform.position, 0.3f);
        SettingButton.transform.DOScale(SettingButtonRaiseScale, 0.5f);

    }

    public void SettingButtonDown()
    {
        StartCoroutine(SettingButtonShowDown());
    }

    IEnumerator SettingButtonShowDown()
    {
        SettingButton.transform.DOScale(SettingButtonDecreaseScale, 0.3f);
        yield return DownShowTime;
        SettingButton.transform.DOScale(SettingButtonRaiseScale, 0.3f);
    }

    public void SettingButtonClick()
    {
        ContinueButton.image.DOFade(0, 0.2f);
        EndGameButton.image.DOFade(0, 0.2f);
        catHead.transform.DOMoveY(ContinueButton.transform.position.y+20, 0.3f);
        catChin.transform.DOMoveY(EndGameButton.transform.position.y, 0.3f);

        StartCoroutine(DelaySettingClick());
    }

    IEnumerator DelaySettingClick()
    {
        yield return new WaitForSeconds(0.4f);
        Panel.SetActive(false);

        OptionUIPanel.transform.position = new Vector3(Panel.transform.position.x, OptionUIPanel.transform.position.y, OptionUIPanel.transform.position.z);
        OptionUIPanel.SetActive(true);
        OptionUIPanel.transform.DOMove(OptionUIPanelOriginPosition, 0.2f);

        yield return new WaitForSeconds(0.2f);
        SoundUIPanel.SetActive(true);
    }

public void SettingButtonExit()
    {
        SettingButton.transform.DOScale(OriginSettingButtonLocalScale, 0.5f);
    }

    public void EndButtonEnter()
    {
        SelectSwordImg.transform.DOMove(EndGameButton.transform.position, 0.3f);
        EndGameButton.transform.DOScale(EndButtonRaiseScale, 0.5f);
    }

    public void EndButtonDown()
    {
        StartCoroutine(EndButtonShowDown());
    }

    IEnumerator EndButtonShowDown()
    {
        EndGameButton.transform.DOScale(EndButtonDecreaseScale, 0.3f);
        yield return DownShowTime;
        EndGameButton.transform.DOScale(EndButtonRaiseScale, 0.3f);
    }

    public void EndButtonClick()
    {
        StartCoroutine(DelayEnd());
    }

    IEnumerator DelayEnd()
    {
        yield return DelayTime;
        DOTween.Kill(this.gameObject);
        SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
    }

    public void EndButtonExit()
    {
        EndGameButton.transform.DOScale(OriginEndButtonLocalScale, 0.5f);
    }

}
