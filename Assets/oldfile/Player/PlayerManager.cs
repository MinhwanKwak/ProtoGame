using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Eat,
    ShootAttack,
    Attack,
    BackWalk,
    Die,
    Destory,
    Hit

}

public class PlayerManager : MonoBehaviour {
    public PlayerState currentState;
    public PlayerState startState;
    public CharacterController cc;
    public Animator anim;
    public ParticleSystem move_effect;

    PlayerStat ps;
    public float gravity = 20.0f;
    Vector3 moveDir;
    Dictionary<PlayerState, PlayerStateManager> states = new Dictionary<PlayerState, PlayerStateManager>();

    public bool b_bullet = false;
    public int bulletCount = 0;
    public int bulletMaxCount = 0;


    public ItemStat i_itemS;
    private void Awake()
    {
        b_bullet = false;
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        ps = GetComponent<PlayerStat>();
        states.Add(PlayerState.Idle, GetComponent<PlayerIdle>());
        states.Add(PlayerState.Run, GetComponent<PlayerRun>());
        states.Add(PlayerState.Jump, GetComponent<PlayerJump>());
        states.Add(PlayerState.Eat, GetComponent<PlayerEat>());
        states.Add(PlayerState.ShootAttack, GetComponent<PlayerShoot>());
        states.Add(PlayerState.Die, GetComponent<PlayerDie>());
        states.Add(PlayerState.Destory, GetComponent<PlayerDestory>());
        states.Add(PlayerState.Hit, GetComponent<PlayerHit>());

    }
    // Use this for initialization
    void Start () {
        SetState(startState);
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S)))
        {
            SetState(PlayerState.Run);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&currentState!=PlayerState.Run&&currentState != PlayerState.Jump)
        {
            SetState(PlayerState.Jump);
        }
        if (Input.GetMouseButtonDown(0)&& !b_bullet)
        {
            SetState(PlayerState.Eat);
        }
        else if (Input.GetMouseButtonDown(0)&& b_bullet)
        {
            SetState(PlayerState.ShootAttack);
        }
        if(ps.currentHp<=0)
        {
            SetState(PlayerState.Die);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetState(PlayerState.Destory);
        }
    }
    public void SetState(PlayerState newState)
    {
        foreach (PlayerStateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        //if (Input.GetAxisRaw("Vertical") <= -1)
        //    anim.SetInteger("SetAnim", (int)PlayerState.BackWalk);
      //  else
            anim.SetInteger("SetAnim", (int)currentState);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (hit.transform.tag == "DeadZone")
        {
            ps.PlayerTakeDamage(100);
            hit.transform.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
