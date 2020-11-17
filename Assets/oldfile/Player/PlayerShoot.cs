using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum bulletkind
{
    Cookie,
    Gum,
    bim
}

public class PlayerShoot : PlayerEat {
    bulletkind b_kind;
    public GameObject[] P_bullet;
    public GameObject N_bullet;
    public Camera cam;
    RaycastHit temp;

    public override void BeginState()
    {
        manager.move_effect.Stop();
        base.BeginState();
    }
	// Update is called once per frame
	void Update () {
        if (AttackCookie)
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 200.0f, Color.green);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out temp, Mathf.Infinity)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
            {
                //충돌이 검출되면 총알의 리스폰포인트가 충돌이 발생한위치를 바라보게 만든다.
                // 이 상태에서 발사입력이 들어오면 총알은 충돌점으로 날아가게 된다.
                Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
            }

            if (manager.bulletCount < 0)
            {
                AttackCookie = false;
                manager.b_bullet = false;
            }
            manager.SetState(PlayerState.Idle);
        }
        else
        {
            manager.SetState(PlayerState.Idle);
        }
	}
    public void BulletShootTiming()
    {

        manager.bulletCount -= 1;
        if (manager.bulletCount <= 0)
        {
            AttackCookie = false;
            manager.b_bullet = false;
        }
        if (manager.i_itemS.s_tag == "Cookie")
        {
            N_bullet = Instantiate(P_bullet[(int)bulletkind.Cookie], transform.position, transform.rotation);
            N_bullet.GetComponent<bullet>().SetBullet(manager.i_itemS.speed, manager.i_itemS.distance, manager.i_itemS.damage);
            N_bullet.transform.LookAt(temp.point);

        }
        if (manager.i_itemS.s_tag == "Bubblegum")
        {
            N_bullet = Instantiate(P_bullet[(int)bulletkind.Gum], transform.position, transform.rotation);
            N_bullet.GetComponent<bullet>().SetBullet(manager.i_itemS.speed, manager.i_itemS.distance, manager.i_itemS.damage);
            N_bullet.transform.LookAt(temp.point);
        }
        if (manager.i_itemS.s_tag == "WhippingCream")
        {
            N_bullet = Instantiate(P_bullet[(int)bulletkind.bim], transform.position, transform.rotation,transform);
            N_bullet.GetComponent<lineRendererTest>().SetBullet(manager.i_itemS.speed, manager.i_itemS.distance, manager.i_itemS.damage,temp);
        }
    }
}
