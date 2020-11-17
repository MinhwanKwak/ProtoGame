using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Idle : Enenmy_1StateManager
{
    float F_Time = 0.0f;
    public GameObject Obj;
    public GameObject Obj_copy;
    float distance;

    public override void BeginState()
    {
        Obj_copy = GetComponent<Enemy1_Push>().Obj_copy;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        base.BeginState();
    }
   
	// Update is called once per frame
	void Update () {
        F_Time += Time.deltaTime;
        if(F_Time>=3.0f&& !Obj_copy)
        {
            F_Time = 0.0f;
           
            manager.SetState(Enemy_1State.Push);
        }
        if (Obj_copy != null)
            distance = Vector3.Distance(transform.position,Obj_copy.transform.position);
        if (distance >= 35.0f)
        {
            if (manager.wiat_Enemy != null)
                manager.wiat_Enemy.SetBool("Break_Boll", true);

            Destroy(Obj_copy.gameObject);
           
        }
    }
   
}
