using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {


	public GameObject floor;
	private GameObject[] ghosts;

	// Use this for initialization
	void Start () {
		ghosts = GameObject.FindGameObjectsWithTag("Enemy1");
		StartCoroutine(DelayedGameOver());
	}

	IEnumerator DelayedGameOver() { 
		yield return new WaitForSeconds(3);
		foreach (GameObject ghost in ghosts) {
			Animator ghostAnimator = ghost.GetComponentInChildren<Animator>();
			ghostAnimator.SetBool("Chasing", true);
		}
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);


	}
}
