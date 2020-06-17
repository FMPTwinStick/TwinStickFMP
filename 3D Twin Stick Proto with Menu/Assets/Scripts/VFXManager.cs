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

    //Private variables:

    //private bool isCullingTimerOn;
    //private float cullingTimer;
    //private float bigExplosionDuration;

    // Start is called before the first frame update
    void Start()
    {
        //isCullingTimerOn = false;
        //cullingTimer = 0f;
        //bigExplosionDuration = 2f;

        //bigExplosion.SetActive(true);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (isCullingTimerOn)
    //    {
    //        //do culling timer
    //        if(cullingTimer < bigExplosionDuration)
    //        {
    //            cullingTimer += Time.deltaTime;
    //        }

    //        if (cullingTimer >= bigExplosionDuration)
    //        {
    //            bigExplosion.SetActive(false);
    //            cullingTimer = 0f;
    //            isCullingTimerOn = false;
    //        }
    //    }
    //}

    public void InstantiateBigExplosion(Transform transform )
    {
        Debug.Log("Big Explosion!");
        Instantiate(bigExplosion, transform.position, transform.rotation);
        //bigExplosion.SetActive(true);
        //isCullingTimerOn = true;
    }
}
