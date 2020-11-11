using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
   private void AttackStart()
    {
        for(int i =0; i  < GameManager.Instance.playercontroller.weapons.Length; ++i)
        {
            GameManager.Instance.playercontroller.weapons[i].trailRenderer.enabled = true;
            GameManager.Instance.playercontroller.weapons[i].MeleeArea.enabled = true;

        }
    }

    private void AttackEnd()
    {
        for (int i = 0; i < GameManager.Instance.playercontroller.weapons.Length; ++i)
        {
            GameManager.Instance.playercontroller.weapons[i].trailRenderer.enabled = false;
            GameManager.Instance.playercontroller.weapons[i].MeleeArea.enabled = false;

        }
    }
}
