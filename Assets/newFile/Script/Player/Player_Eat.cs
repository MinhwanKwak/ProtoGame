using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Eat : Player_StateManager
{
    public override void BeginState()
    {
        manager.anim.SetLayerWeight(1, 0);
        manager.anim.SetTrigger("Eat");
        base.BeginState();
    }
    private void Update()
    {
        manager.AniStateEnd("Eat", Player_State.Idle);
    }
   
}
