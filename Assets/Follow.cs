using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	
	public GameObject following;
	public float followSpeed;
	private float minFollowDistance = 1;

	private Vector3 velocity = Vector3.zero;

	void Start () {
		
	}
	
	void FixedUpdate () {
		if (Vector3.Distance(transform.position, following.transform.position) > minFollowDistance) {
			transform.position = Vector3.SmoothDamp (transform.position, following.transform.position, ref velocity, followSpeed);
		}
	}
}
