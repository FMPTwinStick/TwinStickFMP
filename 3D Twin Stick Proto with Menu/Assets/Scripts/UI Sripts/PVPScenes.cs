using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PVPScenes : MonoBehaviour
{
    public void LoadRandomScene()
    {
        int Index = Random.Range(44, 53);
        SceneManager.LoadScene(Index);
        Debug.Log("RandomSceneLoaded");
    }
}
