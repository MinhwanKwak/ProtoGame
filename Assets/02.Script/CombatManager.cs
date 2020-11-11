using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public bool CanReciveInput;
    public bool inputReceived;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inputReceived = true;
            CanReciveInput = false;
        }
        else
        {
            return;
        }
    }


    public void InputManager()
    {
        if(!CanReciveInput)
        {
            CanReciveInput = true;
        }
        else
        {
            CanReciveInput = false;
        }
    }
}
