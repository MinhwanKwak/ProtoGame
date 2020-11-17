using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum Ant_State
{
    Idle,
    Run,
    Chase,
    Attack,
    Hit
}
public class Ant_Manager : MonoBehaviour
{
    public float hp = 15;
    public int Attack = 1;
    public Ant_State currentState;
    public Ant_State startState;
    public Camera Sight;
    public NavMeshAgent nv;
    public CapsuleCollider p_col;
    public Transform PlayerPos;
    public Vector3 prePlayerPos;

    public Animator anim;
    ItemManager i_managger;
    Dictionary<Ant_State, Ant_StateManager> states = new Dictionary<Ant_State, Ant_StateManager>();

    bool dead;
    float d_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        states.Add(Ant_State.Idle, GetComponent<Ant_Idle>());
        states.Add(Ant_State.Run, GetComponent<Ant_Run>());
        states.Add(Ant_State.Chase, GetComponent<Ant_Chase>());
        states.Add(Ant_State.Attack, GetComponent<Ant_Attack>());
        states.Add(Ant_State.Hit, GetComponent<Ant_Hit>());
        prePlayerPos = Vector3.zero;
        Sight = GetComponentInChildren<Camera>();
        nv = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        p_col = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SetState(Ant_State.Idle);
        dead = false;
    }
    private void Update()
    {
        if (dead)
        {
            d_time += Time.deltaTime;
            if (d_time >= 2.0f)
                Destroy(gameObject);
        }
    }
    public bool Detect(Camera sight, CapsuleCollider player)
    {
        if (player == null)
            return false;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, player.bounds);
    }

    public void SetState(Ant_State newState)
    {
        foreach (Ant_StateManager fsm in states.Values)
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
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    public void AttackCheck()
    {
        Player_Stat targetStat = p_col.GetComponent<Player_Stat>();
        targetStat.DamgaeSend(Attack);
        p_col.GetComponent<Player_Manager>().SetState(Player_State.Hit);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "weapon_Hammer")
        {
            SetState(Ant_State.Hit);
            hp -= PlayerPos.GetComponent<Player_Stat>().melee_Attack;
        }
        if (col.tag == "Cookie")
        {
            SetState(Ant_State.Hit);
            hp -= i_managger.Damage[0];
        }
        if (col.tag == "Bubblegum")
        {
            SetState(Ant_State.Hit);
            hp -= i_managger.Damage[1];
        }
        if (col.tag == "WhippingCream")
        {
            SetState(Ant_State.Hit);
            hp -= i_managger.Damage[2];
        }
    }
}
