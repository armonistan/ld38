using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour {
	public GameObject PauseScreen;

	private int PAUSED = 0;
	private int UNPAUSED = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (Time.timeScale == PAUSED) {
				PauseScreen.GetComponent<Canvas>().enabled = false;
				UnpauseGame ();
			} else {
				PauseScreen.GetComponent<Canvas>().enabled = true;
				PauseGame ();
			}
		}
	}

	private void PauseGame (){
		Time.timeScale = PAUSED;
	}

	private void UnpauseGame(){
		Time.timeScale = UNPAUSED;
	}
}
