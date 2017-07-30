using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameController gameController;

    private Rigidbody rb;

    //SPEED
    public float speedMultiplier;

    //JUMP
	public float jumpMultiplier;
	private Vector3 jumpVector = new Vector3(0.0f, 2.0f, 0.0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 v3 = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(speedMultiplier * v3);
    }
    void Update() {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < .01)
        {
			rb.AddForce(jumpVector * jumpMultiplier, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
		print ("collided");
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
			GameObject collectible = other.gameObject;
			gameController.collect (collectible);
			gameController.evaluateWinCondition ();
        }
    }
}
