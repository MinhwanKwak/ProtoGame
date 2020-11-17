using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Backwalk : Player_StateManager
{
    Player_Move p_move;
    public override void BeginState()
    {
        p_move = GetComponent<Player_Move>();
        base.BeginState();
    }
    private void FixedUpdate()
    {
        p_move.move();
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
            manager.SetState(Player_State.FrontWalk);
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.Rotate(0, (90 * Input.GetAxisRaw("Horizontal")), 0, Space.Self);
            // manager.anim.SetInteger("SetAnim", -2);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Rotate(0, (-45 * Input.GetAxisRaw("Horizontal")), 0, Space.Self);
            // manager.anim.SetInteger("SetAnim", -2);
        }
        if (Input.GetAxisRaw("Vertical") == -1)
            manager.anim.SetInteger("SetAnim", -1);

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            manager.SetState(Player_State.Idle);
    }
}
