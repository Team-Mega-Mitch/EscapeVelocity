using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public float minThrust;
    public float maxThrust;
    public float GravityBoostConstant;
    public float GravityPullConstant;

    private float thrust;
    private Vector2 mousePos;
    private Camera camera;
    private GameObject _Planet;


    public float thrust_multiplier = .01f;

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
                thrust = Mathf.Clamp(thrust + thrust_multiplier, 0, maxThrust);
            }
        } else if (thrust != 0) {
            thrust = Mathf.Clamp(thrust - thrust_multiplier, 0, maxThrust);
        }
    }

    void FixedUpdate() {
        float velocity = (minThrust + thrust + GravityBoost());
        Vector3 distance = new Vector3(0, 1, 0) * (minThrust + thrust + GravityBoost()) * Time.fixedDeltaTime;
        
        // if(_Planet) {
        //     Vector2 displacement = _Planet.transform.position - transform.position;
        //     float theta = Mathf.Atan(displacement.y / displacement.x);

        //     Vector3 gravityPull = new Vector3(Mathf.Sin(theta), Mathf.Cos(theta), 0);
        //     gravityPull *= GravityPull(velocity, theta);
        //     distance += gravityPull;
        // }

        transform.Translate(distance);

        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(movementSpeed, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (angle < -180) {
            angle = 90;
        } else if (angle < -90) {
            angle = -90;
        }

        rigidBody.rotation = angle;
    }

    private float GravityBoost() {
        if(!_Planet) {
            return 0;
        }

        float distance = Vector2.Distance(_Planet.transform.position, transform.position);
        float size = _Planet.GetComponent<PlanetInterface>().GetSize();

        return (GravityBoostConstant * size) / distance;
    }

    private float GravityPull(float velocity, float theta) {
        if(!_Planet) {
            return 0;
        }

        float distance = Vector2.Distance(_Planet.transform.position, transform.position);
        float size = _Planet.GetComponent<PlanetInterface>().GetSize();

        return (((GravityPullConstant * size) / distance) * .5f * Time.fixedDeltaTime * Time.fixedDeltaTime) + ((velocity / Mathf.Sin(theta)) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Gravity")) {
            _Planet = collision.gameObject.transform.parent.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Gravity")) {
            _Planet = null;
        }
    }
}
