using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPassScript : MonoBehaviour {

    void OnParticleCollision(GameObject other)
    {
        Rigidbody body = other.GetComponent<Rigidbody>();
        if (body)
        {
            Debug.Log("Ringed!");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
