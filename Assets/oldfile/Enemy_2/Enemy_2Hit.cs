using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_2Hit : Enenmy_2StateManager
{
    NavMeshAgent nv;
    public override void BeginState()
    {
        base.BeginState();
        nv = GetComponent<NavMeshAgent>();
        nv.isStopped = true;
    }
    // Update is called once per frame
    void Update () {
        if(manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")&&manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.5f)
        {
            manager.SetState(Enemy_2State.Run);
        }
    }
}
