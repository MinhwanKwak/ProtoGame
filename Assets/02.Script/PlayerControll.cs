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

    public GameObject DashEffect;

    public bool isGround;



    [SerializeField]
    private float AttackDelay = 1f;

    public PlayerStatus playerStatu = PlayerStatus.IDLE;

    


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
                animator.SetBool("Run", true);

                transform.position += moveValue * speed * Time.deltaTime;
            }
            else
            {
                if (playerStatu != PlayerStatus.DASH)
                {
                    animator.SetBool("Run", false);
                    playerStatu = PlayerStatus.IDLE;
                }
            }
        }
    }

    
    
    
    IEnumerator Dash()
    {
        
        if (playerStatu != PlayerStatus.ATTACK)
        {
            //대쉬 에 힘에 의해 이 방향으로 dash time 만큼 이동한다 

            if(DashTime <= delTime)
            {

                animator.SetTrigger("Dash");
                DashEffect.SetActive(true);
                delTime = 0f;
                playerStatu = PlayerStatus.DASH;
                transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower , 0 , CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);
            }
        }

        yield return new WaitForSeconds(DashTime);
        DashEffect.SetActive(false);
        playerStatu = PlayerStatus.IDLE;
    }


    

    public void SetMousePointLook(Vector3 look)
    {
        CurrentMouseLook = look;
    }


}
