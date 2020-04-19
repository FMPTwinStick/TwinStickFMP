using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalDoor : MonoBehaviour
{

    public GameObject finalDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        finalDoor.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
