using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Player_State
{
    BackWalk=-1,
    Idle,
    FrontWalk,
    JumpDown,
    Shooting,
    Eat,
    DestoryHouse,
    Hit
}

public class Player_Manager : MonoBehaviour
{
    public Player_State currentState;
    public Player_State startState;
    public Animator anim;
    public AnimatorOverrideController[] AOC;
    public float v;
    public float h;
    public Rigidbody rd;

    public Player_Stat stat_Player;
    public Dictionary<Player_State, Player_StateManager> states = new Dictionary<Player_State, Player_StateManager>();
    public bool eating;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat_Player = GetComponentInChildren<Player_Stat>(); 

        states.Add(Player_State.Idle, GetComponent<Player_Idle>());
        states.Add(Player_State.FrontWalk, GetComponent<Player_FrontWalk>());
        states.Add(Player_State.BackWalk, GetComponent<Player_Backwalk>());
        states.Add(Player_State.JumpDown, GetComponent<Player_Jump>());
        states.Add(Player_State.Shooting, GetComponent<Player_Shooting>());
        states.Add(Player_State.Eat, GetComponent<Player_Eat>());
        states.Add(Player_State.DestoryHouse, GetComponent<Player_DestoryHouse>());
        states.Add(Player_State.Hit, GetComponent<Player_Hit>());
        eating = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        SetState(startState);
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stat_Player.getCurrentBulletState())
        {
            case 0:
                anim.runtimeAnimatorController = AOC[0];
                break;
            case 1:
                anim.runtimeAnimatorController = AOC[1];
                break;
            case 2:
                anim.runtimeAnimatorController = AOC[2];
                break;
        }
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        if (v == 1 && currentState == Player_State.Idle)
            SetState(Player_State.FrontWalk);
        if (v == -1 && currentState == Player_State.Idle)
            SetState(Player_State.BackWalk);
        if (v == 0 && currentState == Player_State.Idle)
        {
            if (h != 0)
                SetState(Player_State.FrontWalk);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == Player_State.Idle || currentState == Player_State.FrontWalk || currentState == Player_State.BackWalk || currentState == Player_State.Shooting)
            {
                if (currentState == Player_State.Shooting)
                {
                    anim.SetInteger("SetAnim",2);
                }
                else
                {
                    SetState(Player_State.JumpDown);
                }
            }
        }
       
        if (Input.GetMouseButtonDown(0)&& stat_Player.getCurrentBulletState()!=3&&currentState!=Player_State.Hit)
        {
            SetState(Player_State.Shooting);
        }
        if (Input.GetMouseButtonDown(1) && currentState != Player_State.DestoryHouse)
        {
            SetState(Player_State.DestoryHouse);
        }

    }
    public void SetState(Player_State newState)
    {
        foreach (Player_StateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("SetAnim", (int)currentState);
    }
    public void AniStateEnd(string StateAniname,Player_State newState,int numInfo = 0)
    {
        if (numInfo == 1)
        {
            anim.SetBool("Idle_Shoot", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(numInfo).IsName(StateAniname) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            SetState(newState);
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            stat_Player.DamgaeSend(1);
       //     SetState(Player_State.Hit);
        }
      
        if (col.transform.tag == "DeadZone")
        {
            stat_Player.current_Hp = 0.0f;

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DeadZone")
        {
            stat_Player.current_Hp = 0.0f;
            // SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        if (other.transform.name == "HeartBullet")
        {
            stat_Player.DamgaeSend(other.gameObject.GetComponent<HeartBullet>().Attack);
        //    SetState(Player_State.Hit);
        }
        if (other.transform.name == "ShoulderStrapBullet")
        {
            stat_Player.DamgaeSend(1);
           // SetState(Player_State.Hit);
        }
        if (other.transform.name == "JellyBullet")
        {
            stat_Player.DamgaeSend(1);
          //  SetState(Player_State.Hit);
        }
        if (other.transform.name == "Hp")
        {
            if(stat_Player.current_Hp<5)
            {
                stat_Player.EatHpObject();
                Destroy(other.gameObject);
            }
           
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "DeadZone")
        {
            stat_Player.current_Hp = 0.0f;
           // SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        if (other.gameObject.tag == "Bubblegum")
        {
            eating = true;
            stat_Player.SetBulletNum("Bubblegum");
            stat_Player.SetBulletState(stat_Player.getCurrentBulletState());
            Destroy(other.gameObject.transform.gameObject);
        }
    }
}
