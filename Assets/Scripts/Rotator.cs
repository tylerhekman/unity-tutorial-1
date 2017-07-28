using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public GameController gameController;

    float y0;
    float amplitude = .3F;
    float initialSpeed = 2;
    float initialRotationMultiplier;
    private float speed;
    private float rotationMultiplier;

    void Start()
    {
        y0 = transform.position.y;
        speed = initialSpeed;
        initialRotationMultiplier = 1;
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
			gameController.collectibleInDropZone (other.gameObject.tag);
        }
    }
}
