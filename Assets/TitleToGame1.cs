using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleToGame1 : MonoBehaviour {

    public GameObject Drone1;
    public GameObject Drone2;

    public Camera Camera1;
    public Camera Camera2;

    public GameObject Canvas1;
    private AudioSource DroneEngine1;
    //private AudioSource DroneEngine2;

    public void changeToSinglePlayer()
    {
        Drone1.SetActive(true);
        Drone2.SetActive(false);

        DroneEngine1 = Drone1.GetComponent(typeof(AudioSource)) as AudioSource;

        Canvas1 = GameObject.Find("Canvas");
        //Camera2 = GameObject.Find("Main Camera2");

        Camera1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        //Camera2.view

        Canvas1.SetActive(false);
        DroneEngine1.Play();
    }

    public void changeToMultiPlayer ()
    {
        Drone1.SetActive(true);
        Drone2.SetActive(true);

        DroneEngine1 = Drone1.GetComponent(typeof(AudioSource)) as AudioSource;
        //DroneEngine2 = Drone2.GetComponent(typeof(AudioSource)) as AudioSource;

        Canvas1 = GameObject.Find("Canvas");
        
        Camera1.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        Camera2.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);

        Canvas1.SetActive(false);
        DroneEngine1.Play();
        //DroneEngine2.Play();
    }

    public void quitGame()
    {
        Application.Quit();
    }
    // Use this for initialization
    void Start () {

        //Camera1 = GameObject.Find("Main Camera1");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
