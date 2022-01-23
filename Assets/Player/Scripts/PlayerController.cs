using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public float minThrust;
    public float maxThrust;

    private float thrust;
    private Vector2 mousePos;
    private Camera camera;

    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;

        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
            if (thrust != maxThrust) {
                thrust = Mathf.Clamp(thrust + .05f, 0, maxThrust);
            }
        } else if (thrust != 0) {
            thrust = Mathf.Clamp(thrust - .05f, 0, maxThrust);
        }
    }

    void FixedUpdate() {
        transform.Translate(new Vector3(0, 1, 0) * (minThrust + thrust) * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(movementSpeed, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (angle < -180) {
            angle = 90;
        } else if (angle < -90) {
            angle = -90;
        }

        rigidBody.rotation = angle;
    }
}
