using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    float y0;
    float amplitude = .3F;
    float initialSpeed = 2;
    private float speed;

    void Start()
    {
        y0 = transform.position.y;
        speed = initialSpeed;
    }

	void OnEnable() {
		y0 = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        Vector3 bobbingPosition = transform.position;
        bobbingPosition.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = bobbingPosition;
    }

    public void resetSpeed()
    {
        speed = initialSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DropZone"))
        {
            speed = 20;
        }
    }
}
