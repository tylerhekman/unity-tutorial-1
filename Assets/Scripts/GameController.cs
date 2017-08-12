using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {

	private float MAX_X = 19.6f;
	private float MIN_X = -19.6f;
	private float MAX_Z = 19.6f;
	private float MIN_Z = -19.6f;

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

	Stack<GameObject> collectibles = new Stack<GameObject>();

	public Transform projectilePrefab;

	//SCORING
	private int winCount = 8;
	private int count;
	public Text countText;
	public Text winText;

	//DROPZONES
	Dictionary<string, bool> dropZoneMap = new Dictionary<string, bool>();

	private int collectibesInDropZone;
	List<GameObject> deactivatedCollectibles = new List<GameObject>();

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

		dropZoneMap.Add ("NE DropZone", false);
		dropZoneMap.Add ("NW DropZone", false);
		dropZoneMap.Add ("SE DropZone", false);
		dropZoneMap.Add ("SW DropZone", false);

		collectibesInDropZone = 0;
	}

	void Update () {
		if (Input.GetButtonDown ("Fire3") && count > 0) {
			dropCollectible ();
		}
	}

	void FixedUpdate() {
		Vector3 velocity = playerRigidBody.velocity;

		Vector3 behind = playerRigidBody.position - (normalizeVelocity(velocity) * followDistance);
		follower1.transform.position = behind;
        boundGameObject(follower1);
	}

	public void dropCollectible() {
		int updatedCount = count - 1;
		Vector3 lastFollowerPosition = followerChain [updatedCount].transform.position;
		lastFollowerPosition.y = Mathf.Max (1, lastFollowerPosition.y);
		GameObject collectible = collectibles.Pop ();
		collectible.transform.position = lastFollowerPosition;
		collectible.SetActive (true);
		followerChain [updatedCount].GetComponent<Renderer> ().enabled = false;
		count = updatedCount;
        updateWinText();
        setCountText();
    }

    public void collect(GameObject collectible) {
        collectible.GetComponent<Rotator>().resetSpeed();
		collectibles.Push(collectible);
	}

	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
	}

    void updateWinText()
    {
        if (count >= winCount)
        {
            winText.text = "You Win!!!";
            countText.text = "";
        }
        else
        {
            winText.text = "";
        }
    }

	public void evaluateWinCondition()
	{
		followerChain [count].GetComponent<Renderer> ().enabled = true;
		count = count + 1;
		setCountText();
        updateWinText();
	}

	Vector3 normalizeVelocity(Vector3 velocity)
	{
		Vector3 normalizedVelocity = velocity;
		normalizedVelocity.y = 0;
		return normalizedVelocity.normalized;
	}

	public void collectibleInDropZone(GameObject collectible, string dropZoneTag) {
		deactivatedCollectibles.Add (collectible);
		dropZoneMap [dropZoneTag] = true;
		if (allDropZonesOccupied ()) {
			print ("all drop zones occupied");
		}
		collectibesInDropZone = collectibesInDropZone + 1;
		evaluateDropZones ();
	}

	void evaluateDropZones() {
		print (collectibesInDropZone);
		if (collectibesInDropZone == 8) {
			print("all collectibles in drop zones");
			if (!allDropZonesOccupied ()) {
				resetCollectibles ();
				resetDropZones ();
			} else {
				deactivateAllCollectibles ();
			}
		}
	}
		
	void resetCollectibles() {
		foreach (var collectible in deactivatedCollectibles) {
			collectible.GetComponent<Rotator> ().resetLocation ();
		}
		collectibesInDropZone = 0;
		deactivatedCollectibles.Clear ();
	}

	void deactivateAllCollectibles() {
		foreach (var collectible in deactivatedCollectibles) {
			collectible.SetActive(false);
		}
		collectibesInDropZone = 0;
	}

	void resetDropZones() {
		var keys = new List<string>(dropZoneMap.Keys);
		foreach (string key in keys)
		{
			dropZoneMap [key] = false;
		}	
	}

	bool allDropZonesOccupied() {
		return dropZoneMap.All(dropZone => dropZone.Value.Equals(true));
	}

	public void boundGameObject(GameObject gameObject) {
		Vector3 boundedPosition = gameObject.transform.position;
		if (gameObject.transform.position.x > MAX_X) {
			boundedPosition.x = MAX_X;
		}
		if (gameObject.transform.position.x < MIN_X) {
			boundedPosition.x = MIN_X;
		}
		if (gameObject.transform.position.z > MAX_Z) {
			boundedPosition.z = MAX_Z;
		}
		if (gameObject.transform.position.z < MIN_Z) {
			boundedPosition.z = MIN_Z;
		}
        gameObject.transform.position = boundedPosition;
	}

	public void fireProjectile(Vector3 position) {
		if (count > 0) {
			var projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
			projectile.GetComponent<ProjectileController> ().gameController = this;
			projectile.GetComponent<ProjectileController> ().collectible = collectibles.Pop ();
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), player.GetComponent<Collider>());
            Vector3 angle = Vector3.Normalize (position - player.transform.position);
			projectile.transform.position = player.transform.position + angle;
			projectile.GetComponent<Rigidbody> ().AddForce (Vector3.Normalize (position - player.transform.position) * 1000);
			followerChain [count - 1].GetComponent<Renderer> ().enabled = false;
			count = count - 1;
			updateWinText();
			setCountText();
		}
	}

	public void projectileInDropZone(GameObject projectile) {
		projectile.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		projectile.GetComponent<Renderer> ().enabled = false;
		projectile.GetComponent<Collider> ().enabled = false;
		Vector3 projectilePosition = projectile.transform.position;
		projectilePosition.y = Mathf.Max (1, projectilePosition.y);
		print (projectilePosition);
		projectile.GetComponent<ProjectileController>().collectible.transform.position = projectilePosition;
		projectile.GetComponent<ProjectileController>().collectible.SetActive (true);
		Destroy (projectile);
	}

    public void reinstantiateCollectible(GameObject projectile)
    {
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Renderer>().enabled = false;
        projectile.GetComponent<Collider>().enabled = false;
        Vector3 projectilePosition = projectile.transform.position;
        projectilePosition.y = Mathf.Max(1, projectilePosition.y);
        print(projectilePosition);
        projectile.GetComponent<ProjectileController>().collectible.transform.position = projectilePosition;
        projectile.GetComponent<ProjectileController>().collectible.SetActive(true);
        Destroy(projectile);
    }
}
