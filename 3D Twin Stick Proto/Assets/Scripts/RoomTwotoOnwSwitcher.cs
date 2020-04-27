using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwotoOnwSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomTwoCam; 

    void start()
    {
        roomTwoCam.enabled = true;
    }

    void OnTriggerEnter()
    {
        GameMode.roomOneActive = true;
        GameMode.roomTwoActive = false;
        roomOneCam.enabled = true;
        roomTwoCam.enabled = false;
        playerOneTank.transform.position -= new Vector3(0, 0, 20); 
    }

}

