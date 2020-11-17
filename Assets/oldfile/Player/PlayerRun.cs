using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerStateManager {
    Vector3 moveDir;
    public Camera cam; //메인카메라
    public float moveSpeed = 3.0f;
    public float jupm_p;

    ParticleSystem.MainModule pm;
    public override void BeginState()
    {
        pm = manager.move_effect.main;
        manager.move_effect.Play();
        pm.loop = true;
        base.BeginState();
    }
   
    // Update is called once per frame
    void Update () {
        if (manager.cc.isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                moveDir = cam.transform.right * Input.GetAxisRaw("Horizontal");
                moveDir *= moveSpeed;
            }
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                moveDir = cam.transform.forward * Input.GetAxisRaw("Vertical");
                if (Input.GetAxisRaw("Vertical") <= -1)
                {
                    moveDir *= moveSpeed / 2;
                    manager.anim.SetInteger("SetAnim", (int)PlayerState.BackWalk);
                }
                else
                    moveDir *= moveSpeed;
            }
            if (Input.GetAxisRaw("Horizontal") != 0&& Input.GetAxisRaw("Vertical") != 0)
            {
               // Debug.Log(Input.GetAxisRaw("Horizontal")+","+ Input.GetAxisRaw("Vertical"));
                if (Input.GetAxisRaw("Vertical") <= -1)
                {
                    moveDir = -cam.transform.forward + cam.transform.right * Input.GetAxisRaw("Horizontal");
                    moveDir *= moveSpeed / 2;
                    manager.anim.SetInteger("SetAnim", (int)PlayerState.BackWalk);
                }
                else
                {
                    moveDir = cam.transform.forward + cam.transform.right * Input.GetAxisRaw("Vertical") * Input.GetAxisRaw("Horizontal");
                    moveDir *= moveSpeed;
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                manager.move_effect.Stop();
                pm.loop = false;
                manager.SetState(PlayerState.Idle);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = jupm_p;
                Debug.Log(moveDir.y);
                //manager.anim.SetInteger("SetAnim", (int)PlayerState.Jump);
               // manager.cc.Move(moveDir * Time.deltaTime);
             //   manager.SetState(PlayerState.Jump);
            }
            manager.cc.Move(moveDir * Time.deltaTime);
        }
        else
        {
            moveDir.y -= manager.gravity * Time.deltaTime;
            manager.cc.Move(moveDir * Time.deltaTime);

        }
        //if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        //    manager.SetState(PlayerState.Idle);
    }
}
//transform.rotation = Quaternion.RotateTowards(
//     transform.rotation,
//     Quaternion.LookRotation(moveDir),
//     90.0f * Time.deltaTime
//     );