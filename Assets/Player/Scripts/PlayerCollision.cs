
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public WinLose winLoseScript;
    void OnCollisionEnter2D(Collision2D collision) 
    {
        winLoseScript.LoseLevel();
    }
}
