using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleToGame1 : MonoBehaviour {

    public GameObject Drone1;
    public GameObject Drone2;

    public Camera Camera1;
    public Camera Camera2;

    public GameObject Canvas1;

    public void changeToSinglePlayer()
    {
        Drone1.SetActive(true);
        Drone2.SetActive(false);

        Canvas1 = GameObject.Find("Canvas");
        //Camera2 = GameObject.Find("Main Camera2");

        Camera1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        //Camera2.view

        Canvas1.SetActive(false);
    }

    public void changeToMultiPlayer ()
    {
        Drone1.SetActive(true);
        Drone2.SetActive(true);

        Canvas1 = GameObject.Find("Canvas");
        
        Camera1.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        Camera2.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);

        Canvas1.SetActive(false);
    }

    // Use this for initialization
    void Start () {

        //Camera1 = GameObject.Find("Main Camera1");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
