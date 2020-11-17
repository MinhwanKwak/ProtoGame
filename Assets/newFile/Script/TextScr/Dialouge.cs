using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialouge
{
    public string name;
    public string misson_state;
    public string[] contexts;
}
[System.Serializable]
public class DialougeEvent
{
    public string name;

    public Dialouge[] dialouges;
}
