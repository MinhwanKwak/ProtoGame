using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public Ant_Manager ant_manager;
    private void Awake()
    {
        ant_manager = transform.GetComponent<Ant_Manager>();
    }
    public void AttackHitCheck()
    {
        ant_manager.AttackCheck();
    }
}
