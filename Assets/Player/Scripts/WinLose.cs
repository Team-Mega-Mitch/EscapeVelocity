using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public void LoseLevel() {
        SceneManager.LoadScene("LoseScreen");
    }
}
