using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

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
            Vector3 heightAdjustedPosition = position;
            heightAdjustedPosition.y = player.transform.position.y;
            Vector3 angle = Vector3.Normalize(heightAdjustedPosition - player.transform.position);
            transform.position = player.transform.position + (angle * 2.5f);
            transform.LookAt(player.transform.position + (angle * 100.0f));
        }
        else {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
