using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enenmy_2StateManager : MonoBehaviour {
    public Enemy_2Manager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<Enemy_2Manager>();
    }

}
