using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_Chase : Ant_StateManager
{
    public override void BeginState()
    {
        manager.nv.isStopped = false;
        base.BeginState();
    }
    // Update is called once per frame
    void Update()
    {
        manager.nv.SetDestination(manager.PlayerPos.position);
        if (!manager.Detect(manager.Sight, manager.p_col))
        {
            manager.SetState(Ant_State.Idle);
            return;
        }
        Vector3 destination = manager.PlayerPos.position;
        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude <= 2.0f * 2.0f)
        {
            manager.nv.isStopped = true;
            manager.SetState(Ant_State.Attack);
        }
        if (manager.hp <= 0)
            manager.Dead();
    }
}
