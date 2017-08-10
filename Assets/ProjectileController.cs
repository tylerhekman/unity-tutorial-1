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

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.EndsWith("DropZone"))
		{
			gameController.projectileInDropZone (this.gameObject);
		}
	}
}
