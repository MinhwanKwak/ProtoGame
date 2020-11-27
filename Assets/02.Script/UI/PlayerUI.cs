using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : UIBase
{
    public GameObject[] ArmorUI;
    public GameObject[] AttackBuffUI;
    public GameObject[] HpUI;
    

    private bool isAromor = true;

    

    public void Damage()
    {
        if (isAromor)
        {
            UpdateDisplayUI();
        }

    }


    public void UpdateDisplayUI()
    {

    }


}
