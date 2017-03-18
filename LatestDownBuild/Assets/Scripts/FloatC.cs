//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class FloatC : MonoBehaviour {
//	public float bobSpeed  = 3.0f;  //Bob speed
//	public float bobHeight  = 0.5f; //Bob height
//	public float bobOffset = 0.0f;
//	private float yT; 
//	private float bottom;
//	// Use this for initialization
//	void Start () {
//		bottom = transform.position.y;			
//	}
//
//	void Awake() {
//
//
//	} 
//	
//	// Update is called once per frame
//	void Update () {
//		
//		yT = bottom + (((Mathf.Cos((Time.time + bobOffset) * bobSpeed) + 1) / 2 ) * bobHeight);
//		GameObject.transform.position.y +yT;	
//	}
//}