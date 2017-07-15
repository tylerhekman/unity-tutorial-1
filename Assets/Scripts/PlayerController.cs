using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    public float speedMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 v3 = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(speedMultiplier * v3);
    }
    void Update() {
        Vector3 currentPosition = rb.position;
        if (Input.GetKeyDown("space") && Mathf.Abs(rb.velocity.y) < .01)
        {
            print("jump");
            rb.AddExplosionForce(300, currentPosition, 100);
        }
    }
}
