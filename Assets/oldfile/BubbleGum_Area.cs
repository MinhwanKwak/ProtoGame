
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum_Area : MonoBehaviour
{
    public Transform child;
    public GameObject HitBubble;
    public GameObject HitBubble_;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.transform.tag == "Bubblegum")
        {
            child.gameObject.SetActive(true);
            //HitBubble_ = Instantiate(HitBubble, transform.position, HitBubble.transform.rotation, child.gameObject.transform);
        }
        Debug.Log("예압!");
    }
}
