using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    
    void Update()
    {
        if(player != null) {
            scoreText.text = player.position.y.ToString("0");
        }
    }

}
