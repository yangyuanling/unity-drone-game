using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleToGame1 : MonoBehaviour {

    public GameObject Drone1;
    public GameObject Drone2;

    public Camera Camera1;
    public Camera Camera2;

    public GameObject Canvas1;
	//public GameObject InterfaceCanvas;
    private AudioSource DroneEngine1;

	public bool gameStarted=false;
    //private AudioSource DroneEngine2;

	public Text RingsText2Object;
	public Text Drone2FinishTimer;

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
		gameStarted = true;
		RingsText2Object.GetComponent<Text>().enabled = false;
		Drone2FinishTimer.GetComponent<Text>().enabled = false;
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
		gameStarted = true;
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
