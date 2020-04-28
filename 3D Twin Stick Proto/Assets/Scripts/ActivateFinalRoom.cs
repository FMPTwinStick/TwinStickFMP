using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalRoom : MonoBehaviour
{

    public GameObject finalDoor;
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;


    // Start is called before the first frame update
    void Start()
    {
        enemyOne.SetActive(false);
        enemyTwo.SetActive(false);
        enemyThree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 

    }

    void OnTriggerExit(Collider other)
    {
        GameMode.enemiesLeft += 3;
        finalDoor.gameObject.SetActive(true);
        enemyOne.SetActive(true);
        enemyTwo.SetActive(true);
        enemyThree.SetActive(true);
        Destroy(gameObject);
    }

}
