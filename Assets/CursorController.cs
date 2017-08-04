using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int maskOfPlane = 1 << 8;
		if (Physics.Raycast (ray, out hit, maskOfPlane)) {
			var position = hit.point;
			print (position);
		} else {
			print("no raycast collision");
		}
	}
}
