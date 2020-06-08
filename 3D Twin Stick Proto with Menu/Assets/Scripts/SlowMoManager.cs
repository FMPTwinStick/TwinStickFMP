using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoManager : MonoBehaviour
{
    //public variables:
    public bool isSlowMo;

    //private variables:
    private Camera camera;
    private Vector3 m_ogCamPosition;
    private float m_ogCamOrthographicSize;
    private bool m_isSlowMoAvailable;

    private float defaultSlowTimeScale;
    
    private float followCamLerpPercent;
    private float followCamLerpSpeed;
    private float slowMoLerpPercent;
    private float slowMoLerpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
        defaultSlowTimeScale = 0.1f;
        isSlowMo = false;

        camera = Camera.main;
        m_ogCamPosition = camera.transform.position;
        m_ogCamOrthographicSize = camera.orthographicSize;
        GameManager.GetGameManager().MakeSlowMoAvailable();

        RayTraceBulletBehaviour.canAffectTimeScale = true;

        followCamLerpPercent = 0f;
        followCamLerpSpeed = 0.003f;
        slowMoLerpPercent = 0f;
        slowMoLerpSpeed = 0.003f;
    }

    // Update is called once per frame
    public void Update()
    {
        AlterTime();
        StoreOriginalCameraSettings();
    }

    //Checks if Slow motion is activated and acts accordingly:
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

    //Slows time to the default slow mo time scale:
    void SlowTime()
    {
        if (GameManager.GetGameManager().GetIsSlowMoAvailable())
        {
            if (slowMoLerpPercent < 1f)
            {
                slowMoLerpPercent += slowMoLerpSpeed;
            }
            if (slowMoLerpPercent > 1f)
            {
                slowMoLerpPercent = 1f;
            }
            Time.timeScale = Mathf.Lerp(1f, defaultSlowTimeScale, slowMoLerpPercent);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    //Slows time to the specified timeScale:
    void SlowTime( float timeScale )
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    //Resets the timeScale back to 1:
    void ResetTime()
    {
        Time.timeScale = 1f;
        RayTraceBulletBehaviour.canAffectTimeScale = true;
    }

    //follow cam takes a position and makes the camera follow that position:
    public void FollowCam( Vector3 position )
    {
        if (GameManager.GetGameManager().GetIsSlowMoAvailable())
        {
            StoreOriginalCameraSettings();
            Vector3 followPos = new Vector3(position.x, position.y + 10f, position.z - 10f);
            if (followCamLerpPercent < 1f)
            {
                followCamLerpPercent += followCamLerpSpeed;
            }
            if (followCamLerpPercent > 1f)
            {
                followCamLerpPercent = 1f;
            }
            camera.transform.position = Vector3.Lerp(m_ogCamPosition, followPos, followCamLerpPercent);
            camera.orthographicSize = Mathf.Lerp(m_ogCamOrthographicSize, 2f, followCamLerpPercent);
        }
    }

    //Resets the camera to its original settings in the scene:
    public void ResetCam()
    {
        followCamLerpPercent = 0f;
        camera.transform.position = m_ogCamPosition;
        camera.orthographicSize = m_ogCamOrthographicSize;
    }

    //StoreOriginalCameraSettings stores the settings of the camera before it is altered so that it can be reverted back:
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
