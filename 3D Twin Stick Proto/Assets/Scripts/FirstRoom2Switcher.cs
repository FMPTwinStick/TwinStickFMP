﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoom2Switcher : MonoBehaviour
{
    // Start is called before the first frame update
    bool roomOneActive;
    bool roomTwoActive;
    public Camera roomOneCam;
    public Camera roomTwoCam;
    void Start()
    {
        roomTwoCam.enabled = false;
        roomOneActive = true;
        roomTwoActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && roomOneActive == true)
        {
            roomOneCam.enabled = false;
            roomTwoCam.enabled = true;
            roomOneActive = false;
         
        }

        if (other.tag == "Player" && roomTwoActive == true)
        {
            roomOneCam.enabled = true;
            roomTwoCam.enabled = false;
            roomOneActive = true;
         

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (roomOneActive == false)
        {
            roomTwoActive = true;
        }
        if (roomTwoActive == false)
        {
            roomOneActive = true;
        }
        if (roomOneActive == true)
        {
            roomTwoActive = false;
        }

    }
}

