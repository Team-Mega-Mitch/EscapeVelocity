using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    
    
    private bool gameOver;
    public void LoseLevel() 
    {
        if(!gameOver)
        {
        Debug.Log("You lose!");
        SceneManager.LoadScene("LoseScreen");
        gameOver = true;
        }
    }

}
