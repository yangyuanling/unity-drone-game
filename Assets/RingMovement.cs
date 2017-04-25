using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour {

	private float min = 2.0f;
	private float max = 7.0f;
	//private float rotationleft = 180;
	//private float rotationSpeed = 50;
	//private float rotation;
	// Use this for initialization
	void Start () {
		min = transform.position.x;
		max = transform.position.x + 10;
	}

	// Update is called once per frame
	void Update () {

		//rotation = rotationSpeed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.PingPong (Time.time * 10, max - min) + min, transform.position.y, transform.position.z);
		//transform.Rotate (0, rotation, 0);
	}
}

