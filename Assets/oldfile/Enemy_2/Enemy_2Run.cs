using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class Enemy_2Run : Enenmy_2StateManager
{
    NavMeshAgent nv;
    int rand;
    [SerializeField] Vector3[] m_tfWayPoints = new Vector3[4];

    void MoveToNextWayPoint()
    {
        if(nv.velocity == Vector3.zero)
        {
            rand = Random.Range(0, 3);
            nv.SetDestination(m_tfWayPoints[rand]);
        }
    }

    public override void BeginState()
    {
        base.BeginState();

        nv = GetComponent<NavMeshAgent>();
        nv.isStopped = false;
        nv.speed = 0.5f;
        setPos();
        InvokeRepeating("MoveToNextWayPoint",0.0f,2.0f);
    }
   
	// Update is called once per frame
	void Update () {
        if(transform.position == m_tfWayPoints[rand])
            manager.SetState(Enemy_2State.Idle);

        if (Detect(manager.sight, 1, manager.playerCC))
        {
            CancelInvoke("MoveToNextWayPoint");
            manager.SetState(Enemy_2State.Chase);
            return;
        }
    }
    public bool Detect(Camera sight,float aspect,CharacterController cc)
    {
        if (cc == null)
            return false;
        sight.aspect = aspect;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }
    void setPos()
    {
        m_tfWayPoints[0] = new Vector3(-75.25f, 19.71f, -24.47f);
        m_tfWayPoints[1]= new Vector3(-74.95f, 19.71f, -32.49f);
        m_tfWayPoints[2] = new Vector3(-81.510f, 15.76f, -25f);
        m_tfWayPoints[3] = new Vector3(-82f, 15.76f, -32f);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            CancelInvoke("MoveToNextWayPoint");
            InvokeRepeating("MoveToNextWayPoint", 0.0f, 2.0f);
        }
    }
}
