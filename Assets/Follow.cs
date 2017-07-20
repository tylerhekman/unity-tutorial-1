using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	
	public GameObject follower;
	public float followSpeed;

	private Vector3 velocity = Vector3.zero;

	void Start () {
		
	}
	
	void FixedUpdate () {
		transform.position = Vector3.SmoothDamp(transform.position, follower.transform.position, ref velocity, followSpeed);
	}
}
