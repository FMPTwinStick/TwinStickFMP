using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script has been done by Aziz Ali
public class DeathScreen : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;

    private int loadPreviousScene;
    void Start()
    {
        highscoreText.text = "Current Highscore : " + PlayerPrefs.GetFloat("Highscore");
        scoreText.text = "Your Score : " + PlayerPrefs.GetFloat("Score", Score.scoreValue);
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
        loadPreviousScene = PlayerPrefs.GetInt("SavedScene");
        if (loadPreviousScene != 0)
            SceneManager.LoadScene(loadPreviousScene);
    }

    void update()
    {

    }
}
