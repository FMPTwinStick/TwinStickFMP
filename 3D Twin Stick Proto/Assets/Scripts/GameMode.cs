﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode: MonoBehaviour
{

    public static int enemiesLeft;
    public GameObject doorOne;
    public GameObject doorTwo;
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesLeft <= 0)
        {
            doorOne.gameObject.SetActive(false);
            doorTwo.gameObject.SetActive(false);
        }
        
     
    }
}