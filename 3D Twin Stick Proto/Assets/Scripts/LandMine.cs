using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public GameObject fuseBurnout;
    public Collider cubeCollider;
    public Collider sphereCollider;
    public GameObject explosion;
    bool landMineActive = false;
    public float fuseLength;
    float timer;
  //  bool destroyLandmine = false;


    // Start is called before the first frame update
    void Start()
    {
        sphereCollider.enabled = false;
        explosion.gameObject.SetActive(false);
        timer = 0.0f;
        fuseLength = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //  DestroyLandmine();
        timer += Time.deltaTime;
        if (timer >= fuseLength)
        {
            TriggerLandmine();
        }
    }
    

    void OnTriggerExit(Collider other)
    {
        landMineActive = true;
        if (timer == fuseLength)
        {
            sphereCollider.enabled = true;
            Destroy(other.gameObject);
            Invoke("DestroyLandmine", 0.1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (landMineActive == true)
        {
            sphereCollider.enabled = true;
            explosion.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Invoke("DestroyLandmine", 0.1f);
        }
   
       
        
    }

    void DestroyLandmine()
    {
        Destroy(gameObject);
    }

    void TriggerLandmine()
    {
        Instantiate(fuseBurnout, transform.position, Quaternion.identity);
    }



}




