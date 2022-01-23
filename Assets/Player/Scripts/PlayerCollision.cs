
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private WinLose _GameOver;

    private void Start() {
        _GameOver = GetComponent<WinLose>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name.Contains("Planet")) {
            _GameOver.LoseLevel();
        }
    }
}
