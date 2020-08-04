using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwotoOnwSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
 

    void start()
    {
        //makes sure the correct camera is active when the scene is loaded
       
    }

    void OnTriggerEnter()
    {
        //activates the next room when the player walks into the trigger
        GameMode.roomOneActive = true;
        GameMode.roomTwoActive = false;
        roomOneCam.enabled = true;
        playerOneTank.transform.position -= new Vector3(0, 0, 20);
        roomOneCam.transform.position -= new Vector3(0, 0, 40);
        //SlowMoManager.GetSloMoManager().SetNewCameraPos(new Vector3(-3, 43, -90));
    }

}

