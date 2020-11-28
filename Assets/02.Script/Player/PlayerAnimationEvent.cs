using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private bool isDamageCheck = true;

    
    private void AttackStart1()
    {
        isDamageCheck = true;
        PlayerManager.Instance.playerControll.SetisAttack(true);
        PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        PlayerManager.Instance.playerControll.Effects[(int)EffectStatus.ATTACK1].SetActive(true);
      


    }

    private void AttackEnd1()
    {
        isDamageCheck = true;
       PlayerManager.Instance.playerControll.SetisAttack(false);
       PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
       PlayerManager.Instance.playerControll.Effects[(int)EffectStatus.ATTACK1].SetActive(false);
        PlayerManager.Instance.playerControll.playerStatu = PlayerStatus.IDLE;
      


    }

    private void AttackStart2()
    {
        isDamageCheck = true;
        
        PlayerManager.Instance.playerControll.SetisAttack(true);
        PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        PlayerManager.Instance.playerControll.Effects[(int)EffectStatus.ATTACK2].SetActive(true);
        
    }

    private void AttackEnd2()
    {
        isDamageCheck = false;
        
       PlayerManager.Instance.playerControll.SetisAttack(false);
       PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
       PlayerManager.Instance.playerControll.Effects[(int)EffectStatus.ATTACK2].SetActive(false);
        PlayerManager.Instance.playerControll.playerStatu = PlayerStatus.IDLE;
        
    }

    private void DashAttackStart()
    {
        isDamageCheck = true;
        PlayerManager.Instance.playerControll.SetisAttack(true);
        PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
    

    }
    private void DashAttackEnd()
    {
        isDamageCheck = false;

        PlayerManager.Instance.playerControll.SetisAttack(false);
        PlayerManager.Instance.playerControll.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;

    }


    public bool GetDamageCheck()
    {
        return isDamageCheck;
    }
    public void SetDamageCheck(bool damage)
    {
        isDamageCheck =  damage;
    }



}
