using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBullet : MonoBehaviour
{
    float Timer;
    public float aliveTime;
    public GameObject Damage_eff;
    private void Start()
    {
        transform.name = "JellyBullet";
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);
        if (Timer >= aliveTime)
            Destroy(transform.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Enemy_Bullet")
            return;
        Instantiate(Damage_eff, transform.position, transform.rotation);
        Destroy(transform.gameObject);
    }
}
