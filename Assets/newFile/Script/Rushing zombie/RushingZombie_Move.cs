using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushingZombie_Move : RushingZombie_StateManager
{
    float time;
    public float move_Timer;
    public override void BeginState()
    {
        manager.prePlayerPos = Vector3.zero;
        manager.nv.isStopped = false;
        time = 0f;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
            time += Time.deltaTime;
            if (time >= move_Timer)
            {
                manager.SetState(RushingZombie_State.Attack);
            }
            if (manager.hp <= 0)
                manager.Dead();
    }
}
