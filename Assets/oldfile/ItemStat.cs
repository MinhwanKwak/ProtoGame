using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    public float speed = 1;
    public float distance = 2.5f;
    public float damage = 5.0f;
    public string s_tag;
    public int bullet_count;

    private void Start()
    {
        s_tag = transform.tag;
    }
    public int getBulletCount()
    {
        return bullet_count;
    }
   
}
