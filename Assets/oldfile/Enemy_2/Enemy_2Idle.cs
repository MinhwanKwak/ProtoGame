using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_2Idle : Enenmy_2StateManager
{
    float i_Time = 0.0f;
    NavMeshAgent nv;
    public override void BeginState()
    {
        base.BeginState();
        nv = GetComponent<NavMeshAgent>();
        nv.isStopped = false;
    }

    // Update is called once per frame
    void Update () {
        i_Time += Time.deltaTime;
        if(i_Time>=2.0f)
        {
            i_Time = 0.0f;
            manager.SetState(Enemy_2State.Run);
        }
        if (Detect(manager.sight, 1, manager.playerCC))
        {
            manager.SetState(Enemy_2State.Chase);
            return;
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
