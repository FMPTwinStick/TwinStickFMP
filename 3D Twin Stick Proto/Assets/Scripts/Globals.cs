using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static int enemiesLeft;
    public GameObject doorOne;
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesLeft <= 0)
            Destroy(doorOne.gameObject);
    }
}
