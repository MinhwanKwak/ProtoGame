using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_Idle : Ant_StateManager
{
    float Timer;
    public override void BeginState()
    {
        base.BeginState();
    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 2.0f)
        {
            Timer = 0.0f;
            manager.SetState(Ant_State.Run);
        }
        if (manager.Detect(manager.Sight, manager.p_col))
        {
            manager.SetState(Ant_State.Chase);
            return;
        }
        if (manager.hp <= 0)
            manager.Dead();
    }
}
