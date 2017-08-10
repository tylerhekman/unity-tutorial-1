using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public GameController gameController;

	private Vector3 velocity = Vector3.zero;

    float y0;
    float amplitude = .3F;
    float initialSpeed = 2;
    float initialRotationMultiplier = 2;
    private float speed;
    private float rotationMultiplier;

	private bool moveTowardsInitialLocation;

	private Vector3 initialLocation;

    void Start()
    {
        y0 = transform.position.y;
        speed = initialSpeed;
		rotationMultiplier = initialRotationMultiplier;
		initialLocation = transform.position;
		moveTowardsInitialLocation = false;
    }

	void OnEnable() {
		y0 = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * rotationMultiplier);
        Vector3 bobbingPosition = transform.position;
        bobbingPosition.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = bobbingPosition;

		if (moveTowardsInitialLocation) {
			transform.position = Vector3.SmoothDamp (transform.position, initialLocation, ref velocity, 0.3f);
			if (Vector3.Distance(transform.position, initialLocation) < .2) {
				moveTowardsInitialLocation = false;
				GetComponent<Collider> ().enabled = true;
			}
		}
    }

    public void resetSpeed()
    {
        speed = initialSpeed;
        rotationMultiplier = initialRotationMultiplier;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.EndsWith("DropZone"))
        {
            speed = 20;
            rotationMultiplier = 10;
			GetComponent<Collider> ().enabled = false;
			gameController.collectibleInDropZone (this.gameObject, other.gameObject.tag);
        }
    }

	public  void resetLocation() {
		moveTowardsInitialLocation = true;
		resetSpeed ();	}
}
