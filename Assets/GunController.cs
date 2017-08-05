using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void trackMouse(Vector3 position) {
		Vector3 heightAdjustedPosition = position;
		heightAdjustedPosition.y = .5f;
		Vector3 angle = Vector3.Normalize (heightAdjustedPosition - player.transform.position);
		transform.position = player.transform.position + (angle * .6f);
	}
}
