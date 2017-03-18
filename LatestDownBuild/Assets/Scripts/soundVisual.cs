using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundVisual : MonoBehaviour {
	private const int SAMPLE_SIZE = 1024; 
	//average power output of sound
	public float rmsValue;
	//sound in that exact frame
	public float dbValue; 
	public float pitchValue; 


	private AudioSource source; 
	private float[] samples;
	private float[] spectrum;
	private float sampleRate; 


	// Use this for initialization
	private void Start () {
		source = GetComponent<AudioSource> ();
		samples = new float[SAMPLE_SIZE];
		spectrum = new float[SAMPLE_SIZE];
		sampleRate = AudioSettings.outputSampleRate;
		
	}
	
	// Update is called once per frame
	private void Update () {

		AnalyzeSound (); 
		
	}

	private void AnalyzeSound() {
		//sample passed as a reference because it is an array
		source.GetOutputData (samples, 0); 
		//get RMS value, getting sum of all the squared sample 
		int i = 0; 
		float sum = 0;

		for (; i < SAMPLE_SIZE; i++) {
			sum = samples [i] * samples [i];
		}
			
		rmsValue = Mathf.Sqrt (sum / SAMPLE_SIZE);
		//Get DB value

		dbValue = 20 * ( Mathf.Log10(rmsValue / 1.0f));
		//get sound spectrum
		source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris); 
	} 
}
