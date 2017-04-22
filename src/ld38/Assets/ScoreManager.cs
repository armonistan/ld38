using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public Text Score;

	private int _score;
	private string SCORE_BASE_STRING = "Score: ";

	// Use this for initialization
	void Start () {
		_score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Score.text = SCORE_BASE_STRING + _score;
	}

	public void UpdateScore(int scoreToAdd){
		_score += scoreToAdd;
	}
}
