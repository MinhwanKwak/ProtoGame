using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour
{
    public GameObject[] lifeBar;
    public GameObject[] halfBar;

    public PlayerControll playerControl;

    private void Update()
    {
        //SetHPBar();
    }

    //public void SetHPBar()
    //{
    //    switch(playerControl.playerHP)
    //    {

    //        case 9:
    //            lifeBar[4].SetActive(false);
    //            halfBar[4].SetActive(true);
    //            break;
    //        case 8:
    //            halfBar[4].SetActive(false);
    //            break;
    //        case 7:
    //            lifeBar[3].SetActive(false);
    //            halfBar[3].SetActive(true);
    //            break;
    //        case 6:
    //            halfBar[3].SetActive(false);
    //            break;
    //        case 5:
    //            lifeBar[2].SetActive(false);
    //            halfBar[2].SetActive(true);
    //            break;
    //        case 4:
    //            halfBar[2].SetActive(false);
    //            break;
    //        case 3:
    //            lifeBar[1].SetActive(false);
    //            halfBar[1].SetActive(true);
    //            break;
    //        case 2:
    //            halfBar[1].SetActive(false);
    //            break;
    //        case 1:
    //            lifeBar[0].SetActive(false);
    //            halfBar[0].SetActive(true);
    //            break;
    //        case 0:
    //            halfBar[0].SetActive(false);
    //            break;
    //    }
    //}
}
