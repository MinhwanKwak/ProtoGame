using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
   private void AttackStart1()
    {
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK1].SetActive(true);

     
    }

    private void AttackEnd1()
    {
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK1].SetActive(false);
        
        
    }

    private void AttackStart2()
    {
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK2].SetActive(true);
        
    }

    private void AttackEnd2()
    {
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[(int)WeaponHandStatus.LEFT].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[(int)EffectStatus.ATTACK2].SetActive(false);
        

    }



}
