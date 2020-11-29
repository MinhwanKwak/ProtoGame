using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAnimationEvent : MonoBehaviour
{
    public MonsterBasic monster;

    public void StartAttack()
    {
        if(!monster.IsInSight)
        {
            monster.animator.SetTrigger("Idle");
            monster.monsterStatus = MonsterStatus.IDLE;
        }
        monster.Nav.isStopped = true;
    }

    public void FinishedAttack()
    {
        monster.Nav.isStopped = false;
        monster.IsProgressAttack = false;

    }

    public void StartWalk()
    {
        monster.Nav.isStopped = false;
    }

    public void EndWalk()
    {
        monster.Nav.isStopped = false;
    }

    public void ZombieDead()
    {
        monster.Dead();
        
    }
}
