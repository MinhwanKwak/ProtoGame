using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum HeartZombie_State
{
    Idle,
    Attack,
    Hit,
    move
}

public class HeartZombie_Manager : MonoBehaviour
{

    public float hp = 15;
    public int Attack = 1;
    public HeartZombie_State currentState;
    public HeartZombie_State startState;
    public Camera Sight;
    public NavMeshAgent nv;
    public CapsuleCollider p_col;
    public Transform PlayerPos;
    public Vector3 prePlayerPos;
    public GameObject HPObject;
    public Animator anim;
    ItemManager i_managger;
    Dictionary<HeartZombie_State, HeartZombie_StateManager> states = new Dictionary<HeartZombie_State, HeartZombie_StateManager>();

    bool dead;
    float d_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        states.Add(HeartZombie_State.Idle, GetComponent<HeartZombie_Idle>());
        states.Add(HeartZombie_State.Attack, GetComponent<HeartZombie_Attack>());
        states.Add(HeartZombie_State.Hit, GetComponent<HeartZombie_Hit>());
        states.Add(HeartZombie_State.move, GetComponent<HeartZombie_Move>());

        prePlayerPos = Vector3.zero;
        Sight = GetComponentInChildren<Camera>();
        nv = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SetState(HeartZombie_State.move);
        dead = false;
        AkSoundEngine.RegisterGameObj(gameObject);
    }
    private void Update()
    {
        if (hp <= 0 && !dead)
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
    public bool Detect(Camera sight, CapsuleCollider player)
    {
        if (player == null)
            return false;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, player.bounds);
    }

    public void SetState(HeartZombie_State newState)
    {
        foreach (HeartZombie_StateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("SetAnim", (int)currentState);
    }
    public void Dead()
    {
        if (!dead)
        {
            anim.SetTrigger("Dead");
            dead = true;
            nv.isStopped = true;
            GameObject obj;
            obj = Instantiate(HPObject, transform.position, transform.rotation);
            obj.GetComponent<ItemGround>().SetPos(-1);
            obj.transform.position = transform.position;
            obj.name = "Hp";
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
  
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie")
        {
            AkSoundEngine.PostEvent("Zombie_Hit", gameObject);
            SetState(HeartZombie_State.Hit);
            hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            AkSoundEngine.PostEvent("Zombie_Hit", gameObject);
            SetState(HeartZombie_State.Hit);
            hp -= i_managger.Damage[1];
        }
        if (col.tag == "WhippingCream")
        {
            SetState(HeartZombie_State.Hit);
            hp -= i_managger.Damage[2];
        }
    }
}