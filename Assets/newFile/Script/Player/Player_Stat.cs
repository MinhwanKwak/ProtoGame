using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    enum bullet_State
    {
        CookieBullet,
        BubbleBullet,
        StrawberryBullet,
        Fork
    }
    bullet_State currentBullet;
    public float melee_Attack;//근거리 공격
    public float max_Hp;
    public float current_Hp;
    public float shoot_Attack;
    float shoot_Attack_Speed;
    public int []bulletNum = new int [3];
    public int currentBulletNum;
    public GameObject hp;

    public Text Ui_bulletNum;
    public Image[] hpIcon;
    ItemManager manager_imte;
    Animator anim;
    public GameObject[] currentWeapon;

    public GameObject GameOver;
    private void Awake()
    {
        hpIcon = hp.GetComponentsInChildren<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        manager_imte = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        anim = GetComponentInChildren<Animator>();
        current_Hp = max_Hp;
        SetBulletState((int)bullet_State.CookieBullet);
        Ui_bulletNum.text = bulletNum[(int)currentBullet].ToString();
    }
    private void Update()
    {
        if (current_Hp <= 0)
        {
           for(int i = 0;i<5;i++)
            hpIcon[i].enabled = false;
            gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
        //if ((int)currentBullet != 3)
        //    Ui_bulletNum.text = bulletNum[(int)currentBullet].ToString();
    }
    public void SetBulletState(int setBullet)
    {
        currentBullet = (bullet_State)setBullet;
        currentWeaponfunc((int)currentBullet);
        switch (currentBullet)
        {
            case bullet_State.CookieBullet:
                shoot_Attack = 5;
                currentBulletNum = bulletNum[(int)bullet_State.CookieBullet];
                break;
            case bullet_State.BubbleBullet:
                shoot_Attack = 7;
                currentBulletNum = bulletNum[(int)bullet_State.BubbleBullet];
                break;
            case bullet_State.StrawberryBullet:
                currentBulletNum = bulletNum[(int)bullet_State.StrawberryBullet];
                shoot_Attack = 10;
                break;
            default:
                break;
        }
        GetComponent<Player_Magazine>().UIbulletNum(currentBulletNum, (int)currentBullet);

    }
    public void SetShoot()
    {
        switch (currentBullet)
        {
            case bullet_State.CookieBullet:
                bulletNum[(int)bullet_State.CookieBullet] = currentBulletNum;
                break;
            case bullet_State.BubbleBullet:
               bulletNum[(int)bullet_State.BubbleBullet] = currentBulletNum; ;
                break;
            case bullet_State.StrawberryBullet:
                bulletNum[(int)bullet_State.StrawberryBullet]=currentBulletNum ;
                break;
            default:
                break;
        }
        Ui_bulletNum.text = bulletNum[(int)currentBullet].ToString();
    }
    public int getCurrentBulletState()
    {
        return (int)currentBullet;
    }
    public void SetBulletNum(string name)
    {
        if (name == "Cookie" && bulletNum[(int)bullet_State.CookieBullet]< manager_imte.BulletcountMax[0])
            bulletNum[(int)bullet_State.CookieBullet] += manager_imte.Bulletcount[0];
        if (name == "Bubblegum" && bulletNum[(int)bullet_State.BubbleBullet] < manager_imte.BulletcountMax[1])
            bulletNum[(int)bullet_State.BubbleBullet] += manager_imte.Bulletcount[1];
        if (name == "WhippingCream" && bulletNum[(int)bullet_State.StrawberryBullet] < manager_imte.BulletcountMax[2])
            bulletNum[(int)bullet_State.StrawberryBullet] += manager_imte.Bulletcount[2];
    }
    public void currentWeaponfunc(int newBulletState)
    {
        foreach (GameObject wes in currentWeapon)
        {
            wes.SetActive(false);
        }
        currentBullet = (bullet_State)newBulletState;
        currentWeapon[(int)newBulletState].SetActive(true);
        anim.SetInteger("WeaponState", newBulletState);
        Ui_bulletNum.text = bulletNum[(int)currentBullet].ToString();
    }
    public void DamgaeSend(int damage)
    {
        current_Hp -= damage;
        hpIcon[(int)current_Hp].enabled = false;
        //hp.fillAmount = current_Hp / max_Hp;
        
    }
    public void EatHpObject()
    {
        hpIcon[(int)current_Hp].enabled = true;
        current_Hp += 1;
    }
}
