using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Transform startTransform;

	// Use this for initialization
	void Start () {
		startTransform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform GetStartTransform() {
		return startTransform;
	}
}
