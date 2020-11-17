using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartZombie_Hit : HeartZombie_StateManager
{
    public override void BeginState()
    {
        manager.anim.SetTrigger("Hit");
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.hp <= 0)
            manager.Dead();
        if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            manager.SetState(HeartZombie_State.Idle);
        }
    }
}
