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
        if (PlayerManager.Instance.playerControll.playerStatu != PlayerStatus.RUN && PlayerManager.Instance.playerControll.playerStatu != PlayerStatus.DEAD)
        {
            Attack();
        }
        if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(PlayerManager.Instance.PlayerUI.MouseClick, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerManager.Instance.playerControll.GetAttack())
        {

            Cursor.SetCursor(PlayerManager.Instance.PlayerUI.MouseNonClick, Vector2.zero, CursorMode.ForceSoftware);
            AudioManager.Instance.PlaySoundSfx("WieldSword");
            inputReceived = true;
            CanReciveInput = false;
            PlayerManager.Instance.playerControll.playerStatu = PlayerStatus.ATTACK;
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
