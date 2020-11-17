using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public enum RushingZombie_State
{
    Move,
    Attack,
    Hit
}

public class RushingZombie_Manager : MonoBehaviour
{
    
    public float hp = 15;
    public int Attack = 1;
    public RushingZombie_State currentState;
    public RushingZombie_State startState;
    public Camera Sight;
    public NavMeshAgent nv;
    public CapsuleCollider p_col;
    public Transform PlayerPos;
    public Vector3 prePlayerPos;

    public Animator anim;
    ItemManager i_managger;
    Dictionary<RushingZombie_State, RushingZombie_StateManager> states = new Dictionary<RushingZombie_State, RushingZombie_StateManager>();

    bool dead;
    float d_time = 0;

    public GameObject DeadEffect;
    public float Boom_Effect_D;
    bool col_player;
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponentInChildren<Animator>();
        states.Add(RushingZombie_State.Move, GetComponent<RushingZombie_Move>());
        states.Add(RushingZombie_State.Attack, GetComponent<RushingZombie_Attack>());
        prePlayerPos = Vector3.zero;
        Sight = GetComponentInChildren<Camera>();
        nv = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        i_managger = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SetState(RushingZombie_State.Move);
        dead = false;
        AkSoundEngine.RegisterGameObj(gameObject);
    }
    private void Update()
    {
        if(dead)
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
  
    public void SetState(RushingZombie_State newState)
    {
        foreach (RushingZombie_StateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("SetAnim", (int)currentState);
    }
    void Boom()
    {
        Vector3 destination = PlayerPos.position;
        Vector3 diff = destination - transform.position;
        Instantiate(DeadEffect, transform.position, transform.rotation, transform);

        if (diff.sqrMagnitude <= Boom_Effect_D * Boom_Effect_D)
        {
            if (!col_player)
                PlayerPos.GetComponent<Player_Stat>().DamgaeSend(1);
        }
    }
    public void Dead()
    {
        if (!dead)
        {
            anim.SetTrigger("Dead");
            Boom();
            dead = true;
            nv.isStopped = true;
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            col_player = true;
            hp = 0;
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
