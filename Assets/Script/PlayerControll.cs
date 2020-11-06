using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;


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

    public float DashTime = 0.5f;

    private float delTime = 0f;


    public Animator animator;
    private string currentState;


    Vector2 CurrentInput;
    Vector2 CurrentInputMouseright;
    Vector2 CurrentInputMouserLeft;

    Vector3 CurrentMouseLook;

    WaitForSeconds Attacktime;





    [SerializeField]
    private float AttackDelay = 1f;

    public PlayerStatus playerStatu = PlayerStatus.IDLE;


    public const string PLAYER_IDLE = "Player_Idle";
    public const string PLAYER_RUN = "Player_Run";
    public const string PLAYER_ATTACK1 = "Player_Attack1";
    public const string PLAYER_ATTACK2 = "Player_Attack2";
    public const string PLAYER_ATTACK3 = "Player_Attack3";
    public const string PLAYER_ATTACK4 = "Player_Attack4";
    public const string PLAYER_ATTACK5 = "Player_Attack5";


    private bool isAttack = false;


    private int AttackCombo = 1;
    void Start()
    {
        Attacktime = new WaitForSeconds(0.02f);


    }
    private void Update()
    {
        delTime += Time.deltaTime;

     
    }

    private void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed && playerStatu != PlayerStatus.ATTACK)
        {
            StartCoroutine(Attack());
        }
        if (Mouse.current.rightButton.isPressed && playerStatu != PlayerStatus.ATTACK)
        {
            StartCoroutine(Dash());
        }

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
              if(playerStatu != PlayerStatus.DASH)
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
                ++AttackCombo;
                break;
            case 3:
                ChangeAnimationState(PLAYER_ATTACK3);
                ++AttackCombo;
                break;
            case 4:
                ChangeAnimationState(PLAYER_ATTACK4);
                ++AttackCombo;
                break;
            case 5:
                ChangeAnimationState(PLAYER_ATTACK5);
                AttackCombo = 1;
                break;
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
                delTime = 0f;
                playerStatu = PlayerStatus.DASH;
                transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower , 0 , CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);
            }
        }

        yield return new WaitForSeconds(DashTime);

        playerStatu = PlayerStatus.IDLE;
    }



    public void SetPositionInput(Vector2 input)
    {
        CurrentInput = input;
    }


    public void SetMousePointLook(Vector3 look)
    {
        CurrentMouseLook = look;
    }


}
