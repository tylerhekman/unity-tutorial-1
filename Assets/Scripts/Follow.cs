using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	
	public GameObject following;
	private float minFollowDistance = 1;

	private Vector3 velocity = Vector3.zero;

	void Start () {
		
	}
	
	void FixedUpdate () {
		float distance = Vector3.Distance (transform.position, following.transform.position);
		if (distance > minFollowDistance) {
			transform.position = Vector3.SmoothDamp (transform.position, following.transform.position, ref velocity, mapRange(distance, 1.0f, 2.0f, .4f, .2f));
		}
	}

	float mapRange (float value, float fromSource, float toSource, float fromTarget, float toTarget)
	{
		return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
	}
}
