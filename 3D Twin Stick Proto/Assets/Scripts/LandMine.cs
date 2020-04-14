using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{

    public Collider cubeCollider;
    public Collider sphereCollider;
    bool landMineActive = false;
  //  bool destroyLandmine = false;


    // Start is called before the first frame update
    void Start()
    {
        sphereCollider.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
      //  DestroyLandmine();
    }
    

    void OnTriggerExit(Collider other)
    {
        landMineActive = true; 
    }
   
    void OnTriggerEnter(Collider other)
    {

        if (landMineActive == true)
        {
            sphereCollider.enabled = true;
            Destroy(other.gameObject);
       //     destroyLandmine = true;
     
        }
    }

    //void DestroyLandmine()
    //{
    //    if (destroyLandmine == true)
    //        Destroy(gameObject);   

    //}


}
