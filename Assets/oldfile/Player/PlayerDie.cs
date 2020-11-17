using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerDie : PlayerStateManager
{
    public override void BeginState()
    {
        base.BeginState();
        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }
}