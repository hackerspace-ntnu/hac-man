using UnityEngine;
using System.Collections;

public class RotateTowards : MonoBehaviour {

	private GameObject playerObject;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(playerObject.transform.position);
	}
}
