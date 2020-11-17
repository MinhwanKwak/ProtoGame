using UnityEngine;
using System.Collections;

public class HeartBullet : MonoBehaviour
{
    public int Attack;
    float Timer;
    public float aliveTime;
    Vector3 playerPos;
    public GameObject Damage_eff;
    private void Start()
    {
        transform.name = "HeartBullet";
        AkSoundEngine.RegisterGameObj(gameObject);
        AkSoundEngine.PostEvent("Berry_Fire", gameObject);
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);
        // transform.position = Vector3.MoveTowards(transform.position, PlayerPos.position,Time.deltaTime*20.0f);
        if (Timer >= aliveTime)
            Destroy(transform.gameObject);
    }
    public void setAttack(int attack)
    {
        Attack = attack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Enemy_Bullet")
            return;
        Instantiate(Damage_eff,transform.position,transform.rotation);
        Destroy(transform.gameObject);
    }
}
