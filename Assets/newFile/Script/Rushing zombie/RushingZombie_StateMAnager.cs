using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushingZombie_StateManager : MonoBehaviour
{
    public RushingZombie_Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<RushingZombie_Manager>();
    }
}
