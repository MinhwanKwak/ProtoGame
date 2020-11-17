using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReStart : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Start is called before the first frame update
    public void OnrestartButton()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
    public void OnTitleButton()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
