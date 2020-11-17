using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float Hp = 15;
    public int EnemyBodyDamage = 1;
    public int e_attackDamage = 1;

    public GameObject HitBubble;
    public GameObject HitBubble_;

    public GameObject HitCookie;
    public GameObject HitCookie_;

    public GameObject HitSmoke;
    public GameObject HitSmoke_;


    Enemy_2Manager enemy2_m;
    ItemManager i_managger;

    // Use this for initialization
    void Start () {
        enemy2_m = transform.GetComponent<Enemy_2Manager>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Hp <= 0)
        {
            HitSmoke_ = Instantiate(HitSmoke, transform.position, HitSmoke.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie")
        {
            Hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            Hp -= i_managger.Damage[1];
        }
        if (col.tag == "WhippingCream")
        {
            Hp -= i_managger.Damage[2];
        }
    }
    public void TakeDamage(float damage,string bullet_tag)
    {
        Hp -= damage;
        Camera.main.GetComponent<CamerRig>().ShakeCamera(1f);
        Vector3 Dir = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().forward;
        Debug.Log(bullet_tag);
        if (bullet_tag == "Bubblegum")
            HitBubble_ = Instantiate(HitBubble, new Vector3(transform.position.x, transform.position.y +1.5f, transform.position.z)-Dir, HitBubble.transform.rotation,transform);
        if (bullet_tag == "Cookie")
            HitCookie_ = Instantiate(HitCookie, transform.position, HitCookie.transform.rotation, transform);
        if (enemy2_m)
            enemy2_m.SetState(Enemy_2State.Hit);
        if (Hp <= 0)
        {
            HitSmoke_ = Instantiate(HitSmoke, transform.position, HitSmoke.transform.rotation);
            Destroy(this.gameObject);
        }
    }
   
}
