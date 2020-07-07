using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script has been done by Aziz Ali
public class DeathScreen : MonoBehaviour
{
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }    
    
    //This allows you to quit the application
    public void QuitGame()
    {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    //This allows you to laod the main menu scene
    public void LoadMainMenu()
    {
        Debug.Log("Loading Menu");
        SceneManager.LoadScene("MainMenu");
    }

    //This allows you to restart the previous scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void update()
    {
        scoreText.text = "Score :" + Score.scoreValue;
    }
}
