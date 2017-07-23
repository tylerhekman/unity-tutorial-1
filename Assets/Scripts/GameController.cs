using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//PLAYER
	public GameObject player;
	private Rigidbody playerRigidBody;

	//FOLLOWER
	public GameObject follower1;
	public GameObject follower2;
	public GameObject follower3;
	public GameObject follower4;
	public GameObject follower5;
	public GameObject follower6;
	public GameObject follower7;
	public GameObject follower8;
	private int followDistance = 2;
	GameObject[] followerChain = new GameObject[8];

	GameObject[] collectibles = new GameObject[8];

	//SCORING
	private int winCount = 8;
	private int count;
	public Text countText;
	public Text winText;

	void Start () {
		playerRigidBody = player.GetComponent<Rigidbody> ();

		count = 0;
		setCountText();
		winText.text = "";

		followerChain [0] = follower1;
		followerChain [1] = follower2;
		followerChain [2] = follower3;
		followerChain [3] = follower4;
		followerChain [4] = follower5;
		followerChain [5] = follower6;
		followerChain [6] = follower7;
		followerChain [7] = follower8;
		foreach (GameObject follower in followerChain) {
			follower.GetComponent<Renderer> ().enabled = false;
		}
	}

	void Update () {
		if (Input.GetButtonDown ("Fire3") && count > 0) {
			collectibles [count - 1].transform.position = followerChain [count - 1].transform.position;
			collectibles [count-1].SetActive (true);
			followerChain [count - 1].GetComponent<Renderer> ().enabled = false;
			count = count - 1;
		}
	}

	void FixedUpdate() {
		Vector3 velocity = playerRigidBody.velocity;

		Vector3 behind = playerRigidBody.position - (normalizeVelocity(velocity) * followDistance);
		follower1.transform.position = behind;
	}

	public void collect(GameObject collectible) {
		collectibles [count] = collectible;
	}

	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
	}

	public void evaluateWinCondition()
	{
		followerChain [count].GetComponent<Renderer> ().enabled = true;
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
		return normalizedVelocity.normalized;
	}
}
