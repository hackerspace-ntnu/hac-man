using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	private AudioSource aSource;
	public float[] samples = new float[64];
	private float[] audioValues = new float[64];

	// Use this for initialization
	void Start () {
		this.aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);
		for(int i=0; i<samples.Length;i++)
		{
			audioValues[i] = Mathf.Clamp(samples[i]*(50+i*i),0,50);
		}
	}

	public float getAudioValue(int n) {
		return audioValues[n];
	}
}
