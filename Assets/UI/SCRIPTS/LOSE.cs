using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LOSE : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void PlayAgain()

    {
        SceneManager.LoadScene("MainScene");
    }
}
