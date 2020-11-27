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
        if (GameManager.Instance.playercontroller.playerStatu != PlayerStatus.RUN )
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.playercontroller.GetAttack())
        {
            inputReceived = true;
            CanReciveInput = false;
            GameManager.Instance.playercontroller.playerStatu = PlayerStatus.ATTACK;
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
