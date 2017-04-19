using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerSingle : MonoBehaviour {

    public void changeToSceneSingle(int changeTheSceneSingle)
    {

        SceneManager.LoadScene(changeTheSceneSingle);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
