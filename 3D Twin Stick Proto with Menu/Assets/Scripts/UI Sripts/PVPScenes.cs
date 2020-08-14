using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//This Script has been done by Aziz Ali
public class PVPScenes : MonoBehaviour
{
    public void LoadRandomScene()
    {
        //This allows you to laod a random scene in the scene index between 44 and 53
        int Index = Random.Range(44, 53);
        SceneManager.LoadScene(Index);
        Debug.Log("RandomSceneLoaded");
    }
}
