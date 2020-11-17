using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Title : MonoBehaviour
{
    public Image[] CutScene;
    int next;
    public Sprite[]ChoiceBuottonImage;
    public Button[] _Button;
    // Update is called once per frame
    void Update()
    {
        //if(Input.anyKeyDown)
        //{
        //    if (next < CutScene.Length)
        //        CutScene[next].enabled = true;
        //    else
        //        SceneManager.LoadScene(1,LoadSceneMode.Single);

        //    next++;
        //}
    }
    public void OnStart()
    {
        _Button[0].GetComponent<Image>().sprite = ChoiceBuottonImage[0];
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
