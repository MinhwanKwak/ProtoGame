using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHouse : MonoBehaviour
{
    public Collider[] colliders;
    public Transform[] objects;
    public GameObject[] desserts;
    public Transform Ground;

    public GameObject HitHouse;
    public GameObject HitHouse_;

    public bool Regening = false;
    public int[] CookieCount;

    public float Mass = 1;                                  // Rigidbody Mass 제어
    public float Drag = 2;                                   // Rigidbody Drag 제

    public float hp = 10;
    ItemManager i_managger;
    void Awake()
    {
        objects = gameObject.GetComponentsInChildren<Transform>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        foreach (Transform item in objects)
        {
            item.gameObject.AddComponent<BoxCollider>();
            item.gameObject.AddComponent<Rigidbody>();
            item.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)126;
            item.GetComponent<Rigidbody>().mass = Mass;
            item.GetComponent<Rigidbody>().drag = Drag;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
        objects[0].gameObject.GetComponent<BoxCollider>().size = new Vector3(7, 7, 7);
        objects[0].gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, -3.0f);
        objects[0].gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    private void Update()
    {
        if (hp <= 0&& !Regening)
            BreakHome();
    }
    void OnTriggerStay(Collider col)
    {
        if (col.transform.root.tag == "Player"&& col.transform.root.GetComponent<Player_DestoryHouse>().GetDestroyAnim())
        {
            hp -= hp;
        }
        if (col.tag == "Cookie")
        {
            hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            hp -= i_managger.Damage[1];
        }
        if (col.tag == "WhippingCream")
        {
            hp -= i_managger.Damage[2];
        }
    }
    void BreakHome()
    {
        Regening = true;
        colliders = gameObject.GetComponentsInChildren<Collider>();
        Destroy(objects[0].gameObject.GetComponent<Rigidbody>());
        HitHouse_ = Instantiate(HitHouse, new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y + 1.2f, GameObject.FindGameObjectWithTag("Player").transform.position.z), HitHouse.transform.rotation, transform);
        for (int j = 0; j < desserts.Length; j++)
        {
            for (int i = 0; i < CookieCount[j]; i++)
            {
                float rndy = Random.Range(-2f, 2f);
                float rndx = Random.Range(-2f, 2f);
                if (j == 0)
                    Instantiate(desserts[j], new Vector3(transform.position.x + rndx, Ground.position.y , transform.position.z + rndy), desserts[j].transform.rotation);
                if (j == 1)
                    Instantiate(desserts[j], new Vector3(transform.position.x + rndx, Ground.position.y , transform.position.z + rndy), desserts[j].transform.rotation);
                if (j == 2)
                    Instantiate(desserts[j], new Vector3(transform.position.x + rndx, Ground.position.y , transform.position.z + rndy), desserts[j].transform.rotation);
            }
            if (j >= 5)
                Regening = false;
        }

        foreach (Collider item in colliders)
        {
            if (item.tag == "HouseWall")
            {
                Destroy(item.gameObject);
            }
            if (item.GetComponent<Rigidbody>() != null)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            }
            Destroy(gameObject, 3);
        }
    }
}