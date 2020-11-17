using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool Keydown = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Keydown)
            {
                Keydown = true;
                transform.GetComponent<Canvas>().enabled = Keydown;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0.0f;
            }
            else
            {
                Keydown = false;
                transform.GetComponent<Canvas>().enabled = Keydown;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1.0f;
            }
        }
    }
    public void OnrestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void OnTitleButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
