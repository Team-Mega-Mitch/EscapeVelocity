using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LOSE : MonoBehaviour
{
    public void Start() {
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void PlayAgain()

    {
        //SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("play again");
    }
}
