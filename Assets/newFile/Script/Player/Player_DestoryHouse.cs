using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DestoryHouse : Player_StateManager
{
    float time;
    bool DestroyAnim = false;
    public MeshCollider col;
    bool anim_Attack = false;
    string currentAnimName;
    public override void BeginState()
    {
        manager.anim.SetLayerWeight(1,0);
        manager.anim.SetTrigger("DestoryHouse");
        manager.anim.SetInteger("Attack_Hammer", 0);
        DestroyAnim = false;
        manager.stat_Player.currentWeaponfunc(3);
        time = 0f;
        col.enabled = true;
        anim_Attack = false;
        currentAnimName = "Hammer_attack_1";
        base.BeginState();
    }
    private void Update()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        if (Input.GetMouseButtonDown(1) && time <= 2.0f&& !anim_Attack)
        {
            time = 2.0f;
            col.enabled = true;
            manager.anim.SetInteger("Attack_Hammer", 1);
            currentAnimName = "Hammer_attack_2";
            anim_Attack = true;
        }
        else if (Input.GetMouseButtonDown(1)&& anim_Attack)
        {
            col.enabled = true;
            manager.anim.SetInteger("Attack_Hammer", 2);
            currentAnimName = "Hammer_attack_3";
        }
        else
            manager.AniStateEnd(currentAnimName, Player_State.Idle);
        if (time >= 4f)
            manager.SetState(Player_State.Idle);
        if (manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            col.enabled = false;
            DestroyAnim = false;
        }
    }
    public void SetAnimeDestroy()
    {
        DestroyAnim = true;
    }
    public  bool GetDestroyAnim()
    { return DestroyAnim; }
}
