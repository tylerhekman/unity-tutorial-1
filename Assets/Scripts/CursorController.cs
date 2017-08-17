using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public BeamController beamController;
	public GunController gunController;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int maskOfPlane = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskOfPlane))
        {
            var position = hit.point;
            gunController.trackMouse(position);
//            if (Input.GetMouseButtonDown(0))
//            {
//                gunController.fire(position);
//            }
            beamController.tractorBeam(position);
        }
		if (Input.GetMouseButtonDown(0))
		{
			gunController.fireAll ();
		}
    }
}
