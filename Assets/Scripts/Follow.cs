using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	
	public GameObject following;
	private float minFollowDistance = 1;

	private Vector3 velocity = Vector3.zero;

	private float MAX_X = 19.6f;
	private float MIN_X = -19.6f;
	private float MAX_Z = 19.6f;
	private float MIN_Z = -19.6f;

	void Start () {
		
	}
	
	void FixedUpdate () {
		float distance = Vector3.Distance (transform.position, following.transform.position);
		if (distance > minFollowDistance) {
			transform.position = Vector3.SmoothDamp (transform.position, following.transform.position, ref velocity, mapRange(distance, 1.0f, 2.0f, .4f, .2f));
		}
		keepFollowerInBounds ();
	}

	void keepFollowerInBounds() {
		Vector3 boundedPosition = transform.position;
		if (transform.position.x > MAX_X) {
			boundedPosition.x = MAX_X;
		}
		if (transform.position.x < MIN_X) {
			boundedPosition.x = MIN_X;
		}
		if (transform.position.z > MAX_Z) {
			boundedPosition.z = MAX_Z;
		}
		if (transform.position.z < MIN_Z) {
			boundedPosition.z = MIN_Z;
		}
		transform.position = boundedPosition;
	}

	float mapRange (float value, float fromSource, float toSource, float fromTarget, float toTarget)
	{
		return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
	}
}
