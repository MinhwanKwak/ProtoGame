using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_2State
{
    Idle,
    Run,
    Chase,
    Attack,
    Hit
}
public class Enemy_2Manager : MonoBehaviour
{
    public Enemy_2State currentState;
    public Enemy_2State startState;
    public CharacterController cc;
    public Transform target;
    public CharacterController playerCC;
    public Camera sight;
    public Animator anim;
    Enemy enemy_stat;
    public float gravity = 20.0f;
    Dictionary<Enemy_2State, Enenmy_2StateManager> states = new Dictionary<Enemy_2State, Enenmy_2StateManager>();

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        sight = GetComponentInChildren<Camera>();
        playerCC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy_stat = GetComponent<Enemy>();
        states.Add(Enemy_2State.Idle, GetComponent<Enemy_2Idle>());
        states.Add(Enemy_2State.Run, GetComponent<Enemy_2Run>());
        states.Add(Enemy_2State.Chase, GetComponent<Enemy_2Chase>());
        states.Add(Enemy_2State.Attack, GetComponent<Enemy_2Attack>());
        states.Add(Enemy_2State.Hit, GetComponent<Enemy_2Hit>());
    }
    // Use this for initialization
    void Start()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void AttackCheck()
    {
        PlayerStat targetStat = playerCC.GetComponent<PlayerStat>();
        targetStat.PlayerTakeDamage(enemy_stat.e_attackDamage);
    }
    public void SetState(Enemy_2State newState)
    {
        foreach (Enenmy_2StateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("SetAnim", (int)currentState);
    }
}
