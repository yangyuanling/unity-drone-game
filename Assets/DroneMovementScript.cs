using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DroneMovementScript : MonoBehaviour
{

    Rigidbody ourDrone;
    public GameObject ForPropellarSpin1;
    public GameObject ForPropellarSpin2;
    public GameObject ForPropellarSpin3;
    public GameObject ForPropellarSpin4;
    public GameObject Canvas1;
	public GameObject InterfaceCanvas;

	public Text TimerText;
	public Text RingsText1Object;
	public Text Drone1FinishTimer;
	private Text counterText;
	private Text RingsText1Display;
	private Text Drone1FinishDisplay;
	private int counterRings1;
	public float seconds, minutes;

    void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
        ForPropellarSpin1 = GameObject.Find("Cylinder062");
        ForPropellarSpin2 = GameObject.Find("Cylinder052");
        ForPropellarSpin3 = GameObject.Find("Cylinder015");
        ForPropellarSpin4 = GameObject.Find("Cylinder057");
        Canvas1 = GameObject.Find("Canvas");
    }


    void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerve();
        ourDrone.AddRelativeForce(Vector3.up * upForce);
        ourDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );

        ForPropellarSpin1.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin2.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin3.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin4.transform.Rotate(0, 0 * Time.deltaTime, 500);
    }

    public float upForce;
    void MovementUpDown()
    {
        /*
		Levitate based on input up or down
		*/
        if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                ourDrone.velocity = ourDrone.velocity;
            }
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                upForce = 410;
            }
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            upForce = -200;
        }
        else if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

	public float movementForwardSpeed = 100.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;
    void MovementForward()
    {
        /*
		if(Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f)  {
			ourDrone.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical") * movementForwardSpeed);
			tiltAmountForward = Mathf.SmoothDamp (tiltAmountForward, 30 * Input.GetAxis ("Vertical"), ref tiltVelocityForward, 0.1f);
		}*/
        if (Input.GetAxis("Vertical") != 0)
        {
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 30 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    private float wantedYRotation;
    [HideInInspector]
    public float currentYRotation;
    private float rotateAmountByKeys = 2.5f; //how much we want to rotate as keys are held
    private float rotationYVelocity;

    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            wantedYRotation += rotateAmountByKeys;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

    }

    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 20.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 20.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerve()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);
        }
        else
        {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
        }
    }

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("Entered");
		if (col.gameObject.tag == "SlowSphere") {

			Time.timeScale = 0.4f;
		}

		if (col.gameObject.tag == "RingPassDetector") {

			col.gameObject.SetActive (false);
			counterRings1 += 1;
			SetRing1CounterText ();
		}

		if (col.gameObject.tag == "LastRing") {

			col.gameObject.SetActive (false);
			SetDrone1FinishTimeText ();
			col.gameObject.SetActive (true);
		}
	}
	void OnTriggerExit(Collider col)
	{
		Debug.Log ("Exited");
		if (col.gameObject.tag == "SlowSphere") {
			//movementForwardSpeed = 100.0f;
			Time.timeScale = 1.0f;
		}
	}

	void SetRing1CounterText() {
		RingsText1Display.text = "Rings: " + counterRings1.ToString();
	}

	void SetDrone1FinishTimeText() {
		Debug.Log("Finished!");// + counterText.text);
		Drone1FinishDisplay.text = "Finish Time:\n" + counterText.text;
	}
    //}

    // Update is called once per frame
    
	void Start() {
		counterRings1 = 0;
		counterText = TimerText.GetComponent(typeof(Text)) as Text;
		RingsText1Display = RingsText1Object.GetComponent(typeof(Text)) as Text;
		Drone1FinishDisplay = Drone1FinishTimer.GetComponent(typeof(Text)) as Text;

		//counterText = GetComponent<Text> () as Text;
	}

	void Update () {

		if((GameObject.Find("1PlayerButtonPressed").GetComponent<TitleToGame1>().gameStarted==true) || (GameObject.Find("2PlayerButtonPressed").GetComponent<TitleToGame1>().gameStarted==true)) {
		 	minutes = (int)(Time.time / 60.0f);
			seconds = (int)(Time.time % 60.0f);
			counterText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
		}
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Entered");
            //Canvas1.SetActive(true);
			SceneManager.LoadScene(0);
        }
    }
}
