using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartZombie_Attack : HeartZombie_StateManager
{
    public GameObject bullet;
    GameObject InstageBullet;
    float Timer;
    Transform currentPlayerPos;
    public override void BeginState()
    {
        transform.LookAt(manager.PlayerPos);
        currentPlayerPos = manager.PlayerPos;
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 4.0f)
            manager.SetState(HeartZombie_State.Idle);
        if (manager.hp <= 0)
            manager.Dead();
    }
    //public void AnimBulletCall()
    //{
    //    InstageBullet = Instantiate(bullet, transform.position,bullet.transform.rotation,null);
    //    InstageBullet.name = "HeartBullet";
    //    InstageBullet.transform.LookAt(manager.PlayerPos);
    //    Vector3 yZeroPlayer = new Vector3(manager.PlayerPos.position.x, 0, manager.PlayerPos.position.z);
    //    InstageBullet.GetComponent<HeartBullet>().Shoot(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), yZeroPlayer, 9.8f, 5);
    //    InstageBullet.GetComponent<HeartBullet>().setAttack(manager.Attack);
    //}
}
