using System;

[Serializable]
public class MonsterStatusValue
{
    public string monsterName;
    public float hp;
    public float maxHp;
    public float range;

    public float tickRate;

    public void Initialize()
    {
        hp = maxHp;
    }
}
