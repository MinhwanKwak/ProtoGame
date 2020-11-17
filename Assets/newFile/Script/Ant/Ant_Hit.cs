using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_Hit : Ant_StateManager
{
    public override void BeginState()
    {
        manager.nv.isStopped = true;
        base.BeginState();
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            manager.SetState(Ant_State.Run);
        }
        if (manager.hp <= 0)
            manager.Dead();
    }
}
