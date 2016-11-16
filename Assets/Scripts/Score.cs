using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreText;

	private int score = 0;

	private GameObject eventSystem;
	private Events events;

	public void Start() {
		eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
		events = eventSystem.GetComponent<Events> ();
		print("test");
	}

	public void Update() {
		scoreText.text = "Score: " + score;
	}

	public void IncrementScore (int value) {
		score += value;
		if (score >= 174) {
			events.Win();
		}
	}

	public int GetScore() {
		return score;
	}

}
