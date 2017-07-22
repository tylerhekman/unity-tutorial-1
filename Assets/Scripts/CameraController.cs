using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
	private Vector3 normalizedOffset;

	private Vector3 scrollOffset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
		normalizedOffset = offset.normalized;
	}

	void Update() {
		scrollOffset = scrollOffset - (Input.GetAxis ("Mouse ScrollWheel") * normalizedOffset);
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset + scrollOffset;
	}

}
