using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    public Image image;


    private void Start()
    {
        
    }


    public void UpdatePositionFromWorldPosition(Vector3 worldPos)
    {
        //image.rectTransform.anchoredPosition = Camera.WorldToScreenPoint(worldPos);
    }
}
