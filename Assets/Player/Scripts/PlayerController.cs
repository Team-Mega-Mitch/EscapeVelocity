using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public float minThrust;
    public float maxThrust;
    public float GravityBoostConstant = 2f;
    public float GravityPullConstant = 8f;

    private float thrust;
    private Vector3 distance;
    private Vector3 directionOfPlayerFromPlanet;
    private Vector2 mousePos;
    private Camera camera;
    private GameObject _Planet;
    private Animator animator;


    public float thrust_multiplier = .01f;

    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;

        directionOfPlayerFromPlanet = Vector3.zero;
        animator = this.GetComponent<Animator>();

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

        animator.SetFloat("speedMultiplier", Mathf.Clamp(thrust, 1, maxThrust));

        float velocity = (minThrust + thrust + GravityBoost());
        distance = new Vector3(0, 1, 0) * velocity * Time.fixedDeltaTime;

        if (_Planet != null) {
            directionOfPlayerFromPlanet = (_Planet.transform.position - transform.position).normalized;
        } else {
            directionOfPlayerFromPlanet = Vector3.zero;
        }
    }

    void FixedUpdate() {
        if (_Planet != null) {
            rigidBody.AddForce(directionOfPlayerFromPlanet * GravityPull());
        } else {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
        }

        transform.Translate(distance);

        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        // if (angle < -180) {
        //     angle = 90;
        // } else if (angle < -90) {
        //     angle = -90;
        // }

        rigidBody.rotation = angle;
    }

    private float GravityBoost() {
        if (!_Planet) {
            return 0;
        }

        float distance = Vector2.Distance(_Planet.transform.position, transform.position);
        float size = _Planet.GetComponent<PlanetInterface>().GetSize();

        return (GravityBoostConstant * size) / distance;
    }

    private float GravityPull() {
        if (!_Planet) {
            return 0;
        }

        float distance = Vector2.Distance(_Planet.transform.position, transform.position);
        float size = _Planet.GetComponent<PlanetInterface>().GetSize();

        return (GravityPullConstant * size) / distance;
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
