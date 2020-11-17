using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_Attack : Ant_StateManager
{
    public override void BeginState()
    {
        base.BeginState();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 destination = manager.PlayerPos.position;

        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude > 2.0f * 2.0f)
        {
            manager.SetState(Ant_State.Chase);
        }
        if (!manager.Detect(manager.Sight, manager.p_col))
        {
            manager.SetState(Ant_State.Idle);
        }
        if (manager.hp <= 0)
            manager.Dead();
    }
}
