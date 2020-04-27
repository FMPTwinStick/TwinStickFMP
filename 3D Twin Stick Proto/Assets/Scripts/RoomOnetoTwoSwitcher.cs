using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOnetoTwoSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomTwoCam; 

    void start()
    {
        roomTwoCam.enabled = false;
    }

    void OnTriggerEnter()
    {
        GameMode.roomOneActive = false;
        GameMode.roomTwoActive = true;
        roomOneCam.enabled = false;
        roomTwoCam.enabled = true;
        playerOneTank.transform.position += new Vector3(0, 0, 20); 
    }

}

