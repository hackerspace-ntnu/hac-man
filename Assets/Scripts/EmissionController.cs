using UnityEngine;
using System.Collections;

public class EmissionController : MonoBehaviour {

	private Renderer colorRenderer;
	private Material mat;
	private Color originalColor;
	private Color baseColor;

	public float emissionTimer = 1.0f;

	// Use this for initialization
	void Awake () {
		colorRenderer = GetComponent<Renderer>();
		mat = colorRenderer.material;
		originalColor = mat.GetColor("_EmissionColor");
		baseColor = originalColor;
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetKeyDown("e")) {
			baseColor = Color.red;
		}
		if (Input.GetKeyUp("e")) {
			baseColor = originalColor;
		}

		float emission = Mathf.PingPong (Time.time, emissionTimer);
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		mat.SetColor("_EmissionColor", finalColor);
	}
}
