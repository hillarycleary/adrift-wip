using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioRayCast : MonoBehaviour {

	//Material skybox;

	public float skyExposureIncrement = 0.25f; 
	public float skyExposure01 = 1.0f;
	public float originalSkybox = 0f;
	private float lightIntensity = .1f; 
	private bool violinPlayed = false; 
	private bool ringer6Played = false; 
	private bool bassPlayed = false; 
	private bool arpPlayed = false; 
	private bool violin2Played = false; 


	//platform variables
	public Transform farEnd;
	private Vector3 frometh;
	private Vector3 untoeth;
	float moveDistance = 10f;
	private float secondsForOneLength = 10f;


	void Start() {
		GameObject.FindGameObjectWithTag ("bassGlow").GetComponent<ParticleSystem>().Stop();
		GameObject.FindGameObjectWithTag ("violinGlow").GetComponent<ParticleSystem>().Stop();
		GameObject.FindGameObjectWithTag ("ringer6Glow").GetComponent<ParticleSystem>().Stop();
		GameObject.FindGameObjectWithTag ("arpGlow").GetComponent<ParticleSystem>().Stop();
		GameObject.FindGameObjectWithTag ("bass2Glow").GetComponent<ParticleSystem> ().Stop ();


		GameObject.FindGameObjectWithTag ("violin2Glow").GetComponent<ParticleSystem> ().Stop ();

		GameObject.FindGameObjectWithTag ("platform1").GetComponent<Renderer> ().enabled = false;


	} 




	void FixedUpdate()
	{
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
		
			RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
		
			if(hit.transform.gameObject.tag == "violinSphere") {
		
				hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
				print("Found an object - distance: " + hit.distance);
			 GameObject.FindGameObjectWithTag ("violinGlow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = .1f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;
					violinPlayed = true;



			}

				if(hit.transform.gameObject.tag == "bassSphere") {

					hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
					hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					print("Found an object - distance: " + hit.distance);
					GameObject.FindGameObjectWithTag ("bassGlow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = .4f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;

					bassPlayed = true; 

				}

				if(hit.transform.gameObject.tag == "ringer6Sphere") {

					hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
					hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					print("Found an object - distance: " + hit.distance);
					GameObject.FindGameObjectWithTag ("ringer6Glow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = .5f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;

					ringer6Played = true;

				}

				if(hit.transform.gameObject.tag == "arpSphere") {

					hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
					hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					print("Found an object - distance: " + hit.distance);
					GameObject.FindGameObjectWithTag ("arpGlow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = .7f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;

					arpPlayed = true; 

				}

				if(hit.transform.gameObject.tag == "bass2Sphere") {

					hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
					hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					print("Found an object - distance: " + hit.distance);
					GameObject.FindGameObjectWithTag ("bass2Glow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = 1.3f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;

				}


				if(hit.transform.gameObject.tag == "violin2Sphere") {

					hit.collider.gameObject.GetComponent<AudioSource> ().Play();
					hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
					hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					print("Found an object - distance: " + hit.distance);
					GameObject.FindGameObjectWithTag ("violin2Glow").GetComponent<ParticleSystem> ().Play ();
					lightIntensity = 1.0f; 
					GameObject.FindGameObjectWithTag ("light").GetComponent<Light> ().intensity = lightIntensity;

					violin2Played = true; 

				}


		}
		}

		if (bassPlayed == true && violinPlayed == true) {
            //GameObject.FindGameObjectWithTag ("blockrock").GetComponent<Renderer>().enabled = false;
			GameObject.FindGameObjectWithTag ("blockrock").GetComponent<MeshCollider>().enabled = false;
			//GameObject.FindGameObjectWithTag ("blockrock").GetComponent<AudioSource> ().Play();
			GameObject.FindGameObjectWithTag ("blockrock").transform.Translate(Vector3.down * Time.deltaTime * 3, Space.World);
		}

		if (arpPlayed == true && ringer6Played == true && violin2Played == true) {
			
			//GameObject.FindGameObjectWithTag ("platform1").transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
			//GameObject.FindGameObjectWithTag ("platform1").transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World);



		

			//GameObject.FindGameObjectWithTag ("platform1").transform.position = Vector3.Lerp(frometh, untoeth, Mathf.SmoothStep(0f,1f,Mathf.PingPong(Time.time/secondsForOneLength, 1f)));
			//GameObject.FindGameObjectWithTag ("platform1").transform.position = Vector3.Lerp(frometh, untoeth, Mathf.SmoothStep(0f,1f,Mathf.PingPong(Time.time/secondsForOneLength, 1f)));


			//GameObject.FindGameObjectWithTag ("platform1").SetActive (true); 
			GameObject.FindGameObjectWithTag ("platform1").GetComponent<Renderer>().enabled = true;
			//GameObject.FindGameObjectWithTag ("platform1").transform.Translate(transform.forward*Mathf.Cos (Time.time)*Time.deltaTime);
			//GameObject.FindGameObjectWithTag ("platform1").transform.Translate(transform.up*Mathf.Cos(-40)*Time.deltaTime);
		 
	}


}
}
//GameObject.FindGameObjectWithT		ag ("violinGlow").GetComponent<ParticleSystem>().enableEmission = false;

//darken sky 
//RenderSettings.skybox.SetFloat("_Exposure", skyExposure01);