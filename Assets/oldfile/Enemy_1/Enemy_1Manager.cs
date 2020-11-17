using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_1State
{
    Idle,
    Push,
}
public class Enemy_1Manager : MonoBehaviour {

    public Enemy_1State currentState;
    public Enemy_1State startState;
    public CharacterController cc;
    public Animator wiat_Enemy;
    public Animator anim;

    public float gravity = 20.0f;
    Dictionary<Enemy_1State, Enenmy_1StateManager> states = new Dictionary<Enemy_1State, Enenmy_1StateManager>();

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        states.Add(Enemy_1State.Idle, GetComponent<Enemy_1Idle>());
        states.Add(Enemy_1State.Push, GetComponent<Enemy1_Push>());
    }
    // Use this for initialization
    void Start()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetState(Enemy_1State newState)
    {
        foreach (Enenmy_1StateManager fsm in states.Values)
        {
            fsm.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("SetAnim", (int)currentState);
    }
}
