using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //0 피해량,생존시간,발사속도,발사 간격,탄수

    public float[] Damage = new float[3];
    public float[] alive = new float[3];
    public float[] speedBullet = new float[3];
    public  float[] intervalSpeed = new float[3];
    public int[] Bulletcount = new int[3];
    public int[] BulletcountMax = new int[3];

    public GameObject[] Bullet;
   
    
}
