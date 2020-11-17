using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Hp : MonoBehaviour
{
    public GameObject[] spawnHart;
    public Image spawnBullt;
    public GameObject bulletParent;

    public PlayerStat ps;
    public Image[] CookieIcon;

    Image[] bulletList;
    PlayerManager p_manager;
    private void Start()
    {
        p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        bulletList = bulletParent.gameObject.GetComponentsInChildren<Image>();
        foreach (GameObject item in spawnHart)
        {
            item.GetComponent<Image>().enabled = true;
        }
        foreach (Image item in bulletList)
        {
            item.GetComponent<Image>().enabled = false;
        }

    }

    private void Update()
    {
        int currHp = ps.currentHp;
        int currBullet = p_manager.bulletCount;
        int currMaxBullet = p_manager.bulletMaxCount;
        for (int i = 0; i < ps.max_hp; ++i)
        {
            if (i < currHp)
                spawnHart[i].GetComponent<Image>().enabled = true;
            else
                spawnHart[i].GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < currMaxBullet; ++i)
        {
            if (i < currBullet)
            {
                bulletList[i].GetComponent<Image>().enabled = true;
            }
            else
                bulletList[i].GetComponent<Image>().enabled = false;
        }

        foreach (Image i_cookie in CookieIcon)
        {
                if (i_cookie.transform.tag == p_manager.GetComponent<PlayerEat>().getCookie() && currBullet > 0)
                    i_cookie.enabled = true;
                else
                    i_cookie.enabled = false;
        }
    }
}
