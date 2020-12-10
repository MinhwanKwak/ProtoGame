using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInCheck : MonoBehaviour
{
   public LayerMask PlayerLayer;

    public Animator DoorAnim;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if ((1 << (other.gameObject.layer) & PlayerLayer) != 0)
        {
            DoorAnim.SetTrigger("InStage");
            yield break;
        }
    }
}
