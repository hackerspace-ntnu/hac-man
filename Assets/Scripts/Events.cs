using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour {

	public int noOfEnemies = 3;

	public LifeManager lifeSystem;


	private GameObject[] enemyAgents;
	private GameObject playerAgent;

	private Vector3[] enemyStartPositions;
	private Quaternion[] enemyStartRotations;

	private Vector3 playerStartPosition;
	private Quaternion playerStartRotation;

	public Transform gameOverRoomtransform;

	public GameObject wallObject;
	private CubeManager walls;

	MoveTo[] enemies;

	// Use this for initialization
	void Start () {
		enemyAgents = new GameObject[3]{GameObject.FindGameObjectWithTag("Enemy1"), GameObject.FindGameObjectWithTag("Enemy2"), GameObject.FindGameObjectWithTag("Enemy3")};
		enemyStartPositions = new Vector3[noOfEnemies];
		enemyStartRotations = new Quaternion[noOfEnemies];
		for (int i = 0; i < noOfEnemies; i++) {
			enemyStartPositions[i] = enemyAgents[i].transform.position;
			enemyStartRotations[i] = enemyAgents[i].transform.rotation;
		}
		playerAgent = GameObject.FindGameObjectWithTag("Player");
		playerStartPosition = playerAgent.transform.position;
		playerStartRotation = playerAgent.transform.rotation;
		walls = wallObject.GetComponent<CubeManager>();
		enemies = GameObject.FindObjectsOfType(typeof(MoveTo)) as MoveTo[];
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnLoseLife() {
		int livesLeft = lifeSystem.Loselife();
		if (livesLeft <= 0) {
			GameOver();
		} else {
			for (int i = 0; i < noOfEnemies; i++ ) {
				enemyAgents[i].transform.position = enemyStartPositions[i];
				enemyAgents[i].transform.rotation = enemyStartRotations[i];
				MoveTo agentMoveToScript = enemyAgents[i].GetComponent<MoveTo> ();
				agentMoveToScript.OnReset();
			}
			playerAgent.transform.position = playerStartPosition;
			playerAgent.transform.rotation = playerStartRotation;
		}


	}

	public void GameOver() {
		playerAgent.transform.position = gameOverRoomtransform.position;
		SceneManager.LoadScene(2);
	}

	public void Win() {
		for (int i = 0; i < enemies.Length; i++) {
			MoveTo startDeathSequence = enemies[i];
			startDeathSequence.Die();
		}

		walls.StartShrink();
		print("Win!");
	}
}
