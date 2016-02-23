using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EatPellet : MonoBehaviour {

	public int points = 1;
	public AudioSource eatSound;

	public void OnTriggerEnter(Collider col) {
		if (col.transform.CompareTag("Player")) {
			if (eatSound != null) {
				eatSound.Play();
			}
			Destroy(gameObject);
		}
	}



}
