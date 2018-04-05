using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public BeamController beamController;
	public GunController gunController;

	private float chargeTime = 0f;

	// Use this for initialization
	void Start() {

    }

    // Update is called once per frame
    void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int maskOfPlane = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskOfPlane)) {
            var position = hit.point;
            gunController.trackMouse(position);
			if (Input.GetMouseButton(0)) {
				chargeTime += Time.deltaTime;
			}
			if (Input.GetMouseButtonUp(0)) {
				if (chargeTime > 1f) {
					gunController.fireAll();
				}
				else {
					gunController.fire(position);
				}
				chargeTime = 0f;
			}
            beamController.tractorBeam(position);
        }
    }
}
