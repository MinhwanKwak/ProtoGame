using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SettingCanvas settingCanvas;
    Color color;

    void Start()
    {
        color.a = 255;
        color = new Color(255, 255, 255);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingCanvas.BackPanel.activeSelf)
            {
                settingCanvas.BackPanel.SetActive(true);
            }
            else if (settingCanvas.BackPanel.activeSelf)
            {
                settingCanvas.SoundUIPanel.SetActive(false);
                settingCanvas.OptionUIPanel.SetActive(false);
                settingCanvas.Panel.SetActive(true);
                settingCanvas.BackPanel.SetActive(false);

                settingCanvas.ContinueButton.image.color = color;
                settingCanvas.EndGameButton.image.color = color;
                settingCanvas.catHead.transform.position = settingCanvas.catHeadOriginPosition;
                settingCanvas.catChin.transform.position = settingCanvas.catChinOriginPosition;

            }
        }
        
      
    }
}
