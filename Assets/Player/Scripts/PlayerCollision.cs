
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private WinLose _GameOver;
    private bool collided = false;
    private GameObject obj;

    private void Start() {
        _GameOver = GetComponent<WinLose>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Planet")) {
            _GameOver.LoseLevel();
        } else {
            collided = true;
            obj = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        collided = false;
        obj = null;
    }

    void FixedUpdate() {
        if (collided) {
            Debug.Log("We hiting");
            
        } else {
            Debug.Log("Where to hit???");
        }
    }
}
