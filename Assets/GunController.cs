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
		heightAdjustedPosition.y = .5f;
		Vector3 angle = Vector3.Normalize (heightAdjustedPosition - player.transform.position);
		transform.position = player.transform.position + (angle * .6f);
		transform.LookAt (heightAdjustedPosition);
	}

    public void fire(Vector3 position)
    {
//        cursor.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 heightAdjustedPosition = position;
        heightAdjustedPosition.y = .5f;
        //cursor.transform.position = heightAdjustedPosition;
        //gameController.boundGameObject(cursor);
//        cursor.transform.position = player.transform.position;
//        cursor.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(heightAdjustedPosition - player.transform.position) * 10000);
		gameController.fireProjectile(heightAdjustedPosition);
    }
}
