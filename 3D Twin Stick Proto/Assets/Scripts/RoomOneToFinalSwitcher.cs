using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOneToFinalSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomThreeCam;

    void start()
    {
        roomThreeCam.enabled = true;
    }

    void OnTriggerEnter()
    {
        GameMode.roomOneActive = false;
        GameMode.roomThreeActive = true;
        roomOneCam.enabled = false;
        roomThreeCam.enabled = true;
        playerOneTank.transform.position -= new Vector3(0, 0, 20);
    }

}


