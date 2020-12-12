using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ResultUI : UIBase
{
    public void ExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit() // 
#endif
    }
    

    public void RestartClick()
    {
        SceneManager.LoadScene("LastScene");
    }

    public void mainClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
