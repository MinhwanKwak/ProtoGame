using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartZombie_Idle : HeartZombie_StateManager
{
    float Timer;
    public override void BeginState()
    {
        Timer = 0;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 4.0f)
            manager.SetState(HeartZombie_State.Attack);
        if (manager.hp <= 0)
            manager.Dead();
    }
}
