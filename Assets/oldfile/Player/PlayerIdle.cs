using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerStateManager {
    Vector3 moveDir;
    public override void BeginState()
    {
        base.BeginState();
    }
    // Update is called once per frame
    void Update () {
        if (!manager.cc.isGrounded)
        {
            moveDir.y -= manager.gravity * Time.deltaTime;
            manager.cc.Move(moveDir * Time.deltaTime);
        }
        //moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDir = transform.TransformDirection(moveDir * Time.deltaTime);
        //manager.cc.Move(moveDir * Time.deltaTime);
    }
}
