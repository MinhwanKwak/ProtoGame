using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationEvent : MonoBehaviour
{
    public MonsterBasic monster;

    public void StartAttack()
    {
        if(!monster.IsInSight)
        {
            monster.animator.SetTrigger("Idle");
        }
        monster.Nav.isStopped = true;

    }

    public void FinishedAttack()
    {
        monster.Nav.isStopped = false;
        monster.GetComponent<MonsterControl>().IsProgressAttack = false;

    }
}
