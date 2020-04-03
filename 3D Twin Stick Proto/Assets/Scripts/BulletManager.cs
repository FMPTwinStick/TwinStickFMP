using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static int maxPlayerBullets;
    public static int currentPlayerBullets;

    // Start is called before the first frame update
    void Start()
    {
        maxPlayerBullets        = 40;
        currentPlayerBullets    = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
