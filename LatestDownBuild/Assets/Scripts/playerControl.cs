using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	public float speed = 11.0f;
	public float sensitivity = 2.0f; 



	public float moveFB;
	public float moveLR;

	public float gravity = -9.8f; 

	float rotX; 
	float rotY; 

	CharacterController player; 

	public GameObject eyes; 

	// Use this for initialization
	void Start () {
		
		player = GetComponent<CharacterController> (); 	
	}
	
	// Update is called once per frame
	void Update () {

		moveFB = Input.GetAxis ("Vertical") * speed;
		moveLR = Input.GetAxis ("Horizontal") * speed; 

		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY = Input.GetAxis ("Mouse Y") * sensitivity; 

		Vector3 movement = new Vector3 (moveLR, 0, moveFB); 
		movement = transform.rotation * movement; 
		//movement = Vector3.ClampMagnitude (movement, speed);
		movement.y = gravity; 

		player.Move (movement * Time.deltaTime);

		transform.Rotate (0, rotX, 0); 
		eyes.transform.Rotate (-rotY, 0, 0); 


	
	}


}
