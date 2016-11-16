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

	public float chaseDuration = 3.0f;
	private float baseSpeed;
	private float chaseTime = 0.0f;



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
			print (tag + " chasing!");
			agent.destination = player.transform.position;
			chaseTime -= Time.deltaTime;
			if (chaseTime <= 0) {
				print (tag + " giving up");
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
		agent.speed = baseSpeed * 2;
		currentEnemyState = EnemyState.Chasing;
		chaseTime = chaseDuration;
	}

	public void Die() {
		currentEnemyState = EnemyState.Dying;
	}
}