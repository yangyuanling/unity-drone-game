using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneMovementScript : MonoBehaviour {


	private float min = 2.0f;
	private float max = 7.0f;
	private float rotationleft = 180;
	private float rotationSpeed = 50;
	private float rotation;
	// Use this for initialization
	void Start () {
		min = transform.position.z;
		max = transform.position.z + 10;
	}
	
	// Update is called once per frame
	void Update () {

		rotation = rotationSpeed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x, transform.position.y, Mathf.PingPong (Time.time * 7, max - min) + min);
		transform.Rotate (0, rotation, 0);
	}
}

