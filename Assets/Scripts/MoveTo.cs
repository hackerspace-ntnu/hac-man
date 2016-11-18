// MoveTo.cs
using UnityEngine;
using System.Collections;
    
public class MoveTo : MonoBehaviour {

	public enum EnemyState {
		Roaming,
		Chasing,
		Fleeing,
		Dying,
		Respawning
	};

	public EnemyState currentEnemyState;
    
	private NavMeshAgent agent;

	public GameObject[] waypoints;
	//private GameObject lastWaypoint;
	private GameObject currentWaypoint;

	private GameObject player;

	private float baseSpeed;

	public float chaseDuration = 3.0f; // Time in seconds you have to stay out of sight before enemy stops chasing
	private float chaseTime = 0.0f;

	private float fleeDuration = 15.0f; // Time in seconds enemies will try to flee when Player picks up a Powerup
	private float fleeTime = 0.0f;

	private float dyingSpinSpeed = 3.0f;

	private Animator ghostAnimator;

	public GameObject explosion;

	public GameObject ghostBody;
	public GameObject hitbox;
	private CapsuleCollider ghostCollider;
	public GameObject ghostRespawnWaypoint;

	private bool isFleeing = false;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
   		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		currentWaypoint = waypoints[Random.Range(0, waypoints.Length-1)];
		agent.destination = currentWaypoint.transform.position;
		baseSpeed = agent.speed;
		currentEnemyState = EnemyState.Roaming;
		ghostAnimator = GetComponentInChildren<Animator> ();
		ghostCollider = GetComponent<CapsuleCollider> ();
    }

	void Update() {
		if (currentEnemyState == EnemyState.Chasing) {
			agent.destination = player.transform.position;
			chaseTime -= Time.deltaTime;
			if (chaseTime <= 0) {
				currentWaypoint = GetNewWaypoint(currentWaypoint);
				currentEnemyState = EnemyState.Roaming;
				ghostAnimator.SetBool("Fleeing", false);
				ghostAnimator.SetBool("Roaming", true);
				ghostAnimator.SetBool("Chasing", false);
				agent.speed = baseSpeed;
			}
		} else if (currentEnemyState == EnemyState.Roaming) {
			Vector3 dstToWP = (this.transform.position - currentWaypoint.transform.position);
			dstToWP.y = 0;
			float distance = dstToWP.sqrMagnitude;
			if (distance < 1) {
				currentWaypoint = GetNewWaypoint (currentWaypoint);
			}
		} else if (currentEnemyState == EnemyState.Dying) {
			ghostAnimator.SetBool("Fleeing", false);
			ghostAnimator.SetBool("Roaming", false);
			ghostAnimator.SetBool("Chasing", false);
			ghostAnimator.SetBool("Dying", true);
			if (dyingSpinSpeed < 16.0f) {
				this.transform.RotateAround(this.transform.position, Vector3.up, 5.0f);
				dyingSpinSpeed += 0.02f;
			} else {
				GameObject ghostExplosion = (GameObject) Instantiate(explosion, this.transform.position, Quaternion.LookRotation(Vector3.up));
				Destroy(ghostExplosion, 5);
				Destroy(gameObject);
			}

		} else if (currentEnemyState == EnemyState.Fleeing) {
			Vector3 dstToWP = (this.transform.position - currentWaypoint.transform.position);
			dstToWP.y = 0;
			float distance = dstToWP.sqrMagnitude;
			if (distance < 1) {
				currentWaypoint = GetNewWaypoint (currentWaypoint);
			}
			fleeTime -= Time.deltaTime;
			if (fleeTime <= 0) {
				isFleeing = false;
				currentWaypoint = GetNewWaypoint(currentWaypoint);
				currentEnemyState = EnemyState.Roaming;
				agent.speed = baseSpeed;
				ghostAnimator.SetBool("Fleeing", false);
				ghostAnimator.SetBool("Roaming", true);
				ghostAnimator.SetBool("Chasing", false);
			}

		} else if (currentEnemyState == EnemyState.Respawning) {
			Vector3 dstToWP = (this.transform.position - currentWaypoint.transform.position);
			dstToWP.y = 0;
			float distance = dstToWP.sqrMagnitude;
			if (distance < 1) {
				OnReset();
			}
		}


	}

	GameObject GetNewWaypoint (GameObject currentWaypoint) {
		GameObject newTarget = waypoints[Random.Range(0, waypoints.Length-1)];
		while (newTarget.transform.position == currentWaypoint.transform.position ) {
			newTarget = waypoints[Random.Range(0, waypoints.Length-1)];
		}
		// lastWaypoint = currentWaypoint;
		currentWaypoint = newTarget;
		agent.destination = currentWaypoint.transform.position;
		return currentWaypoint;
	}

	public void OnReset () {
		currentWaypoint = GetNewWaypoint (currentWaypoint);
		currentEnemyState = EnemyState.Roaming;
		agent.speed = baseSpeed;
		isFleeing = false;
		ghostBody.SetActive(true);
		hitbox.SetActive(true);
		ghostCollider.enabled = true;
		ghostAnimator.SetBool("Fleeing", false);
		ghostAnimator.SetBool("Roaming", true);
		ghostAnimator.SetBool("Chasing", false);
	}

	public void ChasePlayer () {
		if (currentEnemyState != EnemyState.Dying && currentEnemyState != EnemyState.Fleeing && currentEnemyState != EnemyState.Respawning) {
			agent.speed = baseSpeed * 2;
			currentEnemyState = EnemyState.Chasing;
			chaseTime = chaseDuration;
			ghostAnimator.SetBool("Fleeing", false);
			ghostAnimator.SetBool("Roaming", false);
			ghostAnimator.SetBool("Chasing", true);
		}
	}

	public void FleeFromPlayer() {
		currentWaypoint = GetNewWaypoint (currentWaypoint);
		isFleeing = true;
		agent.speed = baseSpeed / 2;
		currentEnemyState = EnemyState.Fleeing;
		fleeTime = fleeDuration;
		ghostAnimator.SetBool("Fleeing", true);
		ghostAnimator.SetBool("Roaming", false);
		ghostAnimator.SetBool("Chasing", false);
	}

	public void EatenByPlayer() {
		isFleeing = false;
		currentEnemyState = EnemyState.Respawning;
		agent.speed = baseSpeed * 2;
		currentWaypoint = ghostRespawnWaypoint;
		agent.destination = currentWaypoint.transform.position;
		ghostBody.SetActive(false);
		hitbox.SetActive(false);
		ghostCollider.enabled = false;
	}

	public void Die() {
		agent.Stop();
		currentEnemyState = EnemyState.Dying;
		ghostAnimator.SetBool("Fleeing", false);
		ghostAnimator.SetBool("Roaming", false);
		ghostAnimator.SetBool("Chasing", false);
		ghostAnimator.SetBool("Dying", true);
	}

	public bool IsFleeing () {
		return isFleeing;
	}
}