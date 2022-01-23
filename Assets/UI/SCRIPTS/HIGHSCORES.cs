using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HIGHSCORES : MonoBehaviour
{
    public Transform player;
    public Text score;
    public Text HighScoreText;

    void Start()
    {
        HighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void HighScore ()

    {
        if (player.position.y > PlayerPrefs.GetInt("HighScore", 0))
        {
            int PlayPos = (int) player.position.y;
            PlayerPrefs.SetInt("HighScore", PlayPos);
            HighScoreText.text = player.position.y.ToString();
        }
        
    }


}
