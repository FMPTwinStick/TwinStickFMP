using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRoomTwo : MonoBehaviour
{

    public GameObject doorTwo;
    public GameObject enemyOne;
    public GameObject enemyTwo;

    // Start is called before the first frame update
    void Start()
    {
        enemyOne.SetActive(false);
        enemyTwo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameMode.enemiesLeft <=0)
        //{
        //    doorTwo.gameObject.SetActive(false);
        //}
    }

    void OnTriggerExit(Collider other)
    {
        GameMode.enemiesLeft += 2;
      //  doorTwo.SetActive(true);
        enemyOne.SetActive(true);
        enemyTwo.SetActive(true);
        Destroy(gameObject);
    }

}
