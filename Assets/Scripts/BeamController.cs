using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {

    public GameObject player;

    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

	void FixedUpdate() {
		velocity = player.GetComponent<Rigidbody> ().velocity;
	}

    void LateUpdate()
    {
        transform.Rotate(180.0f, 0.0f, 0.0f);
    }

    public void tractorBeam(Vector3 position)
    {
        if (Input.GetMouseButton(1))
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            Vector3 heightAdjustedPosition = position;
            heightAdjustedPosition.y = player.transform.position.y;
            Vector3 angle = Vector3.Normalize(heightAdjustedPosition - player.transform.position);
            transform.position = player.transform.position + (angle * 2.5f);
            transform.LookAt(player.transform.position + (angle * 100.0f));
        }
        else {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
			var distance = Vector3.Distance (other.gameObject.transform.position, player.transform.position);
			var coefficient = mapRange (distance, 2.0f, 8.0f, .05f, .2f);
            other.gameObject.transform.position = Vector3.SmoothDamp(other.gameObject.transform.position, 
				player.transform.position, 
				ref velocity, coefficient);
        }
    }

	float mapRange (float value, float fromSource, float toSource, float fromTarget, float toTarget)
	{
		return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
	}
}
