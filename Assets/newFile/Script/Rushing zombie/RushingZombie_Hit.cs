using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushingZombie_Hit : RushingZombie_StateManager
{
    public override void BeginState()
    {
        manager.nv.isStopped = true;
        manager.anim.SetTrigger("Hit");
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            manager.SetState(RushingZombie_State.Move);
        }
    }
}
