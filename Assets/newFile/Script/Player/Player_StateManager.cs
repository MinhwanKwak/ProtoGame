using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateManager : MonoBehaviour
{
    public Player_Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<Player_Manager>();
    }
}
