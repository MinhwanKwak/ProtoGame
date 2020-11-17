using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinata : MonoBehaviour
{
    public float Hp;
    public GameObject bbg_obj;
    public GameObject hp_obj;
    ItemManager i_managger;
    int hitNum = 0;
    private void Start()
    {
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie")
        {
            Debug.Log(i_managger.Damage[0]);
            Hp -= i_managger.Damage[0];
            ItemCreat();
        }
        if (col.tag == "Bubblegum"&&col.name!= "Bubblegum")
        {
            Hp -= i_managger.Damage[1];
            ItemCreat();
        }
        if (col.tag == "WhippingCream")
        {
            Hp -= i_managger.Damage[2];
        }
    }
    void ItemCreat()
    {
        GameObject obj = null;
        if (Hp <= 20 && Hp > 10)
        {
            hitNum += 1;
            obj = Instantiate(bbg_obj, transform.position, transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(hitNum);
        }
        if (Hp <= 10)
        {
            hitNum += 1;
            obj = Instantiate(bbg_obj, transform.position, transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(hitNum);
            hitNum += 1;
            obj = Instantiate(bbg_obj, transform.position, transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(hitNum);
          
        }
        if (Hp <= 0)
        {
            hitNum += 1;
            Debug.Log(hitNum);
            obj = Instantiate(bbg_obj, transform.position, transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(hitNum);
            hitNum += 1;
            Debug.Log(hitNum);
            obj = Instantiate(hp_obj, transform.position, hp_obj.transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(hitNum);
            obj.name = "Hp";
            Destroy(this.gameObject);
        }
    }
}
