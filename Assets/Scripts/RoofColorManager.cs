using UnityEngine;
using System.Collections;

public class RoofColorManager : MonoBehaviour {

	public void ChangeRoofColor() {
		foreach(Transform child in transform) {
			EmissionController controller = child.GetComponent<EmissionController>();
			controller.PowerUpColorChange();
		}
	}
}
