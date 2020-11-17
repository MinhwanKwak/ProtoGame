using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : Player_StateManager
{
    public Transform playerTagetPos;
    float Timer;
    ItemManager manager_imte;
    float intervalSpeed;
    bool shoot = false;
    Player_Move p_move;
   
    public override void BeginState()
    {
        manager.anim.SetLayerWeight(1, 1);
        intervalSpeed = 0f;
        p_move = GetComponent<Player_Move>();
        manager_imte = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        shoot = true;
        manager.anim.SetBool("Idle_Shoot", shoot);
        manager.anim.SetTrigger("shooting");
        base.BeginState();
    }
    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            manager.anim.SetInteger("SetAnim", (int)Player_State.FrontWalk);
           p_move.move();
        }
        else
        {
            manager.anim.SetInteger("SetAnim", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            intervalSpeed = manager_imte.intervalSpeed[manager.stat_Player.getCurrentBulletState()];
            Timer += Time.deltaTime;
            if (Timer >= intervalSpeed)
            {
                manager.anim.SetTrigger("shooting");
                Timer = 0f;
                shoot = true;
            }
        }
        else
        {
            manager.AniStateEnd("Shooting", Player_State.Idle,1);
        }

    }
    public void Anishoot()
    {
        if (shoot)
        {
            GetComponent<Player_Magazine>().Shooting();
            Timer = 0f;
        }
    }
}
