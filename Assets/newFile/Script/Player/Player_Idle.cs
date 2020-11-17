using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : Player_StateManager
{
    public override void BeginState()
    {
        manager.eating = false;
        base.BeginState();
    }
    // Update is called once per frame
    void Update()
    {
   //     transform.LookAt(Vector3.Lerp(transform.position, playerTagetPos.position, Time.deltaTime));
    }

}
