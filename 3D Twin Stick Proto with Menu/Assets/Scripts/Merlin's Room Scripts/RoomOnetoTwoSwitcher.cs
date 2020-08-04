using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOnetoTwoSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
  

    void start()
    {
        
      
    }

    void OnTriggerEnter()
  
    {
        //activates the next room when the player walks into the trigger
        GameMode.roomOneActive = false;
        GameMode.roomTwoActive = true;
        playerOneTank.transform.position += new Vector3(0, 0, 20); 
        roomOneCam.transform.position += new Vector3(0, 0, 40);

    }

}

