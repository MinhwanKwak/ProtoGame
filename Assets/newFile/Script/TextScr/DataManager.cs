using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager instance;

    Dictionary<int, Dialouge> dialougeDic = new Dictionary<int, Dialouge>();

    public static bool isFinish = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Parsing theParser = GetComponent<Parsing>();
            Dialouge[] dialouges = theParser.Parse("TextData");
            for (int i = 0; i < dialouges.Length; i++)
            {
                dialougeDic.Add(i + 1, dialouges[i]);
            }
            isFinish = true;
        }
    }
    public Dialouge[] getDialouge(int strtNum, int endNum)
    {
        List<Dialouge> dialougeList = new List<Dialouge>();
        for (int i = 0; i <= endNum - strtNum; i++)
            dialougeList.Add(dialougeDic[strtNum + i]);
        return dialougeList.ToArray();
    }

}
