using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject player;
    public GameController gameController;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		transform.Rotate (90.0f, 0.0f, 0.0f);
	}

	public void trackMouse(Vector3 position) {
		Vector3 heightAdjustedPosition = position;
		heightAdjustedPosition.y = player.transform.position.y;
		Vector3 angle = Vector3.Normalize (heightAdjustedPosition - player.transform.position);
		transform.position = player.transform.position + (angle * .6f);
		transform.LookAt (heightAdjustedPosition);
	}

    public void fire(Vector3 position)
    {
        Vector3 heightAdjustedPosition = position;
		heightAdjustedPosition.y = player.transform.position.y;
		gameController.fireProjectile(heightAdjustedPosition);
    }
}
