using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private bool isDamageCheck = true;

    
    private void AttackStart1()
    {
        isDamageCheck = true;
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK1].SetActive(true);
      


    }

    private void AttackEnd1()
    {
        isDamageCheck = true;
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK1].SetActive(false);
        GameManager.Instance.playercontroller.playerStatu = PlayerStatus.IDLE;
      


    }

    private void AttackStart2()
    {
        isDamageCheck = true;
        
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK2].SetActive(true);
        
    }

    private void AttackEnd2()
    {
        isDamageCheck = false;
        
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK2].SetActive(false);
        GameManager.Instance.playercontroller.playerStatu = PlayerStatus.IDLE;
        
    }

    private void DashAttackStart()
    {
        isDamageCheck = true;
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
    

    }
    private void DashAttackEnd()
    {
        isDamageCheck = false;

        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;

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
