using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Magazine : MonoBehaviour
{
    public Image[] bulletCurrent;
    public Image[] bulletNum = new Image [20];
    public Image prebullet;
    public Aim aim_;
    Player_Stat stat_p;
    public Image ParentImage;
    int roatation;
    int bulletNumcheck;
    ItemManager manager_imte;
    List<Image> bulletList = new List <Image>();
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        bulletNumcheck = 0;
        manager_imte = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        stat_p = GetComponent<Player_Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Sprite temp = bulletCurrent[1].sprite;
            bulletCurrent[1].sprite = bulletCurrent[0].sprite;//3->4
            bulletCurrent[0].sprite = temp;//5=>3
            roatation++;
            if (roatation > 1)
                roatation = 0;
            stat_p.SetBulletState(roatation);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Sprite temp = bulletCurrent[0].sprite;
            bulletCurrent[0].sprite = bulletCurrent[1].sprite;
            bulletCurrent[1].sprite = temp;
            roatation--;
            if (roatation < 0)
                roatation = 1;
            stat_p.SetBulletState(roatation);
        }
    }
    public void Shooting()
    {
        rand = Random.Range(0, 4);
        if (stat_p.currentBulletNum > 0)
        {
            if (stat_p.getCurrentBulletState() == 0)
            {
                AkSoundEngine.PostEvent("Cookie_Fire", gameObject);
            }
            if (stat_p.getCurrentBulletState() == 1)
                {
                AkSoundEngine.PostEvent("Bubble_Fire", gameObject);
                ParentImage.fillAmount = stat_p.currentBulletNum / 60.0f;
                    stat_p.currentBulletNum--;
                    bulletNumcheck--;
                }
            if (stat_p.getCurrentBulletState() == 2)
                ParentImage.fillAmount = stat_p.currentBulletNum * 0.016f;

            Instance();
            stat_p.SetShoot();
        }
    }
    public void UIbulletNum(int currentBulletNum, int currentBullet)
    {

        switch (currentBullet)
        {
            case 0:
                ParentImage.fillAmount = 1;
                break;
            case 1:
                ParentImage.fillAmount = stat_p.currentBulletNum / 60.0f;
                break;
            case 2:
                ParentImage.fillAmount = stat_p.currentBulletNum * 0.016f;
                break;
            default:
                break;
        }
        //if (bulletList.Count != 0)
        //{
        //    for (int i = 0; i < bulletList.Count; i++)
        //    {
        //        if (bulletList[i] != null)
        //        {
        //            Destroy(bulletList[i].gameObject);
        //        }
        //    }
        //    bulletNumcheck = 0;
        //    bulletList.Clear();
        //}

        //for (; bulletNumcheck < stat_p.currentBulletNum; bulletNumcheck++)
        //    {
        //    if (bulletNumcheck == 0)
        //        bulletList.Add(Instantiate(prebullet, new Vector3(ParentImage.rectTransform.position.x + 1.0f, ParentImage.rectTransform.position.y, 0), new Quaternion(0, 0, 0, 0), ParentImage.transform.parent));
        //    else
        //        bulletList.Add(Instantiate(bulletList[bulletNumcheck - 1], new Vector3(bulletList[bulletNumcheck - 1].rectTransform.position.x + 5.0f, bulletList[bulletNumcheck - 1].rectTransform.position.y, 0), new Quaternion(0, 0, 0, 0), ParentImage.transform.parent));
        //    }
    }
    void Instance()
    {
     
        if (stat_p.currentBulletNum >= 0)
        {
            if (stat_p.getCurrentBulletState() == 0)
            {
                Instantiate(manager_imte.Bullet[0], transform.position, transform.rotation).transform.LookAt(aim_.temp.point);
            }
            if (stat_p.getCurrentBulletState() == 1)
                Instantiate(manager_imte.Bullet[1],transform.position, transform.rotation).transform.LookAt(aim_.temp.point);
        }
    }
    public Vector3 getHitPos()
    {
        return aim_.temp.point;
    }
}
