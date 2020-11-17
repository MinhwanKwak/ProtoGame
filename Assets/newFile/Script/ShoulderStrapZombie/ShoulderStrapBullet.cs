using UnityEngine;
using System.Collections;

public class ShoulderStrapBullet : MonoBehaviour
{
    private float tx;
    private float ty;
    private float tz;

    private float v;
    public float g = 9.8f;

    private float elapsed_time;
    float max_height;

    private float t;
    private Vector3 start_pos;
    private Vector3 end_pos;

    private float dat;  //도착점 도달 시간 
    public int Attack;
    float Timer;
    public float aliveTime;
    Vector3 playerPos;

    public GameObject Damage_effect;
    GameObject E_obj;
    private void Start()
    {
        transform.name = "ShoulderStrapBullet";
        //playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        //playerPos = new Vector3(playerPos.x,0f, playerPos.z);
        //Shoot(new Vector3 (transform.position.x, transform.position.y+0.5f, transform.position.z), playerPos,9.8f, 5);
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);
        // transform.position = Vector3.MoveTowards(transform.position, PlayerPos.position,Time.deltaTime*20.0f);
        if (Timer >= aliveTime)
        {
            Destroy(transform.gameObject);
        }
    }
    public void Shoot(Vector3 startPos, Vector3 endPos, float g, float max_height)
    {
        start_pos = startPos;
        end_pos = endPos;
        this.g = g;
        this.max_height = max_height;
        float dh = endPos.y - startPos.y;
        float mh = max_height - startPos.y;
        if (endPos.y < startPos.y)
            mh *= -1;
        ty = Mathf.Sqrt(2 * this.g * mh);
        float a = this.g;
        float b = -2 * ty;
        float c = 2 * dh;

        dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        tx = -(startPos.x - endPos.x) / dat;
        tz = -(startPos.z - endPos.z) / dat;

        this.elapsed_time = 0;
        StartCoroutine("ShootImpl");
    }

    IEnumerator ShootImpl()
    {
        while (true)
        {
            this.elapsed_time += Time.deltaTime;

            float tx = start_pos.x + this.tx * elapsed_time;
            float ty = start_pos.y + this.ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
            float tz = start_pos.z + this.tz * elapsed_time;

            Vector3 tpos = new Vector3(tx, ty, tz);

            transform.LookAt(tpos);
            transform.position = tpos;

            if (this.elapsed_time >= this.dat)
                break;
            yield return null;
        }
    }
    public void setAttack(int attack)
    {
        Attack = attack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Enemy_Bullet")
            return;
        E_obj=Instantiate(Damage_effect, transform.position, transform.rotation);
        Destroy(transform.gameObject);
    }
}



