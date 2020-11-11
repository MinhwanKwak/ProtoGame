using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    public Image image;

    private Sequence sequence;

    private void Start()
    {
        
    }


    public void UpdatePositionFromWorldPosition(Vector3 worldPos)
    {
        //image.rectTransform.anchoredPosition = Camera.WorldToScreenPoint(worldPos);
    }

    public void SetHPUIFill()
    {
        float fillAmount = 1.0f;
        fillAmount -= 0.5f;


        //sequence = DOTween.Sequence();

        //sequence.Insert(0, image.DOFillAmount(fillAmount, 0.2f));
        image.fillAmount -= fillAmount;
    }
}
