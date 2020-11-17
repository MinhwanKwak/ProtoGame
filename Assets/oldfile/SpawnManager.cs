using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] SpawnPos;
    public GameObject[] EnemyObj;
    public GameObject[] EnemyCopyObj;
    public GameObject wall;

    public GameObject HitSmoke;
    public GameObject HitSmoke_;

    bool SpawnCheck;
    bool visit = false;
    private void Update()
    {
        if (visit)
        {
            if (EnemyCopyObj[0] == null && EnemyCopyObj[1] == null && !SpawnCheck)
            {
                for (int i = 0; i <= 3; i++)
                {
                    EnemyCopyObj[i] = Instantiate(EnemyObj[i], SpawnPos[i].transform.position, EnemyObj[i].transform.rotation, null);
                }
                SpawnCheck = true;
            }
            if (EnemyCopyObj[0] == null && EnemyCopyObj[1] == null && EnemyCopyObj[2] == null && EnemyCopyObj[3] == null)
            {
                Destroy(wall.gameObject);
            }
        }
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player"&& !visit)
        {
            visit = true;
            for (int i = 0; i <= 1; i++)
                EnemyCopyObj[i] = Instantiate(EnemyObj[i], SpawnPos[i].transform.position, EnemyObj[i].transform.rotation, null);
            SpawnCheck = false;
        }
    }
}
