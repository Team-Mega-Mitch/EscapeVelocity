using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public float mouseSpeed;
    public float mapSpeed;

    private Camera camera;

    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;

        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * mouseSpeed;
        Vector3 mouse = Input.mousePosition;

        Vector3 screenPoint = camera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

        transform.Translate(new Vector3(0, 1, 0) * mapSpeed * Time.deltaTime);
    }
}
