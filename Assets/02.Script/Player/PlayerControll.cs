using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{

    public Rigidbody PlayerRigidbody;
    
    [Range(0, 50)]
    public float speed = 4f;

    [Range(0, 50)]
    public float DashSpeed = 25f;

    public float Dashpower = 20f;
    
    public int Dashcount = 3;

    public float DashTime;

    public float DashAttackTime;

    public float DashReloadCoolTime;

    public PlayerAnimationEvent playerAnimationEvent;

    public Weapon[] weapons;

    private Weapon CurrentWeapon;

    public Animator animator;

    public Transform PlayerBody;

    public float CurrentSpeed;

    public LayerMask monsterWeaponLayer;
    
    private float delTime = 0f;

    private float DashCooldelTIme;

    Vector2 CurrentInput;
    Vector3 CurrentMouseLook;

    Vector3 PlayerBodyVec;
    Vector3 moveValue;
    Vector3 moveNormalized;

    public Transform Hittransform;
    
    [SerializeField]
    public PlayerStatus playerStatu = PlayerStatus.IDLE;

    

    private bool isAttack = false;

    private bool isSpaceKey = false;

    private float h;
    private float v;
    
    
    public GameObject[] Effects;
    void Start()
    {
        CurrentSpeed = speed;
        CurrentWeapon = weapons[0];
        StartCoroutine(DashCheck());
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
        DashCooldelTIme += Time.deltaTime;
        if(DashCooldelTIme > DashReloadCoolTime)
        {
            DashCooldelTIme = 0f;
            if (Dashcount >= 3)
            {
                return;
            }
            Dashcount++;
            
        }
       
      
    }

    private void PlayerMouseCheck()
    {
        
      
        if (Input.GetMouseButtonDown(1) && playerStatu != PlayerStatus.ATTACK && DashTime <= delTime)
        {
            StartCoroutine(Dash());
        }
       else if(isSpaceKey && Dashcount != 0 &&Input.GetMouseButton(0) && playerStatu != PlayerStatus.ATTACK && DashAttackTime <= delTime)
        {
            StartCoroutine(DashAttack());
        }

    }


    private void PlayerMoveCheck()
    {
            if (CurrentInput.sqrMagnitude > 0.1f && playerStatu != PlayerStatus.ATTACK)
            {
                moveValue = CurrentInput.x * transform.right + CurrentInput.y * transform.forward;

                playerStatu = PlayerStatus.RUN;
                animator.SetBool("Run", true);

                MoveAnimCheck();


                transform.position += moveValue * speed * Time.deltaTime;
            }
            else
            {
                if (playerStatu != PlayerStatus.DASH)
                {
                    playerStatu = PlayerStatus.IDLE;
                    animator.SetBool("Run", false);
                }
            }
        
    }


    public void MoveAnimCheck()
    {
        PlayerBodyVec = GameManager.Instance.cameraManager.PlayerBodyTransform.forward.normalized;
        moveNormalized = moveValue.normalized;
        Vector3 test = PlayerBodyVec + moveNormalized;


        animator.SetFloat("DirX", Mathf.Abs(test.x));
        animator.SetFloat("DirZ", Mathf.Abs(test.z));


    }



    IEnumerator Dash()
    {
        delTime = 0f;

        animator.SetTrigger("Dash");
        Effects[0].SetActive(true);

        playerStatu = PlayerStatus.DASH;
        transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower, 0, CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);


        float ReTime = DashTime - 0.2f;
        yield return new WaitForSeconds(ReTime);
        Effects[0].SetActive(false);
        playerStatu = PlayerStatus.IDLE;
    }

     IEnumerator DashAttack()
    {
        Dashcount--;
        delTime = 0f;

        animator.SetTrigger("DashAttack");
        Effects[4].SetActive(true);
        playerStatu = PlayerStatus.DASHATTACK;

        transform.position += new Vector3(CurrentMouseLook.normalized.x * DashSpeed * Time.deltaTime * Dashpower, 0, CurrentMouseLook.normalized.z * DashSpeed * Time.deltaTime * Dashpower);


        float ReTime = DashAttackTime - 0.2f;
        yield return new WaitForSeconds(ReTime);

        Effects[4].SetActive(false);
        playerStatu = PlayerStatus.DASHATTACK;
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


    public IEnumerator DashCheck()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isSpaceKey = true;
            }

            yield return new WaitForSeconds(0.2f);

            isSpaceKey = false;
        }
    }

    //public int GetDamageUI()
    //{
    //    playerHP--;

    //    return playerHP;
    //}

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (((1 << other.gameObject.layer) & monsterWeaponLayer) != 0 )//&& other.gameObject.GetComponentInParent<MonsterBasic>().monsterStatus == MonsterStatus.ATTACK)
    //    {
    //        GetDamageUI();
    //    }
    //}



}
