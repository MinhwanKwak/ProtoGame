using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//근거리 원거리
public enum Type {Almost, Distance};

public class Weapon : MonoBehaviour
{
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider MeleeArea;
    public TrailRenderer trailRenderer;

    public LayerMask targetMask;

    WaitForSecondsRealtime timestop;
    public float TimeStop = 0f;
    
    
    
    private void Start()
    {
        timestop = new WaitForSecondsRealtime(TimeStop);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetMask) != 0)
        {
            StartCoroutine(TestTime());
            GameObject newProjectile = GameObject.Instantiate(GameManager.Instance.playercontroller.Effects[3]) as GameObject;
            newProjectile.transform.position = other.gameObject.transform.position;
            DestroyTest(newProjectile);
           
            //  StartCoroutine(GameManager.Instance.cameraManager.camerashake.ShakeCamera());

        }
    }
    

    //오브젝트풀링으로 바꿀예정
    void DestroyTest(GameObject Test)
    {
        Destroy(Test, 1.0f);
    }
    
    IEnumerator TestTime()
    {
        Time.timeScale = 0f;

        yield return timestop;

        Time.timeScale = 1f;

    }

}
