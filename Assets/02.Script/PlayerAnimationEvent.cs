using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
   private void AttackStart1()
    {
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[0].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[1].SetActive(true);

     
    }

    private void AttackEnd1()
    {
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[0].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[1].SetActive(false);
        
        
    }

    private void AttackStart2()
    {
        GameManager.Instance.playercontroller.SetisAttack(true);
        GameManager.Instance.playercontroller.weapons[0].MeleeArea.enabled = true;
        GameManager.Instance.playercontroller.Effects[2].SetActive(true);
        
    }

    private void AttackEnd2()
    {
        GameManager.Instance.playercontroller.SetisAttack(false);
        GameManager.Instance.playercontroller.weapons[0].MeleeArea.enabled = false;
        GameManager.Instance.playercontroller.Effects[2].SetActive(false);
        

    }



}
