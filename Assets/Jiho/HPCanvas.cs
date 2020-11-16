using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCanvas : MonoBehaviour
{
    public RectTransform rootRect;
    public RectTransform anchorRect;

    public RectTransform GetAnchorRect()
    {
        return anchorRect;
    }
}
