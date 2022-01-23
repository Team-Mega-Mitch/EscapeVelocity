using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public Animator animator;
    public PlayerController stopSpeed;
    public GameObject player;

    
    public IEnumerator CallEndScene() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
    public void LoseLevel() {
       // SceneManager.LoadScene("GameOver");
       animator.transform.localScale = new Vector3(animator.transform.localScale.x * 3, animator.transform.localScale.y*3, animator.transform.localScale.z);
       animator.Play("Spark Animation");
       stopSpeed.movementSpeed = 0f;
       stopSpeed.minThrust = 0f;
       stopSpeed.maxThrust = 0f;
       StartCoroutine(CallEndScene());
    }
}
