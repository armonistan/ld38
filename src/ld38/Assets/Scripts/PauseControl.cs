using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour {
	private int PAUSED = 0;
	private int UNPAUSED = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (Time.timeScale == PAUSED) {
				UnpauseGame ();
			} else {
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
