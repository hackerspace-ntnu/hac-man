using UnityEngine;
using System.Collections;

public class CubeManager : MonoBehaviour {

	public GameObject roof;

	private bool shrinking = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shrinking) {
			Vector3 boxScale = transform.localScale;
			Vector3 boxPosition = transform.localPosition;
			if (boxScale.y > 0.04) {
				boxScale.y -= 0.04f;
				transform.localScale = boxScale;
				boxPosition.y -= 0.004f;
				transform.localPosition = boxPosition;

			} else {
				Destroy(roof.gameObject);
				Destroy(gameObject);
			}
		}

	}

	public void StartShrink() {
		shrinking = true;
	}
}
