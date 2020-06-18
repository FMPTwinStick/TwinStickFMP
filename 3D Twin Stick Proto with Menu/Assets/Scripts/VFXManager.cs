using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    static VFXManager instance;

    public static VFXManager GetVFXManager()
    {
        return instance;
    }

    //Making the VFXManager a singleton:
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    //Public Variables/Vfx:

    public GameObject bigExplosion;
    public GameObject tinyExplosion;

    //Functions for calling VFX:
    public void InstantiateBigExplosion(Transform transform )
    {
        
        Instantiate(bigExplosion, transform.position, transform.rotation);
       
    }

    public void InstantiateTinyExplosion( Transform transform )
    {
        
        Instantiate(tinyExplosion, transform.position, transform.rotation);
        
    }

    public void InstantiateTinyExplosion( Vector3 position, Quaternion rotation )
    {

        Instantiate(tinyExplosion, position, rotation);

    }
}
