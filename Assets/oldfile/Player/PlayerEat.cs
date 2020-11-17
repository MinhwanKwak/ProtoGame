using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerEat : PlayerStateManager {
    public GameObject Cookie;
    public string PerCookie = "";

    public static bool AttackCookie;

    public override void BeginState()
    {
        manager.move_effect.Stop();

        base.BeginState();
    }
    // Update is called once per frame
    void Update () {
        Debug.Log("eat");
       
        if (Cookie != null)
        {
            manager.i_itemS = Cookie.transform.GetComponent<ItemStat>();
            manager.bulletCount = manager.i_itemS.getBulletCount();
            manager.bulletMaxCount = manager.bulletCount;
            PerCookie = Cookie.tag;
            Destroy(Cookie.gameObject);
            AttackCookie = true;
            manager.b_bullet = true;
        }
        manager.SetState(PlayerState.Idle);
	}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((hit.transform.tag == "Cookie" || hit.transform.tag == "Bubblegum" || hit.transform.tag == "WhippingCream")&&!Cookie)
        {
            Cookie = hit.gameObject;
        }
    }
    public string getCookie()
    {
        if (PerCookie != null)
            return PerCookie;
        else
            return "";
    }
}
