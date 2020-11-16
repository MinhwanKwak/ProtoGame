using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCamera : MonoBehaviour
{
    public Camera Anothercamera;

    public Camera GetAnotherCamera()
    {
        return Anothercamera;
    }
}
