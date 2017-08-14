using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public GameController gameController;
	public GameObject collectible;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		GetComponent<Rigidbody> ().velocity -= Vector3.ClampMagnitude(GetComponent<Rigidbody> ().velocity, 1) * 1 * Time.deltaTime;
        if(GetComponent<Rigidbody>().velocity.magnitude < .2 && GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            gameController.reinstantiateCollectible(this.gameObject);
        }
	}

	void OnCollisionEnter (Collision col)
	{
        if (col.gameObject.CompareTag("Wall"))
        {
            var bounceVelocity = Vector3.Reflect(-col.relativeVelocity * 0.5f, col.contacts[0].normal);
            GetComponent<Rigidbody>().velocity = bounceVelocity;
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.EndsWith("DropZone"))
		{
			gameController.projectileInDropZone (this.gameObject);
		}
	}
}
