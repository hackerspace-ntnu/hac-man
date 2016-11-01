using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour {

	public int noOfEnemies = 3;

	public LifeManager lifeSystem;



	private GameObject[] enemyAgents;
	private GameObject playerAgent;

	private Vector3[] enemyStartPositions;
	private Quaternion[] enemyStartRotations;

	private Vector3 playerStartPosition;
	private Quaternion playerStartRotation;

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onLoseLife() {
		print("OnLoseLife Called");
		for (int i = 0; i < noOfEnemies; i++ ) {
			enemyAgents[i].transform.position = enemyStartPositions[i];
			enemyAgents[i].transform.rotation = enemyStartRotations[i];
		}
		playerAgent.transform.position = playerStartPosition;
		playerAgent.transform.rotation = playerStartRotation;
		lifeSystem.Loselife();
	}

	public void GameOver() {
		Time.timeScale = 0;
	}
}
