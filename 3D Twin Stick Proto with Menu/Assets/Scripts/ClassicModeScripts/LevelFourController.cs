﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetGameManager().SetEnemiesLeft(3);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameManager().GetEnemiesLeft() == 1)
        {
            GameManager.GetGameManager().MakeSlowMoAvailable();
        }

        if (GameManager.GetGameManager().GetEnemiesLeft() <= 0)
        {
            GameManager.GetGameManager().MakeSlowMoUnavailable();
            GameManager.GetGameManager().DeactivateSlowMo();
            SceneManager.LoadScene("ClassicLevel5");
        }

    }
}