using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    
    public GameObject LosePanel;
    private bool gameOver;
    public void LoseLevel() 
    {
        if(!gameOver)
        {
        Debug.Log("You lose!");
        LosePanel.SetActive(true);
        gameOver = true;
        }
    }

}
