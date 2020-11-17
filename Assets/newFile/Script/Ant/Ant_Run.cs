using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_Run : Ant_StateManager
{
    float Timer;
    int rand;
    Vector3 tagetRand;
    public override void BeginState()
    {
        manager.nv.isStopped = false;
        rand = Random.Range(-3,2);
        tagetRand = new Vector3(transform.position.x + rand, transform.position.y, transform.position.z + rand);
        Timer = 0f;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>=2.0f)
            manager.SetState(Ant_State.Run);
        manager.nv.SetDestination(tagetRand);
        if (tagetRand == transform.position)
            manager.SetState(Ant_State.Idle);
        if (manager.hp <= 0)
            manager.Dead();
    }
}
