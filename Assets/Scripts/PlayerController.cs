using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    //SPEED
    public float speedMultiplier;

    //JUMP
	public float jumpMultiplier;
	private Vector3 jumpVector = new Vector3(0.0f, 2.0f, 0.0f);

    //SCORING
    private int winCount = 8;
    private int count;
    public Text countText;
    public Text winText;

    //FOLLOWER
    public GameObject follower;
    private int followDistance = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 v3 = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(speedMultiplier * v3);

        Vector3 velocity = rb.velocity;

        Vector3 behind = rb.position - normalizeVelocity(velocity);
        follower.transform.position = behind;
    }
    void Update() {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < .01)
        {
			rb.AddForce(jumpVector * jumpMultiplier, ForceMode.Impulse);
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
    
    Vector3 normalizeVelocity(Vector3 velocity)
    {
        Vector3 normalizedVelocity = velocity;
        normalizedVelocity.y = 0;
        return normalizedVelocity.normalized * followDistance;
    }
}
