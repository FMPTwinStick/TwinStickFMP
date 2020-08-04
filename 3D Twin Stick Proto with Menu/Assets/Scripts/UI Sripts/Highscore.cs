using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "Highscore : " + PlayerPrefs.GetFloat("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
