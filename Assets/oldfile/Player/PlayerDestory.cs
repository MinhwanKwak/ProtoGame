using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerDestory : PlayerStateManager
{
    float time;
    public override void BeginState()
    {
        base.BeginState();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.0f)
            manager.SetState(PlayerState.Idle);
    }
}