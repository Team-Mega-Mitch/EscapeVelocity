using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Transform player;
    public Text scoreText;

    public int scoreValue;

    public Text HighScoreText;
    void Update()
    {
        scoreText.text = player.position.y.ToString("0");
    }

    void Start()
    {
        HighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void HighScore ()

    {
        if (player.position.y > PlayerPrefs.GetInt("HighScore", 0))
        {
            scoreValue = (int) player.position.y;
            PlayerPrefs.SetInt("HighScore", scoreValue);
            HighScoreText.text = player.position.y.ToString("0");
            PlayerPrefs.SetString("HighScore",(player.position.y.ToString("0")));
        }
        
    }

}
