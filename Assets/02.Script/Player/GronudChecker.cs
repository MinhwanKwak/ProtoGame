using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GronudChecker : MonoBehaviour
{
    public GroundStatus groundStatus;

    public LayerMask groundlayer;

    private bool isGroundCheck = false;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
        {
            groundStatus = GroundStatus.GROUND;
        }
        else
        {
            groundStatus = GroundStatus.NONGROUND;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundlayer) != 0)
        {
          Animator groundAnimator =   other.gameObject.GetComponent<Animator>();
          groundAnimator.SetTrigger("GroundDown");
        }
      
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundlayer) != 0)
        {
            Animator groundAnimator = other.gameObject.GetComponent<Animator>();
            groundAnimator.SetTrigger("GroundUp");
        }

    }
}
