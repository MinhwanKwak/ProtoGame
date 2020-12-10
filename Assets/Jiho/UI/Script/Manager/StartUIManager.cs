using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    public static StartUIManager Instance;

    Vector2 mousePosition;

    public Button StartGameButton;
    IPointerDownHandler pointerdown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePosition = Input.mousePosition;

        //if (Input.GetMouseButtonDown(0))
        //{

        //}
    }
}
