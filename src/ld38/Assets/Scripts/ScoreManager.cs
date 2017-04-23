using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public Text Score;
	public Text AddedScore;

	private int _score;
	private int _multiplier;

	// Use this for initialization
	void Start () {
		_score = 0;
		_multiplier = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Score.text = _score.ToString();
	}

	public void AddScore(int scoreToAdd) {
		_score += scoreToAdd * _multiplier;
		AddedScore.text = "+" + scoreToAdd.ToString();
		if (_multiplier > 1) 
		{
			AddedScore.text = AddedScore.text + "x" + _multiplier.ToString() + "!";
		}
	}

	public void BoostMultiplier() {
		_multiplier += 1;
	}

	public void ResetMultipler() {
		_multiplier = 1;
	}
}
