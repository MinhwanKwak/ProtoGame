using System;
using System.Collections;

[Serializable]
public class MonsterStatusValue
{
    public string monsterName;
    public float hp;
    public float maxHp;

    public void Initialize()
    {
        hp = maxHp;
    }
}
