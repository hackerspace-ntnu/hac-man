using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public Transform targetLocation;

	public GameObject otherPortal;
	private Portal otherPortalScript;

	public bool recentlyTeleported = false;


	public void Awake () {
		otherPortalScript = otherPortal.GetComponent<Portal>();
	}

	public void OnTriggerEnter(Collider col) {



		print ("Player entered portal: " + transform.name);

		if (col.transform.tag == "Player") {
			if (!recentlyTeleported) {
				col.transform.position = targetLocation.position;
				col.transform.rotation = targetLocation.rotation;
				otherPortalScript.recentlyTeleported = true;

			}
		}
	}

	public void OnTriggerExit(Collider col) {
		recentlyTeleported = false;
	}


}
