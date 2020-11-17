using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enenmy_1StateManager : MonoBehaviour {
    public Enemy_1Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<Enemy_1Manager>();
    }

}
