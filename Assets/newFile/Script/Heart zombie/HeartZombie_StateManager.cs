using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartZombie_StateManager : MonoBehaviour
{
    public HeartZombie_Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<HeartZombie_Manager>();
    }
}
