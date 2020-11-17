using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class ShoulderStrapZombie : MonoBehaviour
{
    public float hp = 15;
    public int Attack = 1;
    public Camera Sight;
    public NavMeshAgent nv;
    public CapsuleCollider p_col;
    public Transform PlayerPos;
    public Vector3 prePlayerPos;
    public Animator anim;
    ItemManager i_managger;

    bool dead;
    float d_time = 0;

    Vector3 move;
    float time;
    float Move_Time;
    float Turn;
    public float TurnTime;
    public float Attack_Time;

    public float max_h;
    public GameObject bullet;
    GameObject InstageBullet;
    Transform currentPlayerPos;
    public DirAxis dirAxis;
    Vector3 moveDir;
    public float IntervalSpeed;
    float IntervalTimer;
    bool[] intervalBullet = new bool [3];
    bool shootIn_d;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        prePlayerPos = Vector3.zero;
        Sight = GetComponentInChildren<Camera>();
        nv = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        dead = false;

        if (dirAxis == DirAxis.xAxis)
            moveDir = Vector3.right;
        if (dirAxis == DirAxis.yAxis)
            moveDir = Vector3.forward;
        Turn = 1;
        transform.Rotate(0, 180, 0, Space.Self);
        AkSoundEngine.RegisterGameObj(gameObject);
    }
    private void Update()
    {
        //IntervalTimer += Time.deltaTime;
        Move_Time += Time.deltaTime;
        move = transform.position + (moveDir*Turn);
        nv.SetDestination(move);
        if (time >= TurnTime)
        {
        //    transform.Rotate(0, 90, 0, Space.Self);
            time = 0.0f;
            Turn *= -1;
            nv.ResetPath();
        }
        //if (IntervalTimer >= IntervalSpeed && !intervalBullet[0])
        //{
        //    AnimBulletCall();
        //    intervalBullet[0] = true;
        //}
        //else if (IntervalTimer >= IntervalSpeed + 1.0f&&!intervalBullet[1])
        //{
        //    AnimBulletCall();
        //    intervalBullet[1] = true;
        //}
        //else if(IntervalTimer >= IntervalSpeed + 2.0f&& !intervalBullet[2])
        //{
        //    IntervalTimer = 0.0f;
        //    AnimBulletCall();
        //    intervalBullet[0] = false;
        //    intervalBullet[1] = false;
        //}
        if (Move_Time <= Attack_Time)
        {
            time += Time.deltaTime;
            nv.isStopped = false;
            shootIn_d = false;
            anim.SetInteger("SetAnim", 0);
        }
        else
        {
            anim.SetInteger("SetAnim", 1);
            shootIn_d = true;
            nv.isStopped = true;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                Move_Time = 0.0f;
            }
        }
        if (hp <= 0&&!dead)
        {
            AkSoundEngine.PostEvent("Zombie_Dead", gameObject);
            Dead();
        }

        if (dead)
        {
            d_time += Time.deltaTime;
            if (d_time >= 2.0f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void AnimBulletCall()
    {
        if (shootIn_d)
        {
            InstageBullet = Instantiate(bullet, transform.position, bullet.transform.rotation, null);
            InstageBullet.name = "ShoulderStrapBullet";
            InstageBullet.transform.LookAt(PlayerPos);
            Vector3 yZeroPlayer = new Vector3(PlayerPos.position.x, 0, PlayerPos.position.z);
            InstageBullet.GetComponent<ShoulderStrapBullet>().Shoot(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), yZeroPlayer, 9.8f, max_h);
            InstageBullet.GetComponent<ShoulderStrapBullet>().setAttack(Attack);
        }
    }
    public bool Detect(Camera sight, CapsuleCollider player)
    {
        if (player == null)
            return false;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, player.bounds);
    }

    public void Dead()
    {
        if (!dead)
        {
            anim.SetTrigger("Dead");
            dead = true;
            nv.isStopped = true;
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie")
        {
            AkSoundEngine.PostEvent("Zombie_Hit", gameObject);
            hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            AkSoundEngine.PostEvent("Zombie_Hit", gameObject);
            hp -= i_managger.Damage[1];
        }
        if (col.tag == "WhippingCream")
        {
            hp -= i_managger.Damage[2];
        }
    }
}
