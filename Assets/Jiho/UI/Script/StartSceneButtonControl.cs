using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class StartSceneButtonControl : MonoBehaviour
{
    public void GameStartButton()
    {
        SceneManager.LoadSceneAsync("LastScene", LoadSceneMode.Single);
    }

    public void GameExitButton()
    {
//#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
