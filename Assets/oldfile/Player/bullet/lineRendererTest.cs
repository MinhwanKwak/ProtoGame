using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRendererTest : MonoBehaviour
{
    float speed;
    float distance;
    float damage;
    Transform platyer_pos;
    Vector3 start_pos;
    RaycastHit temp;
    private LineRenderer lineRenderer;

    public GameObject Ray;
    public GameObject ScaleD;
    public GameObject Cream_effect;
    public GameObject Cream_effect_;
    CapsuleCollider c_collider;
    public void SetBullet(float s, float d, float Damage,RaycastHit hit_)
    {
        speed = s;
        distance = d;
        damage = Damage;
        temp = hit_;
    }
    //private void Start()
    //{
    //    platyer_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    //}
    //private void Update()
    //{
    //    if (Physics.Raycast(Camera.main.transform.position, -platyer_pos.transform.forward, out temp, 100.0f)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
    //    {
    //        Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
    //    }
    //    transform.LookAt(temp.point);

    //    if (transform.localScale.z <= 5)
    //    {
    //        transform.localScale += new Vector3(0, 0, 1.0f);

    //        //if (c_collider == null)
    //        //{
    //        //    transform.gameObject.AddComponent<CapsuleCollider>();
    //        //    c_collider = transform.GetComponent<CapsuleCollider>();
    //        //    c_collider.isTrigger = true;
    //        //    c_collider.radius = 0.1f;
    //        //    c_collider.direction = 2;
    //        //    c_collider.height += 5 / 2;
    //        //}
    //    }
    //    else
    //    {
        
    //    }
    //}


    // Use this for initialization
    void Start()
    {
        start_pos = transform.position;
        platyer_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //라인렌더러 설정
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(1.0f, distance / 100);
    }
    private void Update()
    {

        lineRenderer.SetPosition(0, platyer_pos.position);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out temp, Mathf.Infinity)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
        {
            Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
        }
        lineRenderer.SetPosition(1, temp.point);
        transform.position = lineRenderer.transform.position;
        if (transform.localScale.z <= distance)
            transform.localScale += new Vector3(0, 0, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            if (other.tag == "Enemy")
            {
                Cream_effect_ = Instantiate(Cream_effect, other.transform.position, transform.rotation, transform);
                Enemy enamy = other.GetComponent<Enemy>();
                enamy.TakeDamage(damage,other.tag);
             //   Destroy(this.gameObject);
            }
        }
    }
}

