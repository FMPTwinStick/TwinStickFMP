using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This Script has been done by Aziz Ali
public class MainMenu : MonoBehaviour
{
    public Text highscoreText;

    void Start()
    {
        highscoreText.text = "Highscore : " + PlayerPrefs.GetFloat("Highscore");
    }
    
    //This quits the application
    public void QuitGame()
    {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    //This loads the PvP scene
    public void LoadPvP()
    {
        SceneManager.LoadScene("");
    }
}