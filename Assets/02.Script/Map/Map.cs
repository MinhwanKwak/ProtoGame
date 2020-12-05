using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonsterRespawn
{
    public string name;
    public Vector3 RespawnPosition;
}

public class Map : MonoBehaviour
{

    public Animator DoorAnim;

    public MonsterRespawn[] MonsterRespawns;

    [HideInInspector]
    public int MapMonsterCount;
    

    private void Start()
    {
        MapMonsterCount = MonsterRespawns.Length;
        for (int i = 0; i  < MonsterRespawns.Length; ++i)
        {
            ObjectPooler.Instance.SpawnFromPool(MonsterRespawns[i].name, MonsterRespawns[i].RespawnPosition, Quaternion.identity);
        }
    }

  public void CheckClearMonster()
    {
        if(MapMonsterCount <= 0)
        {
            DoorAnim.SetTrigger("DoorOpen");
        }
    }

   public void CheckInStage()
    {
        DoorAnim.SetTrigger("DoorClose");
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        yield break;
    }
}
