using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEvent : MonoBehaviour {
    [SerializeField] DialougeEvent dialouge;
    public int xLine = 1;
    public int yLine = 14;

    public Dialouge[] getDialouge()
    {
        dialouge.dialouges = DataManager.instance.getDialouge((int)xLine, (int)yLine);
        return dialouge.dialouges;
    }
}
