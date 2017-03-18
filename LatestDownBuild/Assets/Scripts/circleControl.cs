using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleControl : MonoBehaviour {
	private Rigidbody rb;

	public float speed = 2.0f; 
	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody> (); 
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal") * speed; 
		float moveVertical = Input.GetAxis ("Vertical"); 

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical); 
		rb.AddForce (movement); 
	} 



	// Update is called once per frame
	void Update () {
		
	}


}
