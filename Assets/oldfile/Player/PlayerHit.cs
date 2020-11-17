using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHit : PlayerStateManager
{
    float time;
    public override void BeginState()
    {
        base.BeginState();
        time = 0.0f;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.0f)
            manager.SetState(PlayerState.Idle);
    }
}