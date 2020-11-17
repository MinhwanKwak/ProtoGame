using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class Enemy_2Chase : Enenmy_2StateManager
{
    NavMeshAgent nv;
    public override void BeginState()
    {
        base.BeginState();
        nv = GetComponent<NavMeshAgent>();
        nv.speed = 1.5f;
        nv.isStopped = false;
    }

    // Update is called once per frame
    void Update () {
        nv.SetDestination(manager.target.position);
        if (!Detect(manager.sight, 1, manager.playerCC))
        {
            manager.SetState(Enemy_2State.Idle);
            return;
        }
        Vector3 destination = manager.target.position;
        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude <=2.0f*2.0f)
        {
            nv.isStopped = true;
            manager.SetState(Enemy_2State.Attack);
        }
      
    }
    public bool Detect(Camera sight, float aspect, CharacterController cc)
    {
        if (cc == null)
            return false;
        sight.aspect = aspect;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }
}
