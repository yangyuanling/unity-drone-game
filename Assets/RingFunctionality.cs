using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFunctionality : MonoBehaviour {

    public GameObject currentRing;
    public GameObject nextRing;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Entered");

        if (col.gameObject.tag == "Player")
        {
            nextRing.gameObject.SetActive(true);
            currentRing.gameObject.SetActive(false);
        }
    }

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
