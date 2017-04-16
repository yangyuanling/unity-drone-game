using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone2MovementScript : MonoBehaviour
{

    Rigidbody ourDrone2;
    public GameObject ForPropellarSpin5;
    public GameObject ForPropellarSpin6;
    public GameObject ForPropellarSpin7;
    public GameObject ForPropellarSpin8;

    void Awake()
    {
        ourDrone2 = GetComponent<Rigidbody>();
        ForPropellarSpin5 = GameObject.Find("Cylinder063");
        ForPropellarSpin6 = GameObject.Find("Cylinder053");
        ForPropellarSpin7 = GameObject.Find("Cylinder016");
        ForPropellarSpin8 = GameObject.Find("Cylinder058");

    }


    void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerve();
        ourDrone2.AddRelativeForce(Vector3.up * upForce);
        ourDrone2.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );

        ForPropellarSpin5.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin6.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin7.transform.Rotate(0, 0 * Time.deltaTime, 500);
        ForPropellarSpin8.transform.Rotate(0, 0 * Time.deltaTime, 500);
    }

    public float upForce;
    void MovementUpDown()
    {
        /*
		Levitate based on input up or down
		*/
        if ((Mathf.Abs(Input.GetAxis("Vertical2")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f))
        {
            if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.Keypad5))
            {
                ourDrone2.velocity = ourDrone2.velocity;
            }
            if (!Input.GetKey(KeyCode.Keypad8) && !Input.GetKey(KeyCode.Keypad5) && !Input.GetKey(KeyCode.Keypad4) && !Input.GetKey(KeyCode.Keypad6))
            {
                ourDrone2.velocity = new Vector3(ourDrone2.velocity.x, Mathf.Lerp(ourDrone2.velocity.y, 0, Time.deltaTime * 5), ourDrone2.velocity.z);
                upForce = 281;
            }
            if (!Input.GetKey(KeyCode.Keypad8) && !Input.GetKey(KeyCode.Keypad5) && (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Keypad6)))
            {
                ourDrone2.velocity = new Vector3(ourDrone2.velocity.x, Mathf.Lerp(ourDrone2.velocity.y, 0, Time.deltaTime * 5), ourDrone2.velocity.z);
                upForce = 110;
            }
            if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Keypad6))
            {
                upForce = 410;
            }
        }
        if (Mathf.Abs(Input.GetAxis("Vertical2")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f)
        {
            upForce = 135;
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f)
            {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            upForce = -200;
        }
        else if (!Input.GetKey(KeyCode.Keypad8) && !Input.GetKey(KeyCode.Keypad5) && (Mathf.Abs(Input.GetAxis("Vertical2")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    private float movementForwardSpeed = 100.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;
    void MovementForward()
    {
        /*
		if(Mathf.Abs (Input.GetAxis ("Vertical2")) > 0.2f)  {
			ourDrone2.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical2") * movementForwardSpeed);
			tiltAmountForward = Mathf.SmoothDamp (tiltAmountForward, 30 * Input.GetAxis ("Vertical2"), ref tiltVelocityForward, 0.1f);
		}*/
        if (Input.GetAxis("Vertical2") != 0)
        {
            ourDrone2.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical2") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 30 * Input.GetAxis("Vertical2"), ref tiltVelocityForward, 0.1f);
        }
    }

    private float wantedYRotation;
    [HideInInspector]
    public float currentYRotation;
    private float rotateAmountByKeys = 2.5f; //how much we want to rotate as keys are held
    private float rotationYVelocity;

    void Rotation()
    {
        if (Input.GetKey(KeyCode.Keypad4))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            wantedYRotation += rotateAmountByKeys;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

    }

    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical2")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f)
        {
            ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity, Mathf.Lerp(ourDrone2.velocity.magnitude, 20.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical2")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) < 0.2f)
        {
            ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity, Mathf.Lerp(ourDrone2.velocity.magnitude, 20.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical2")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f)
        {
            ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity, Mathf.Lerp(ourDrone2.velocity.magnitude, 10.0f, Time.deltaTime * 2.5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical2")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal2")) < 0.2f)
        {
            ourDrone2.velocity = Vector3.SmoothDamp(ourDrone2.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerve()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal2")) > 0.2f)
        {
            ourDrone2.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal2") * sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal2"), ref tiltAmountVelocity, 0.1f);
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
	}
	void OnTriggerExit(Collider col)
	{
		Debug.Log ("Exited");
		if (col.gameObject.tag == "SlowSphere") {
			//movementForwardSpeed = 100.0f;
			//Destroy(gameObject);
			Time.timeScale = 1.0f;
		}
	}
    //}

    // Update is called once per frame
    //void Update () {

    //}
}
