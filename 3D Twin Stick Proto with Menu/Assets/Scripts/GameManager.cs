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


    //Slow Motion variables:
    private SlowMoManager slowMoManager;
    private bool m_isSlowMoAvailable;

    //General variables
    private int m_enemiesLeft;
    private bool m_isArenaMode;

    public int EnemiesLeft { get; set; }

    // Awake is called befoee anything else:
    void Awake()
    {
        //Setting up a single instance of the GameManager that persists through scenes and avoids duplication:
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        //Adding the SlowMoManager as a component:
        if (slowMoManager == null && m_isArenaMode == false)
        {
            slowMoManager = gameObject.AddComponent<SlowMoManager>();
        }

        

    }

    //Start is called before the first frame of update:
    void Start()
    {
        m_isSlowMoAvailable = false;
        m_enemiesLeft = 0;
        m_isArenaMode = false; 

        DeactivateSlowMo();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isArenaMode == true)
        {
            m_isSlowMoAvailable = false; 
        }
    }

    //Getter and setters for isSlowMoAvailable:
    public bool GetIsSlowMoAvailable()
    {
        return m_isSlowMoAvailable;
    }

    public void MakeSlowMoAvailable()
    {
        if (m_isArenaMode == false)
        { m_isSlowMoAvailable = true; }
    }

    public void MakeSlowMoUnavailable()
    {
        m_isSlowMoAvailable = false;
    }

    //Slow Motion activation and deactivation functions:
    public void ActivateSlowMoWithFollowCam(Vector3 followCamPosition)
    {
        if (m_isArenaMode == false)
        {
            slowMoManager.isSlowMo = true;
            slowMoManager.FollowCam(followCamPosition);
        }
    }

    public void ActivateSlowMoWithRelativePositions( Vector3 bulletPosition, Vector3 tankPosition, float initialDistance )
    {
        if (m_isArenaMode == false)
        {
            slowMoManager.SlowTimeWithVector3Ratio(bulletPosition, tankPosition, initialDistance);
            slowMoManager.FollowCamUsingRatioLerp(bulletPosition);
        }
    }

    public void DeactivateSlowMo()
    {
        slowMoManager.isSlowMo = false;
        slowMoManager.ResetCam();
    }


    public int GetEnemiesLeft()
    {
        return m_enemiesLeft;
    }

    public void SetEnemiesLeft(int numOfEnemies)
    {
        m_enemiesLeft = numOfEnemies;
    }

    public void KillEnemy()
    {
        m_enemiesLeft -= 1;
    }

    public void ArenaModeActive(bool ArenaMode)
    {
        m_isArenaMode = ArenaMode;
    }
}