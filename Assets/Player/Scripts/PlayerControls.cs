using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public RigidBody2D theRB;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        theRB.velocity = new Vector2(Input.GetAxiasRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    }
}
