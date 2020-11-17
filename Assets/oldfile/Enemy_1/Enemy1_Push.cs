using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Push : Enenmy_1StateManager
{
    public GameObject Obj;
    public GameObject Obj_copy;

    float distance;
    public override void BeginState()
    {
        base.BeginState();
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (manager.wiat_Enemy != null)
            manager.wiat_Enemy.SetBool("Break_Boll", false);
        manager.SetState(Enemy_1State.Idle);
    }
     public void Push()
    {
        if (!Obj_copy)
        {
            Obj_copy = Instantiate(Obj, new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), transform.rotation, this.transform);
            Obj_copy.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0);
        }
    }
}
