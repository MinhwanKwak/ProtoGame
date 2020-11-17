using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_StateManager : MonoBehaviour
{
    public Ant_Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<Ant_Manager>();
    }
}
