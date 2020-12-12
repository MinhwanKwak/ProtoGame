using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    public MonsterBasic monster;

    public void StartBossAttack()
    {
        //if (!monster.IsInSight)
        //{
        //    monster.animator.SetTrigger("Idle");
        //    monster.monsterStatus = MonsterStatus.IDLE;
        //}
        monster.Nav.isStopped = true;
    }

    public void FinishedBossAttack()
    {
        monster.Nav.isStopped = false;
        monster.IsProgressAttack = false;
        monster.IsAttackOneTouch = false;

    }

    public void StartBossWalk()
    {
        monster.Nav.isStopped = false;
    }

    public void EndBossWalk()
    {
        monster.Nav.isStopped = false;
    }

    public void BossDead()
    {
        monster.Dead();
        
    }
}
