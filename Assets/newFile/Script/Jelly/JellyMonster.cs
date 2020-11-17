using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMonster : MonoBehaviour
{
    public GameObject bullet;
    Transform[] bulletPos;
    public Transform posParent;
    public GameObject jelly_Shoot_Effect;
    //float time;
    //bool Up;
    //bool DownIdle;
    //float IdleTimer;

    public float Hp = 40.0f;
    //public float I_time = 2.0f;
    //public float Move_time = 1.0f;
    ItemManager i_managger;
    Animator anim;
    bool Bullet_Shoot;
    GameObject eff;
    // Start is called before the first frame update
    void Start()
    {
        bulletPos = posParent.GetComponentsInChildren<Transform>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        anim = gameObject.GetComponent<Animator>();
        Bullet_Shoot = false;
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!DownIdle)
        //{
        //    time += Time.deltaTime;
        //}
        //else if(DownIdle)
        //{
        //    IdleTimer += Time.deltaTime;
        //    if(IdleTimer>=2.0f)
        //    {
        //        DownIdle = false;
        //        IdleTimer = 0.0f;
        //    }
        //}
        //if (!Up&&!DownIdle)
        //{
        //    if(time>= Move_time)
        //    {
        //        Up = true;
        //        time = 0.0f;
        //        CreatBullet();
        //    }
        //    transform.Translate(Vector3.forward*Time.deltaTime);
        //}
        //if(Up)
        //{
        //    if (time >= Move_time)
        //    {
        //        Up = false;
        //        time = 0.0f;
        //        DownIdle = true;
        //    }
        //    transform.Translate(-Vector3.forward * Time.deltaTime);
        //}
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
           Destroy(this.gameObject);
        }
        if (Hp<=0.0f)
        {
            if (!anim.GetBool("Die"))
                AkSoundEngine.PostEvent("Slime_Dead", gameObject);
            anim.SetBool("Die",true);
            if (GetComponent<SphereCollider>()!=null)
            GetComponent<SphereCollider>().enabled = false;
        }
    }
    private void CreatBullet()
    {
        AkSoundEngine.PostEvent("Jelly_Fire", gameObject);
        eff = Instantiate(jelly_Shoot_Effect, new Vector3( transform.position.x, transform.position.y+1.5f, transform.position.z), transform.rotation);
        for (int i = 0;i<bulletPos.Length;i++)
        {
            Instantiate(bullet, bulletPos[i].position, bulletPos[i].rotation);
        }
    }
    public void effDel()
    {
        Destroy(eff);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie")
        {
            AkSoundEngine.PostEvent("Slime_Hit", gameObject);
            Hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            AkSoundEngine.PostEvent("Slime_Hit", gameObject);
            Hp -= i_managger.Damage[1];
        }
    }
}
