using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushingZombie_Attack : RushingZombie_StateManager
{
    float time;
    public MosterArea monsterGround;
    public float Runspeed = 5.0f;
    Vector3 prePlayerPos;
    public override void BeginState()
    {
        manager.nv.speed = Runspeed;
        time = 0f;
        prePlayerPos = manager.PlayerPos.position;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (monsterGround.playerCatch())
            manager.nv.SetDestination(prePlayerPos);
        if (manager.nv.remainingDistance==0.0f)
            {
                if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    manager.SetState(RushingZombie_State.Move);
                }
            }
        if (manager.hp <= 0)
                manager.Dead();
    }
}
