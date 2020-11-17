using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hit : Player_StateManager
{
    int rand;
    float time;
    public override void BeginState()
    {
        //rand = Random.Range(0, 2);
        //if (rand == 0)
        //    manager.anim.SetTrigger("HitAnim");
        //else
        //    manager.anim.SetTrigger("HitAnim2");
        //manager.anim.SetLayerWeight(1, 0);
        manager.anim.SetTrigger("HitAnim");
        base.BeginState();
    }
    private void Update()
    {
        //Debug.Log(rand);
        //if (rand == 0)
        //    manager.AniStateEnd("HitAnim", Player_State.Idle);
        //else
        //    manager.AniStateEnd("HitAnim2", Player_State.Idle);
        if (manager.anim.GetCurrentAnimatorStateInfo(0).IsName("HitAnim") && manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (Input.GetKey(KeyCode.W))
                manager.SetState(Player_State.FrontWalk);
            else manager.SetState(Player_State.Idle);
        }
    }
}
