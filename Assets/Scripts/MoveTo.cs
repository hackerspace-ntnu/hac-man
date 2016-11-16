// MoveTo.cs
using UnityEngine;
using System.Collections;
    
public class MoveTo : MonoBehaviour {

	public enum EnemyState {
		Roaming,
		Chasing,
		Fleeing,
		Dying
	};

	public EnemyState currentEnemyState;
    
	private NavMeshAgent agent;

	public GameObject[] waypoints;
	private GameObject lastWaypoint;
	private GameObject currentWaypoint;

	private GameObject player;

	private float baseSpeed;

	public float chaseDuration = 3.0f; // Time in seconds you have to stay out of sight before enemy stops chasing
	private float chaseTime = 0.0f;

	private float fleeDuration = 10.0f; // Time in seconds enemies will try to flee when Player picks up a Powerup
	private float fleeTime = 0.0f;

	private float dyingSpinSpeed = 3.0f;

	public GameObject explosion;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
   		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		currentWaypoint = waypoints[Random.Range(0, waypoints.Length-1)];
		agent.destination = currentWaypoint.transform.position;
		baseSpeed = agent.speed;
		currentEnemyState = EnemyState.Roaming;

    }

	void Update() {
		if (currentEnemyState == EnemyState.Chasing) {
			agent.destination = player.transform.position;
			chaseTime -= Time.deltaTime;
			if (chaseTime <= 0) {
				currentWaypoint = GetNewWaypoint(currentWaypoint);
				currentEnemyState = EnemyState.Roaming;
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
			if (dyingSpinSpeed < 8.0f) {
				this.transform.RotateAround(this.transform.position, Vector3.up, 5.0f);
				dyingSpinSpeed += 0.01f;
			} else {
				GameObject ghostExplosion = (GameObject) Instantiate(explosion, this.transform.position, Quaternion.LookRotation(Vector3.up));
				Destroy(ghostExplosion, 5);
				Destroy(gameObject);
			}

		}


	}

	GameObject GetNewWaypoint (GameObject currentWaypoint) {
		GameObject newTarget = waypoints[Random.Range(0, waypoints.Length-1)];
		while (newTarget.transform.position == currentWaypoint.transform.position ) {
			newTarget = waypoints[Random.Range(0, waypoints.Length-1)];
		}
		lastWaypoint = currentWaypoint;
		currentWaypoint = newTarget;
		agent.destination = currentWaypoint.transform.position;
		return currentWaypoint;
	}

	public void OnReset () {
		currentWaypoint = GetNewWaypoint (currentWaypoint);
		currentEnemyState = EnemyState.Roaming;
	}

	public void ChasePlayer () {
		if (currentEnemyState != EnemyState.Dying && currentEnemyState != EnemyState.Fleeing) {
			agent.speed = baseSpeed * 2;
			currentEnemyState = EnemyState.Chasing;
			chaseTime = chaseDuration;
		}
	}

	public void FleeFromPlayer() {
		
	}

	public void Die() {
		agent.Stop();
		currentEnemyState = EnemyState.Dying;
	}
}