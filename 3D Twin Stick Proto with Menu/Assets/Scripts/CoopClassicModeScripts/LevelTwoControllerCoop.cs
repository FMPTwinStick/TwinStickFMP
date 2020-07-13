﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoControllerCoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetGameManager().SetEnemiesLeft(2);
    }

    // Update is called once per frame
    void Update()
    {


     if(GameManager.GetGameManager().GetEnemiesLeft() == 1)
        {
            GameManager.GetGameManager().MakeSlowMoAvailable();
        }

     if(GameManager.GetGameManager().EnemiesLeft <=0)
        {
            GameManager.GetGameManager().MakeSlowMoUnavailable();
            GameManager.GetGameManager().DeactivateSlowMo();
            SceneManager.LoadScene("ClassicLevel3Coop");
        }

    }
}