using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public PlayerManager playerManager;

    public static GameManager Instance;

    public Map[] maps;

    public int CurrentMap = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        // DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
