public enum MonsterStatus
{
    IDLE,
    RUN,
    ATTACK,
    RECEIVEDATTACK,
    DEAD,
}


public enum WeaponType { Almost, Distance };



public enum PlayerStatus
{
    IDLE ,
    ATTACK,
    RUN,
    DASH,
    DASHATTACK,
}


public enum EffectStatus
{
    DASH_EFFECT = 0,
    ATTACK1,
    ATTACK2,
    HITEFFECT,
    DASHATTACK,
}


public enum WeaponHandStatus
{
    LEFT = 0,
    RIGHT= 1
}


public enum ItemStatus
{
    SPEEDITEM, 
    ATTACKITEM,
    ARMORITEM,

}


public enum GroundStatus
{
    GROUND,
    NONGROUND
}