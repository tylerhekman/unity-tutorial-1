using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    public float speedMultiplier;

    private int winCount = 8;

    private int count;
    public Text countText;
    public Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 v3 = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(speedMultiplier * v3);
    }
    void Update() {
        Vector3 currentPosition = rb.position;
        if (Input.GetKeyDown("space") && Mathf.Abs(rb.velocity.y) < .01)
        {
            print("jump");
            rb.AddExplosionForce(300, currentPosition, 100);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            evaluateWinCondition();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void evaluateWinCondition()
    {
        count = count + 1;
        setCountText();
        if(count >= winCount)
        {
            winText.text = "You Win!!!";
            countText.text = "";
        }
    }
}
