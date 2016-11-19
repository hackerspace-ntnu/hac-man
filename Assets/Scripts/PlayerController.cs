using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 15;
	public float runSpeed = 20;

	public TextMesh scoreText;
	public int pelletPoints = 1; // Points for each Pellet
	int score = 0;

	public Events eventManager;
	private GameObject wallManager;
	private CubeManager wallManagerScript;
	private GameObject roofManager;
	private RoofColorManager roofManagerScript;


	bool cursorVisible = false;

	// Safety bool
	private bool alreadyWon = false;

	// System vars
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbodyP;

	private Transform startTransform;

	public GameObject pauseText;
	
	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraTransform = Camera.main.transform;
		rigidbodyP = GetComponent<Rigidbody> ();
		startTransform = this.transform;
		wallManager = GameObject.FindGameObjectWithTag("WallManager");
		wallManagerScript = wallManager.GetComponent<CubeManager>();
		roofManager = GameObject.FindGameObjectWithTag("Roof");
		roofManagerScript = roofManager.GetComponent<RoofColorManager>();
	}
	
	void Update() {
		
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		
		Vector3 targetMoveAmount;
		// Check if Running
		if (Input.GetKey("left shift")) {
			targetMoveAmount = moveDir * runSpeed;
		} else {
			targetMoveAmount = moveDir * walkSpeed;
		}
		
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Unlock Mouse on click
		if (Input.GetKeyDown("escape") && !cursorVisible) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			cursorVisible = true;
			Time.timeScale = 0;
			pauseText.SetActive(true);
		} else if (Input.GetKeyDown("escape") && cursorVisible) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			cursorVisible = false;
			Time.timeScale = 1;
			pauseText.SetActive(false);
		}	
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbodyP.MovePosition(rigidbodyP.position + localMove);
	}
		

	// Handle Pickups
	public void OnTriggerEnter (Collider col) {
		if (col.gameObject.CompareTag ("Pickup_Pellet")) {
			score += 1;
			scoreText.text = "Score: " + score;
			if (score >= 148) {
				if (!alreadyWon) {
					alreadyWon = true;
					eventManager.Win();
				}

			}
		} else if (col.gameObject.CompareTag("Powerup")) {
			wallManagerScript.PowerupColorChange();
			roofManagerScript.ChangeRoofColor();
			eventManager.OnPowerupPickup();
			print("Ate Powerup!");
			// Coroutine here
		} else if (col.gameObject.CompareTag("Ghost")) {
			if (!alreadyWon) {
				MoveTo enemyScript = col.GetComponentInParent<MoveTo> ();
				if (!enemyScript.IsFleeing()) {
					print("Collided with ghost!");
					eventManager.OnLoseLife();
				} else {
					enemyScript.EatenByPlayer();
				}
			}
		}
	}

	public Transform GetStartTransform() {
		return startTransform;
	}

}