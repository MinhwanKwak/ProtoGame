using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum DirAxis
{
    xAxis,
    yAxis
}

public class HeartZombie_Move : HeartZombie_StateManager
{
    Vector3 move;
    float time;
    float Turn;
    public float TurnTime;
    public float max_h;
    public GameObject bullet;
    public GameObject bullet_Casting;

    GameObject InstageBullet;
    Transform currentPlayerPos;
    public DirAxis dirAxis;
    Vector3 moveDir;
    public float IntervalSpeed;
    float IntervalTimer;

    public float player_Destance;
    public Transform Heartzombie_hand;

    public override void BeginState()
    {
        Turn = 1;
        currentPlayerPos = manager.PlayerPos;
        if (dirAxis == DirAxis.xAxis)
            moveDir = Vector3.right;
        if (dirAxis == DirAxis.yAxis)
            moveDir = Vector3.forward;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        IntervalTimer += Time.deltaTime;
        time += Time.deltaTime;
        move = transform.position + (moveDir * Turn);
        manager.nv.SetDestination(move);
        if (time >= TurnTime)
        {
            transform.Rotate(0, 180, 0, Space.Self);
            time = 0.0f;
            Turn *= -1;
            manager.nv.ResetPath();
        }
        Vector3 destination = manager.PlayerPos.position;
        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude >= player_Destance * player_Destance)
            manager.nv.isStopped = true;
        //if (IntervalTimer >= IntervalSpeed && diff.sqrMagnitude <= player_Destance * player_Destance)
        //{
        //    manager.nv.isStopped = false;
        //    IntervalTimer = 0.0f;
        //    BulletCreat();
        //}
        if (manager.hp <= 0)
            manager.Dead();
    }

    public void AnimBulletCall()
    {
        AkSoundEngine.PostEvent("HeartZombie_Ready", gameObject);
    }
    public void handBulletCreat()
    {
        Vector3 destination = manager.PlayerPos.position;
        Vector3 diff = destination - transform.position;
        if (diff.sqrMagnitude <= player_Destance * player_Destance)
        {
            InstageBullet = Instantiate(bullet_Casting, transform.position + Vector3.up * 1.5f, bullet.transform.rotation, Heartzombie_hand);
        }
    }
    public void BulletCreat()
    {
        // Vector3 yZeroPlayer = new Vector3(manager.PlayerPos.position.x, 0, manager.PlayerPos.position.z);
        //  InstageBullet.GetComponent<HeartBullet>().Shoot(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), yZeroPlayer, 9.8f, max_h);
        if (InstageBullet != null)
        {
            Destroy(InstageBullet);
            InstageBullet = Instantiate(bullet, transform.position + Vector3.up * 1.5f, bullet.transform.rotation, null);
            InstageBullet.name = "HeartBullet";
            InstageBullet.transform.LookAt(manager.PlayerPos);
            InstageBullet.GetComponent<HeartBullet>().setAttack(manager.Attack);
            InstageBullet.GetComponent<HeartBullet>().enabled = true;
        }
    }
}
