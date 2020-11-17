using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public PlayerManager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

}
