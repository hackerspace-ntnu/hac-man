using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGamePortal : MonoBehaviour {

	public void OnTriggerEnter() {
		SceneManager.LoadScene(1);
	}
}
