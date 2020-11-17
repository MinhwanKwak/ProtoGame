using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEffect : MonoBehaviour
{
    public GameObject[] House_g;
    public bool[] House_check;
    public Vector3[] pos;


    public GameObject House_effect;
    public GameObject House_effect_;

    private void Start()
    {
        int i = 0;
        pos = new Vector3[House_g.Length];
        foreach (GameObject obj in House_g)
        {
            pos[i] = obj.transform.position;
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i< House_g.Length;i++)
        {

            if (House_g[i].gameObject == null && !House_check[i])
            {
                House_check[i] = true;
                if(pos[i].y>=4.0f)
                 House_effect_ = Instantiate(House_effect,new Vector3(pos[i].x, pos[i].y-6.0f, pos[i].z), House_effect.transform.rotation, transform);
              else
                 House_effect_ = Instantiate(House_effect, new Vector3(pos[i].x, pos[i].y-2.0f, pos[i].z), House_effect.transform.rotation, transform);


            }
        }
    }
}
