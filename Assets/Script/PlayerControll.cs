using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public enum PlayerStatus
{
    IDLE,
    ATTACK,
    RUN
}

public class PlayerControll : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Range(0, 50)]
    public float speed = 4f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public Animator animator;
    private string currentState;

    float horizontal;
    float vertical;
    Vector3 direction;

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

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

         direction = new Vector3(horizontal, 0f, vertical).normalized;
 
        if(Input.GetMouseButtonDown(0) && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            
            if (direction.sqrMagnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(MoveDir.normalized * speed * Time.deltaTime);
                ChangeAnimationState(PLAYER_RUN);
                playerStatu = PlayerStatus.RUN;

            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
                playerStatu = PlayerStatus.IDLE;

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
        isAttack = false;
    }


    IEnumerator Attack()
    {
        isAttack = true;
        playerStatu = PlayerStatus.ATTACK;

        switch(AttackCombo)
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

}
