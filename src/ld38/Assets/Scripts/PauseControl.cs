using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour {
	public KeyCode PauseKey = KeyCode.E;
    public KeyCode QuitKey = KeyCode.Q;

	private int PAUSED = 0;
	private int UNPAUSED = 1;

    private Image _pauseImage;

	// Use this for initialization
	void Start () {
        _pauseImage = GetComponentInParent<Image>();
        _pauseImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(PauseKey)) {
			if (Time.timeScale == PAUSED) {
				_pauseImage.enabled = false;
				UnpauseGame ();
			} else {
				_pauseImage.enabled = true;
				PauseGame ();
			}
		}
        if (_pauseImage.enabled)
        {
            if (Input.GetKey(QuitKey))
            {
                FindObjectOfType<GameControl>().Quit();
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
