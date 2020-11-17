using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class Enemy_2Attack : Enenmy_2StateManager
{
    NavMeshAgent nv;
    public override void BeginState()
    {
        base.BeginState();
        nv = GetComponent<NavMeshAgent>();
    }
   
	// Update is called once per frame
	void Update () {
        Vector3 destination = manager.target.position;

        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude > 2.0f * 2.0f)
        {
            manager.SetState(Enemy_2State.Chase);
        }
    }
 
}
