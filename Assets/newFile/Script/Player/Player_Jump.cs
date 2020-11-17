using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : Player_StateManager
{
    Player_Move p_move;
    bool jumpState;
    bool rotation_b;
    public override void BeginState()
    {
       p_move = GetComponent<Player_Move>();
        rotation_b = false;
        manager.anim.SetLayerWeight(1, 0);
        manager.anim.ResetTrigger("JumpDown");
        manager.anim.ResetTrigger("JumpUp");
        manager.anim.SetTrigger("JumpIng");
        // manager.anim.SetTrigger("JumpUp");
        base.BeginState();
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    startJump_t += Time.deltaTime;
        //    if (startJump_t <= 1.5f)
        //    {
        //        if (Input.GetAxisRaw("Vertical") != -1)//뒤로 점프가 아닐때.
        //            p_move.jump(startJump_t);
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //    p_move.jump(0);
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.Rotate(0, (90 * Input.GetAxisRaw("Horizontal")), 0, Space.Self);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Rotate(0, (45 * Input.GetAxisRaw("Horizontal")), 0, Space.Self);
        }


        if (manager.rd.velocity.y > 0)
        {
            manager.anim.SetTrigger("JumpUp");
        }
        if (manager.rd.velocity.y < 1)
        {
            //manager.rd.velocity += Vector3.down * Time.deltaTime* p_move.jumpDownSpeedFunc();
        }

        if (!p_move.Jumping())
        {
            //  manager.AniStateEnd("Jump", Player_State.Idle);
            if (Input.GetKey(KeyCode.W))
                manager.SetState(Player_State.FrontWalk);
            if (Input.GetKey(KeyCode.S))
                manager.SetState(Player_State.BackWalk);
            else
                manager.SetState(Player_State.Idle);
        }
    }
    public bool JumpState()
    {
        return jumpState;
    }
}
