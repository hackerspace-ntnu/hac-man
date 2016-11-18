using UnityEngine;
using System.Collections;

public class EmissionController : MonoBehaviour {

	private Renderer colorRenderer;
	private Material mat;
	private Color originalColor;
	private Color baseColor;

	public float emissionTimer = 1.0f;

	private float powerUpEmissionDuration = 10.0f;
	private float powerUpEmissionTimer = 0.0f;

	private bool powerupActive = false; 

	// Use this for initialization
	void Awake () {
		colorRenderer = GetComponent<Renderer>();
		mat = colorRenderer.material;
		originalColor = mat.GetColor("_EmissionColor");
		baseColor = originalColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (powerupActive) {
			if (powerUpEmissionTimer <= 0) {
				baseColor = originalColor;
				powerupActive = false;
			} else {
				powerUpEmissionTimer-= Time.deltaTime;
			}
		}

		float emission = Mathf.PingPong (Time.time, emissionTimer);
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		mat.SetColor("_EmissionColor", finalColor);
	}

	public void PowerUpColorChange() {
		powerUpEmissionTimer = powerUpEmissionDuration;
		baseColor = Color.cyan;
		powerupActive = true;
	}
}
