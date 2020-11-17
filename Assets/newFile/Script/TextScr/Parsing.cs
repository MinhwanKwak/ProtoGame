using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parsing : MonoBehaviour {

    public Dialouge[] Parse(string _CSVFileName)
    {
        List<Dialouge> dialougeList = new List<Dialouge>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });
        for (int i = 0; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Dialouge dialouge = new Dialouge();
            dialouge.name = row[1];
            Debug.Log(row[1]);
            dialouge.misson_state = row[0];
            Debug.Log(row[0]);
            List<string> ContextList = new List<string>();
            ContextList.Add(row[2]);
            Debug.Log(row[2]);
            if (++i < data.Length) {; }

            dialouge.contexts = ContextList.ToArray();
            dialougeList.Add(dialouge);
        }
        return dialougeList.ToArray();
    }
}
