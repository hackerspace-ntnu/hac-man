using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public Transform targetLocation;

	bool recentlyTeleported = false;
	bool teleportedFromThisPortal = false;

	public void OnTriggerEnter(Collider col) {

		print ("Player entered portal: " + transform.name);

		if (col.transform.tag == "Player") {
			if (!recentlyTeleported) {
				col.transform.position = targetLocation.position;
				col.transform.rotation = targetLocation.rotation;
				recentlyTeleported = true;
				teleportedFromThisPortal = true;
			}
		}
	}

	public void OnTriggerExit(Collider col) {
		if (!teleportedFromThisPortal) {
			recentlyTeleported = false;
		} else {
			teleportedFromThisPortal = false;
		}

	}


}
