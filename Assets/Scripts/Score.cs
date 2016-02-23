using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreText;

	private int score = 0;

	public void Update() {
		scoreText.text = "Score: " + score;
	}

	public void IncrementScore (int value) {
		score += value;
	}

}
