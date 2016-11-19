using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverReset : MonoBehaviour {



	public void OnTriggerEnter() {
		SceneManager.LoadScene(0);
	}
}
