﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Create an instance of the game manager:
    static GameManager instance;

    //Get function to return an a reference of the game manager:
    public static GameManager GetGameManager()
    {
        return instance;
    }

    //Slow Motion variables:
    private SlowMoManager slowMoManager;
    private bool m_isSlowMoAvailable;

    // Awake is called before anything else:
    void Awake()
    {
        //Setting up a single instance of the GameManager that persists through scenes and avoids duplication:
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        //Adding the SlowMoManager as a component:
        if(slowMoManager == null)
        {
            slowMoManager = gameObject.AddComponent<SlowMoManager>();
        }
        
    }

    //Start is called before the first frame of update:
    void Start()
    {
        m_isSlowMoAvailable     = true;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //Getter and setters for isSlowMoAvailable:
    public bool GetIsSlowMoAvailable()
    {
        return m_isSlowMoAvailable;
    }

    public void MakeSlowMoAvailable()
    {
        m_isSlowMoAvailable = true;
    }

    public void MakeSlowMoUnavailable()
    {
        m_isSlowMoAvailable = false;
    }

    //Slow Motion activation and deactivation functions:
    public void ActivateSlowMoWithFollowCam( Vector3 followCamPosition)
    {
        slowMoManager.isSlowMo = true;
        slowMoManager.FollowCam(followCamPosition);
    }

    public void DeactivateSlowMo()
    {
        slowMoManager.isSlowMo = false;
        slowMoManager.ResetCam();
    }
}
