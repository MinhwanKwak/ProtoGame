using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed;

    //   float mass = 3.0f; // the lower the mass, the higher the impact
    //float hitForce = -1.5f; // impact "force" when hit by rigidbody 
    //private Vector3 impact = Vector3.zero; // character momentum 
    //   private CharacterController character;

    //void Start()
    //   {
    //       character = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    //   }

    //   void AddImpact(Vector3 force )
    //   {
    //       Vector3 dir = force.normalized;
    //       dir.y = 0.5f; // add some velocity upwards - it's cooler this way
    //       impact += dir.normalized * force.magnitude / mass;
    //   }

    //   void Update()
    //   {
    //       if (impact.magnitude > 0.2)
    //       { // if momentum > 0.2...
    //           character.Move(impact * Time.deltaTime); // move character
    //       }
    //       // impact vanishes to zero over time
    //       impact = Vector3.Lerp(impact, Vector3.zero, 20 * Time.deltaTime);
    //   }

    //   void OnCollisionEnter(Collision col)
    //   { // collision adds impact
    //       AddImpact(col.relativeVelocity * hitForce);
    //   }
    private void Update()
    {
        transform.Translate(1.0f * speed * Time.deltaTime, 0,0);
    }
  
}
