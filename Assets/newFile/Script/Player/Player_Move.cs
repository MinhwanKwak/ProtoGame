using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rd;
    public ParticleSystem run_dust;
    public ParticleSystem jump_dust;
    public Transform foot;


    Vector3 movement;
    Vector3 jumpVect;
    public float speed;
    public float jumpPower;
    float jump_DownSpeed = 1.0f;
    float Push_time = 1.0f;
    bool isJumping;
    public float jumpMove_d;
   // public float jumpTime;
    bool stanfingJump = false;
    bool groundCheck;
    ParticleSystem obj = null;

    Player_Manager manager;

    Vector3 playerPervPos;
    Vector3 playerPos;

    float xRot;
    float yRot;
    //  float startJump_t;
    public float d; 
    // Start is called before the first frame update
    void Start()
    {
        //startJump_t = 1.0f;
        cam = Camera.main;
        rd = GetComponent<Rigidbody>();
        manager = GetComponent<Player_Manager>();
        jumpVect = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) || groundCheck)
            run_dust.gameObject.SetActive(false);
        if (((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))&&!groundCheck)
            run_dust.gameObject.SetActive(true);
        if (groundCheck&& (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            move();


        //Ray ray = Camera.main.ScreenPointToRay(Vector3.down);
        //RaycastHit temp;
        //if (Physics.Raycast(transform.position, -transform.up, out temp, Mathf.Infinity, 1)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
        //{
        //    if (temp.transform.tag == "Ground")
        //    {
        //        Vector3 destination = temp.transform.position;
        //        Vector3 diff = destination - transform.position;
        //        if (Vector3.Distance(temp.transform.position,transform.position) <= d)
        //            manager.anim.SetInteger("Jump", 2);
        //        Debug.Log(Vector3.Distance(temp.transform.position, transform.position));
        //    }
        //    Debug.DrawRay(transform.position, -transform.up * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
        //}
        //Debug.DrawRay(transform.position, -transform.up * 10f, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && !groundCheck)
        {
            isJumping = true;
            manager.anim.ResetTrigger("JumpDown");
            manager.anim.ResetTrigger("JumpUp");
            manager.anim.SetTrigger("JumpIng");
            jump();
        }
    }
    private void FixedUpdate()
    {
        if (rd.velocity.y > 0)
        {
            manager.anim.SetTrigger("JumpUp");
        }
        if (rd.velocity.y < 0)
        {
            jumpVect.Set(movement.x, -1, movement.z);
            rd.velocity += jumpVect * Time.deltaTime;
        }
    }
    public void move()
    {

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            movement = cam.transform.right * Input.GetAxisRaw("Horizontal");
            movement *= speed;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            movement = cam.transform.forward * Input.GetAxisRaw("Vertical");
            if (Input.GetAxisRaw("Vertical") <= -1)
            {
                movement *= speed / 2;
            }
            else
                movement *= speed;
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            // Debug.Log(Input.GetAxisRaw("Horizontal")+","+ Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Vertical") <= -1)
            {
                movement = -cam.transform.forward + cam.transform.right * Input.GetAxisRaw("Horizontal");
                movement *= speed / 2;
            }
            else
            {
                movement = cam.transform.forward + cam.transform.right * Input.GetAxisRaw("Vertical") * Input.GetAxisRaw("Horizontal");
                movement *= speed;
            }
        }
        //movement.Set(h, y, v);
        //movement = movement.normalized * speed * Time.deltaTime;
        rd.MovePosition(transform.position + movement);
    }
    public void jump()
    {
        if (!isJumping)
            return;
        //if (Input.GetAxisRaw("Horizontal") !=0||Input.GetAxisRaw("Vertical")!=0&& !stanfingJump)
        //{
        //    Vector3 MoveCam = Vector3.zero;
        //    obj = Instantiate(jump_dust, foot.position, jump_dust.transform.rotation,null);
        //    isJumping = false;
        //    groundCheck = true;
        //    if (Input.GetAxisRaw("Vertical") != 0)
        //    {
        //        MoveCam = cam.transform.forward;
        //        movement = MoveCam * (Input.GetAxisRaw("Vertical") * jumpMove_d);
        //    }
        //    if (Input.GetAxisRaw("Horizontal") != 0)
        //    {
        //        MoveCam = cam.transform.right;
        //        movement = MoveCam * (Input.GetAxisRaw("Horizontal") * jumpMove_d);
        //    }
        //    if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        //    {

        //        if(Input.GetAxisRaw("Vertical") == 1)
        //        {
        //            if (Input.GetAxisRaw("Horizontal") == -1)
        //            {
        //                MoveCam = -cam.transform.right + cam.transform.forward;
        //                movement = MoveCam * (1 * Input.GetAxisRaw("Vertical") * jumpMove_d);
        //            }
        //            if (Input.GetAxisRaw("Horizontal") == 1)
        //            {
        //                MoveCam = cam.transform.right + cam.transform.forward;
        //                movement = MoveCam * (1 * Input.GetAxisRaw("Vertical") * jumpMove_d);
        //            }
        //        }
        //        if (Input.GetAxisRaw("Vertical") == -1)
        //        {
        //            if (Input.GetAxisRaw("Horizontal") == -1)
        //            {
        //                MoveCam = cam.transform.right + cam.transform.forward;
        //                movement = MoveCam * (1 * Input.GetAxisRaw("Vertical") * jumpMove_d);
        //            }
        //            if (Input.GetAxisRaw("Horizontal") == 1)
        //            {
        //                MoveCam = -cam.transform.right + cam.transform.forward;
        //                movement = MoveCam * (1 * Input.GetAxisRaw("Vertical") * jumpMove_d);
        //            }
        //        }
        //    }
        //    jumpVect.Set(movement.x, 1, movement.z);
        //    rd.velocity = jumpPower * jumpVect;
        //    //  if (Mathf.Clamp(transform.position.y, -0.5f, 2) <= transform.position.y)
        //    Debug.Log(rd.velocity);
        //}
        //else
        //{

        obj = Instantiate(jump_dust, foot.position, jump_dust.transform.rotation, null);
            isJumping = false;
            groundCheck = true;
            stanfingJump = true;
            rd.velocity = jumpPower * Vector3.up;
    //    }
    }
    public bool Jumping()
    {
        return groundCheck;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            manager.anim.SetTrigger("JumpDown");
            groundCheck = false;
            stanfingJump = false;
            manager.anim.ResetTrigger("JumpIng");
            Destroy(obj);
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.tag == "Ground")
    //    {
    //        //startJump_t = 1.0f;
    //    }
    //}
}
