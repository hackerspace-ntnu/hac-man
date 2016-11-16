using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public GameObject heartExplosion;


	public GameObject[] lives;
	private int livesLeft;

	// Use this for initialization
	void Start () {
		livesLeft = lives.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int Loselife () {
		GameObject lostLife = lives[livesLeft - 1];
		GameObject explosion = (GameObject) Instantiate(heartExplosion, lostLife.transform.position, Quaternion.identity, this.transform);
		Destroy(lostLife);
		Destroy(explosion, 1);
		livesLeft--;
		return livesLeft;
	}
}
