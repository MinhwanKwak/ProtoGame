using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    IDLE,
    ATTACK,
    RUN,
    DASH,
}

public class PlayerControll : MonoBehaviour
{

    public Rigidbody PlayerRigidbody;

    [Range(0, 50)]
    public float speed = 4f;

    [Range(0, 50)]
    public float DashSpeed = 25f;

    public float Dashpower = 20f;

    public float DashTime;

    public PlayerAnimationEvent playerAnimationEvent;

    public Weapon[] weapons;

    private Weapon CurrentWeapon;

    public Animator animator;

    private float delTime = 0f;
    
    private string currentState;


    Vector2 CurrentInput;
    Vector3 CurrentMouseLook;

    WaitForSeconds Attacktime;





    [SerializeField]
    private float AttackDelay = 1f;

    public PlayerStatus playerStatu = PlayerStatus.IDLE;


    public const string PLAYER_IDLE = "Player_Idle";
    public const string PLAYER_RUN = "Player_Run";
    public const string PLAYER_DASH = "Player_Dash";
    public const string PLAYER_ATTACK1 = "Player_Attack1";
    public const string PLAYER_ATTACK2 = "Player_Attack2";


    private bool isAttack = false;
    
    private float h;
    private float v;

    private int AttackCombo = 1;
    void Start()
    {

        CurrentWeapon = weapons[0];
        
    }
    private void FixedUpdate()
    {
        delTime += Time.deltaTime;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        
        CurrentInput = new Vector2(h, v);
        
        PlayerMouseCheck();
        PlayerMoveCheck();
    }

    private void PlayerMouseCheck()
    {
        if (Input.GetMouseButtonDown(0) && playerStatu != PlayerStatus.ATTACK )
        {
            StartCoroutine(Attack());
        }
        if (Input.GetMouseButtonDown(1) && playerStatu != PlayerStatus.ATTACK)
        {
            StartCoroutine(Dash());
        }

    }


    private void PlayerMoveCheck()
    {
        if (playerStatu != PlayerStatus.ATTACK)
        {

            if (CurrentInput.sqrMagnitude > 0.1f)
            {
                Vector3 moveValue = CurrentInput.y * transform.forward + CurrentInput.x * transform.right;

                playerStatu = PlayerStatus.RUN;
                ChangeAnimationState(PLAYER_RUN);

                transform.position += moveValue * speed * Time.deltaTime;
            }
            else
            {
                if (playerStatu != PlayerStatus.DASH)
                {
                    ChangeAnimationState(PLAYER_IDLE);
                    playerStatu = PlayerStatus.IDLE;
                }
            }
        }
    }


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;

    }

    void AttackDelayTIme()
    {
        playerStatu = PlayerStatus.IDLE;
    }


    IEnumerator Attack()
    {
        isAttack = true;
        playerStatu = PlayerStatus.ATTACK;

        switch (AttackCombo)
        {
            case 1:
                ChangeAnimationState(PLAYER_ATTACK1);
                ++AttackCombo;
                break;
            case 2:
                ChangeAnimationState(PLAYER_ATTACK2);
                AttackCombo = 1;
               break;
            //case 3:
            //    ChangeAnimationState(PLAYER_ATTACK3);
            //    ++AttackCombo;
            //    break;
            //case 4:
            //    ChangeAnimationState(PLAYER_ATTACK4);
            //    ++AttackCombo;
            //    break;
            //case 5:
            //    ChangeAnimationState(PLAYER_ATTACK5);
            //    AttackCombo = 1;
            //    break;
        }
        yield return Attacktime;

        AttackDelay = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("AttackDelayTIme", AttackDelay);
    }

    IEnumerator Dash()
    {
        
        if (playerStatu != PlayerStatus.ATTACK)
        {
            //대쉬 에 힘에 의해 이 방향으로 dash time 만큼 이동한다 

            if(DashTime <= delTime)
            {
                //애니메이션이 약함 
                //ChangeAnimationState(PLAYER_DASH);
                delTime = 0f;
                playerStatu = PlayerStatus.DASH;
                transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower , 0 , CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);
            }
        }

        yield return new WaitForSeconds(DashTime);
        
        playerStatu = PlayerStatus.IDLE;
    }


    

    public void SetMousePointLook(Vector3 look)
    {
        CurrentMouseLook = look;
    }


}
