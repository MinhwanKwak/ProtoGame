using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psuhcc : MonoBehaviour
{
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player_Move>().enabled = false;
            movement = other.transform.forward * -1;
            movement *= 1.5f;
            other.transform.GetComponent<Rigidbody>().AddForce(movement,ForceMode.Impulse);
         //   other.transform.GetComponent<Rigidbody>().MovePosition(other.transform.position + movement);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player_Move>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player_Move>().enabled = false;
            movement = other.transform.forward * -1;
            movement *= 1.5f;
            other.transform.GetComponent<Rigidbody>().AddForce(movement, ForceMode.Impulse);
          //  other.transform.GetComponent<Rigidbody>().MovePosition(other.transform.position + movement);
        }
    }
}
