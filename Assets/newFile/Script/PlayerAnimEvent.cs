using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public string Player_Foot_Hard = "Player_Foot_Hard";
    private void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
    }
    public void AttackHitCheck()
    {
    }
    public void AniJump()
    {
        transform.root.GetComponent<Player_Move>().jump();
    }
  
    public void  CallAnimeDestroy()
    {
        transform.root.GetComponent<Player_DestoryHouse>().SetAnimeDestroy();
    }
    public void CallAnimShoot()
    {
        transform.root.GetComponent<Player_Shooting>().Anishoot();
    }
    public void Player_Foot()
    {
        AkSoundEngine.PostEvent(Player_Foot_Hard, gameObject);
    }
}
