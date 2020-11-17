using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStateManager
{
    Vector3 MoveDir;
    bool g_check = false;
    public float jump_p = 20;
    public Camera cam;
    bool jump_c = false;
    public override void BeginState()
    {
        g_check = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        MoveDir = transform.TransformDirection(MoveDir * Time.deltaTime);
        MoveDir.y = 10.0f;
        base.BeginState();
    }
   // Update is called once per frame
    void Update()
    {
        if (jump_c)
        {
            if (manager.cc.isGrounded && !g_check)
            {
                g_check = true;

                MoveDir = transform.TransformDirection(MoveDir * Time.deltaTime);
                MoveDir.y = jump_p;
                manager.cc.Move(MoveDir * Time.deltaTime);
            }
            else if (manager.cc.isGrounded && g_check)
            {
                jump_c = false;
                manager.SetState(PlayerState.Idle);
            }
            else
            {
                //  manager.anim.SetInteger("SetAnim", 0);
                MoveDir.y -= manager.gravity * Time.deltaTime;
                manager.cc.Move(MoveDir * Time.deltaTime);
            }
        }
    }
    public void Jump()
    {
        jump_c = true;
    }
}
