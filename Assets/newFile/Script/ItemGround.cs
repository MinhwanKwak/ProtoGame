using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGround : MonoBehaviour
{
    int itemNum = 0;
    private void Start()
    {
        itemNum += 1;
        if(this.tag == "Bubblegum")
        this.name = "Bubblegum"; 
    }
   public  void SetPos(int setNum = 1)
    {
        itemNum = setNum;
    }
    public int GetitemNum()
    {
        return itemNum;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log(itemNum);
            if (itemNum == 0)
                return;
            if (itemNum == 1)
                transform.position += Vector3.forward;
            if (itemNum == 2)
                transform.position -= Vector3.forward;
            if (itemNum == 3)
                transform.position += Vector3.right;
            if (itemNum == 4)
                transform.position -= Vector3.right;
            if (itemNum == 5)
                transform.position -= Vector3.forward + Vector3.right;
            if (itemNum == 6)
                transform.position += Vector3.forward + Vector3.right;
            if (itemNum == 7)
                transform.position -= Vector3.forward - Vector3.right;
            if (itemNum == 8)
                transform.position += Vector3.forward - Vector3.right;
            transform.position = new Vector3(transform.position.x, transform.position.y+0.25f, transform.position.z);

        }
    }
}
