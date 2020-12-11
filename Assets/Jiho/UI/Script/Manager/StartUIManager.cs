using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartUIManager : MonoBehaviour
{
    public static StartUIManager Instance;

    //Vector2 mousePosition;

    public Button StartGameButton;
    public Button EndGameButton;
    public Button SettingButton;
    public Button CreatorButton;

    public Image SelectSwordImg;

    Vector3 OriginStartButtonLocalScale;
    Vector3 OriginEndButtonLocalScale;
    Vector3 OriginSettingButtonLocalScale;
    Vector3 OriginCreatorButtonLocalScale;

    Vector3 StartButtonRaiseScale;
    Vector3 EndButtonRaiseScale;
    Vector3 SettingButtonRaiseScale;
    Vector3 CreatorButtonRaiseScale;

    Vector3 StartButtonDecreaseScale;
    Vector3 EndButtonDecreaseScale;
    Vector3 SettingButtonDecreaseScale;
    Vector3 CreatorButtonDecreaseScale;

    Vector3 RaiseScale = new Vector3(0.2f, 0.2f, 0);
    Vector3 DecreaseScale = new Vector3(0.2f, 0.2f, 0);

    WaitForSeconds DelayTime = new WaitForSeconds(0.7f);
    WaitForSeconds DownShowTime = new WaitForSeconds(0.3f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        SelectSwordImg.transform.position = StartGameButton.transform.position;

        OriginStartButtonLocalScale = StartGameButton.transform.localScale;
        OriginEndButtonLocalScale = EndGameButton.transform.localScale;
        OriginSettingButtonLocalScale = SettingButton.transform.localScale;
        OriginCreatorButtonLocalScale = CreatorButton.transform.localScale;

        StartButtonRaiseScale = OriginStartButtonLocalScale + RaiseScale;
        EndButtonRaiseScale = OriginEndButtonLocalScale + RaiseScale;
        SettingButtonRaiseScale = OriginSettingButtonLocalScale + RaiseScale;
        CreatorButtonRaiseScale = OriginCreatorButtonLocalScale + RaiseScale;

        StartButtonDecreaseScale = OriginStartButtonLocalScale - DecreaseScale;
        EndButtonDecreaseScale = OriginEndButtonLocalScale - DecreaseScale;
        SettingButtonDecreaseScale = OriginSettingButtonLocalScale - DecreaseScale;
        CreatorButtonDecreaseScale = OriginCreatorButtonLocalScale - DecreaseScale;

    }

    void Update()
    {
        //mousePosition = Input.mousePosition;

    }

    public void StartButtonEnter()
    {
        SelectSwordImg.transform.DOMove(StartGameButton.transform.position, 0.4f);
        StartGameButton.transform.DOScale(StartButtonRaiseScale, 0.5f);
    }

    public void StartButtonDown()
    {
        StartCoroutine(StartButtonShowDown());
    }

    IEnumerator StartButtonShowDown()
    {
        StartGameButton.transform.DOScale(StartButtonDecreaseScale, 0.3f);
        yield return DownShowTime;
        StartGameButton.transform.DOScale(StartButtonRaiseScale, 0.3f);
        //}
    }

    //public void StartButtonUP()
    //{
    //    StartGameButton.transform.DOScale(StartButtonRaiseScale, 0.5f);
    //}


    public void StartButtonClick()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return DelayTime;

        SceneManager.LoadSceneAsync("LastScene", LoadSceneMode.Single);
    }

    public void StartButtonExit()
    {
        StartGameButton.transform.DOScale(OriginStartButtonLocalScale, 0.5f);
    }


    public void EndButtonEnter()
    {
        SelectSwordImg.transform.DOMove(EndGameButton.transform.position, 0.4f);
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

    //public void EndButtonUP()
    //{
    //    EndGameButton.transform.DOScale(EndButtonRaiseScale, 0.5f);
    //}

    public void EndButtonClick()
    {
        StartCoroutine(DelayEnd());
    }

    IEnumerator DelayEnd()
    {
        yield return DelayTime;

        if (Application.isPlaying)
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
    } 

    public void EndButtonExit()
    {
        EndGameButton.transform.DOScale(OriginEndButtonLocalScale, 0.5f);
    }

    public void SettingButtonEnter()
    {
        SelectSwordImg.transform.DOMove(SettingButton.transform.position, 0.4f);
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

    //public void SettingButtonUp()
    //{
    //    SettingButton.transform.DOScale(SettingButtonRaiseScale, 0.5f);
    //}

    public void SettingButtonExit()
    {
        SettingButton.transform.DOScale(OriginSettingButtonLocalScale, 0.5f);
    }

    public void CreatorButtonEnter()
    {
        SelectSwordImg.transform.DOMove(CreatorButton.transform.position, 0.4f);
        CreatorButton.transform.DOScale(CreatorButtonRaiseScale, 0.5f);
    }

    public void CreatorButtonDown()
    {
         StartCoroutine(CreatorButtonShowDown());
        
    }

    IEnumerator CreatorButtonShowDown()
    {
        CreatorButton.transform.DOScale(CreatorButtonDecreaseScale, 0.3f);
        yield return DownShowTime;
        CreatorButton.transform.DOScale(CreatorButtonRaiseScale, 0.3f);
    }

    //public void CreatorButtonUp()
    //{
    //    CreatorButton.transform.DOScale(CreatorButtonRaiseScale, 0.5f);
    //}

    public void CreatorButtonExit()
    {
        CreatorButton.transform.DOScale(OriginCreatorButtonLocalScale, 0.5f);
    }
}
