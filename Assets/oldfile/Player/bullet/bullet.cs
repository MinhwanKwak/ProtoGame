using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
     float speed ;
     float distance;
     float damage ;
    Vector3 start_pos;
    public GameObject HitBubble;
    public GameObject HitBubble_;
    private void Start()
    {
        start_pos = transform.position;
      
    }
    public void SetBullet(float s,float d,float Damage)
    {
        speed = s;
        distance = d;
        damage = Damage;
    }
    // Update is called once per frame
    void Update()
    {
        if (distance <= Vector3.Distance(start_pos, transform.position))
        {
            Destroy(this.gameObject);
        }
        transform.Translate(new Vector3(0, 0, speed));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            if (other.tag == "Enemy")
            {
                Enemy enamy = other.GetComponent<Enemy>();
                enamy.TakeDamage(damage,this.transform.tag);
                Destroy(this.gameObject);
            }
        }
    }
}
