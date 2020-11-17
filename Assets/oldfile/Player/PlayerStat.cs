using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

    public int max_hp = 5;
    public int currentHp;
    bool Invincibility = false;
    float Invinci_t = 0.0f;
    private void Start()
    {
        currentHp = max_hp;
    }
    private void Update()
    {

        if (Invincibility)
        {
            Invinci_t += Time.deltaTime;

            if (Invinci_t >= 2.0f)
            {
                Invinci_t = 0.0f;
                Invincibility = false;
            }
        }
    }
    public void PlayerTakeDamage(int damage)
    {
        if (!Invincibility)
        {
            transform.GetComponent<PlayerManager>().SetState(PlayerState.Hit);
            Invincibility = true;
            currentHp -= damage;
            if (currentHp <= 0)
            {
                Debug.Log("playerDie");
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
           
                PlayerTakeDamage(hit.gameObject.GetComponent<Enemy>().EnemyBodyDamage);
        }
    }
}
