using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Create an instance of the game manager:
    static GameManager instance;

    //Get function to return an a reference of the game manager:
    public static GameManager GetGameManager()
    {
        return instance;
    }

    private Camera  camera;
    private Vector3 m_ogCamPosition;
    private float   m_ogCamOrthographicSize;
    private bool    m_isSlowMoAvailable;

    //private SlowMoManager slowMoManager;

    //public SlowMoManager getTimeManager()
    //{
    //    return slowMoManager;
    //}

    private float defaultSlowTimeScale;
    public bool isSlowMo;

    private float followCamLerpPercent;
    private float followCamLerpSpeed;
    private float slowMoLerpPercent;
    private float slowMoLerpSpeed;



    // Awake is called befoee anything else:
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        
    }

    //Start is called before the first frame of update:
    void Start()
    {
        //slowMoManager           = new SlowMoManager();

        defaultSlowTimeScale    = 0.1f;
        isSlowMo                = false;

        camera                  = Camera.main;
        m_ogCamPosition         = camera.transform.position;
        m_ogCamOrthographicSize = camera.orthographicSize;
        m_isSlowMoAvailable     = true;

        RayTraceBulletBehaviour.canAffectTimeScale = true;

        followCamLerpPercent    = 0f;
        followCamLerpSpeed      = 0.003f;
        slowMoLerpPercent       = 0f;
        slowMoLerpSpeed         = 0.003f;
    }

    // Update is called once per frame
    void Update()
    {
        AlterTime();
        StoreOriginalCameraSettings();
    }

    void AlterTime()
    {
        if (isSlowMo)
        {
            SlowTime();
        }
        if (!isSlowMo)
        {
            ResetTime();
        }
    }
    void SlowTime()
    {
        if (m_isSlowMoAvailable)
        {
            if (slowMoLerpPercent < 1f)
            {
                slowMoLerpPercent += slowMoLerpSpeed;
            }
            if (slowMoLerpPercent > 1f)
            {
                slowMoLerpPercent = 1f;
            }
            Time.timeScale = Mathf.Lerp(1f,defaultSlowTimeScale,slowMoLerpPercent);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    void SlowTime( float timeScale )
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void ResetTime()
    {
        Time.timeScale = 1f;
        RayTraceBulletBehaviour.canAffectTimeScale = true;
    }

    public void FollowCam(Vector3 position)
    {
        if (m_isSlowMoAvailable)
        {
            StoreOriginalCameraSettings();
            Vector3 followPos = new Vector3(position.x, position.y + 10f, position.z - 10f);
            if (followCamLerpPercent < 1f)
            {
                followCamLerpPercent += followCamLerpSpeed;
            }
            if(followCamLerpPercent > 1f)
            {
                followCamLerpPercent = 1f;
            }
            camera.transform.position   = Vector3.Lerp(m_ogCamPosition,followPos,followCamLerpPercent);
            camera.orthographicSize     = Mathf.Lerp(m_ogCamOrthographicSize, 2f, followCamLerpPercent);
        }
    }

    public void ResetCam()
    {
        followCamLerpPercent = 0f;
        camera.transform.position = m_ogCamPosition;
        camera.orthographicSize = m_ogCamOrthographicSize;
    }

    void StoreOriginalCameraSettings()
    {
        if (camera == null)
        {
            camera = Camera.main;
            m_ogCamPosition = camera.transform.position;
            m_ogCamOrthographicSize = camera.orthographicSize;
        }
    }
}
