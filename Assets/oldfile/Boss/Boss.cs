using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public float Range;
    public GameObject target;
    public float FadeTime = 2f; 
    public Image fadeImg;
    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;


    //Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < Range)
        {
            OutStartFadeAnim();
        }
    }

    public void OutStartFadeAnim()

    {

        if (isPlaying == true) 

        {

            return;

        }

        start = 0.0f;

        end = 1.0f;

        StartCoroutine("fadeoutplay");  

    }

    IEnumerator fadeoutplay()
    {
        isPlaying = true;

        Color fadecolor = fadeImg.color;
        time = 0f;

        while (fadecolor.a < 1f)

        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;
        }

        isPlaying = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(3,LoadSceneMode.Single);
    }
}
