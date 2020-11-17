using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ : MonoBehaviour
{
    enum bullet_effect
    {
        shoot,
        smog,
        bullet,
        collision
    }
    ItemManager manager_imte;
    public Transform hand;
    float DamageB;
    float aliveB;
    float speedBulletB;

    float Timer;
    public GameObject []ps;

    Vector3 hit_pos;

    string SoundName;
    bool SoundPlay;
    bool CollTime;
    float collTimer;
    private void Start()
    {
        manager_imte = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        hand = GameObject.Find("EffectPos").GetComponent<Transform>();
        hit_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Magazine>().getHitPos();
        AkSoundEngine.RegisterGameObj(gameObject);
        SoundPlay = false;
        if (transform.tag == "Cookie")
        {
            ps[(int)bullet_effect.shoot].transform.position = hand.position;
            ps[(int)bullet_effect.bullet].transform.position = hand.position;

            ps[(int)bullet_effect.shoot].SetActive(true);
            DamageB = manager_imte.Damage[0];
            aliveB = manager_imte.alive[0];
            speedBulletB = manager_imte.speedBullet[0];
            SoundName = "Cookie_Collision";
        }

        if (transform.tag == "Bubblegum")
        {
            ps[(int)bullet_effect.shoot].transform.position = new Vector3(hand.position.x, hand.position.y, hand.position.z);
            ps[(int)bullet_effect.bullet].transform.position = new Vector3(hand.position.x, hand.position.y, hand.position.z);

            ps[(int)bullet_effect.shoot].SetActive(true);
            DamageB = manager_imte.Damage[1];
            aliveB = manager_imte.alive[1];
            speedBulletB = manager_imte.speedBullet[1];
            SoundName = "Bubble_Collision";
        }
        if (transform.tag == "WhippingCream")
        {
            //ps[(int)bullet_effect.shoot].transform.position = hand.position;
            //= ps[(int)bullet_effect.bullet].transform.position
            ps[(int)bullet_effect.shoot].SetActive(true);
            DamageB = manager_imte.Damage[2];
            aliveB = manager_imte.alive[2];
            speedBulletB = manager_imte.speedBullet[2];
        }
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        if (CollTime)
            collTimer += Time.deltaTime;
        if (collTimer >= 0.1f && ps[(int)bullet_effect.collision]!=null)
            ps[(int)bullet_effect.collision].GetComponent<SphereCollider>().enabled = false;

        if (ps[(int)bullet_effect.smog] != null)
            ps[(int)bullet_effect.smog].SetActive(true);

        if (Timer >= aliveB)
        {
            if (ps[(int)bullet_effect.collision] != null)
            {
                ps[(int)bullet_effect.collision].SetActive(true);
            }
            if (ps[(int)bullet_effect.bullet] != null)
            {
                if (!SoundPlay)
                    AkSoundEngine.PostEvent(SoundName, gameObject);
                Destroy(ps[(int)bullet_effect.bullet].transform.gameObject);
            }
        }
        if (Timer >= aliveB + 1.0f && ps[(int)bullet_effect.bullet] == null)
        {
            Destroy(transform.gameObject);
        }
        if (ps[(int)bullet_effect.bullet] != null)
        {
            ps[(int)bullet_effect.collision].transform.position = ps[(int)bullet_effect.bullet].transform.position;
            //hit_pos

            ps[(int)bullet_effect.bullet].transform.position = Vector3.MoveTowards(ps[(int)bullet_effect.bullet].transform.position, hit_pos,Time.deltaTime*speedBulletB);
                //new Vector3(Mathf.Lerp(ps[(int)bullet_effect.bullet].transform.position.x, hit_pos.x,speedBulletB),
                //Mathf.Lerp(ps[(int)bullet_effect.bullet].transform.position.y, hit_pos.y,speedBulletB),
                //Mathf.Lerp(ps[(int)bullet_effect.bullet].transform.position.z, hit_pos.z,speedBulletB));
            //  ps[(int)bullet_effect.bullet].transform.Translate(new Vector3(0, 0, speedBulletB));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            SoundPlay = true;
            AkSoundEngine.PostEvent(SoundName, gameObject);
            if (ps[(int)bullet_effect.bullet] != null)
            {
                ps[(int)bullet_effect.collision].transform.position = ps[(int)bullet_effect.bullet].transform.position;
                Destroy(ps[(int)bullet_effect.bullet].transform.gameObject);
            }
            ps[(int)bullet_effect.collision].SetActive(true);
            if (transform.tag == "Bubblegum")
            {
                CollTime = true;
            }
        }
    }
}
