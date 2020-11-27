using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public int playerHP = 10;

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

    public Transform PlayerBody;

    public float CurrentSpeed;

    public LayerMask monsterWeaponLayer;
    
    private float delTime = 0f;
    
    private string currentState;

 

    Vector2 CurrentInput;
    Vector3 CurrentMouseLook;
    Vector3 MoveVec;

    WaitForSeconds Attacktime;
    
    [SerializeField]
    private float AttackDelay = 1f;

    public PlayerStatus playerStatu = PlayerStatus.IDLE;

    

    private bool isAttack = false;
    
    private float h;
    private float v;
    
    public GameObject[] Effects;
    void Start()
    {
        CurrentSpeed = speed;
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

    private void Update()
    {
      
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
                Vector3 moveValue = CurrentInput.x * transform.right + CurrentInput.y * transform.forward;

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
                Effects[0].SetActive(true);
                delTime = 0f;
                playerStatu = PlayerStatus.DASH;
                transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower , 0 , CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);
            }
        }

        yield return new WaitForSeconds(DashTime);
        Effects[0].SetActive(false);
        playerStatu = PlayerStatus.IDLE;
    }


    

    public void SetMousePointLook(Vector3 look)
    {
        CurrentMouseLook = look;
    }


    public void SetisAttack(bool attack)
    {
        isAttack = attack;
    }
    public bool GetAttack()
    {
        return isAttack;
    }

    public int GetDamageUI()
    {
        playerHP--;

        return playerHP;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & monsterWeaponLayer) != 0 )//&& other.gameObject.GetComponentInParent<MonsterBasic>().monsterStatus == MonsterStatus.ATTACK)
        {
            GetDamageUI();
        }
    }



}
