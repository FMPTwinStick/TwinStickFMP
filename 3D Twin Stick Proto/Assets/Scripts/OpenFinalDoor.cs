using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalDoor : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        GameMode.finalKeyActive = true;
        Destroy(gameObject);
    }

}
