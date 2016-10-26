using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public AudioSource powerupSound;

	public void OnTriggerEnter(Collider col) {
		if (col.transform.CompareTag("Player")) {
			if (powerupSound != null) {
				powerupSound.Play();
			}
			Destroy(gameObject);
		}
	}
}
