using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : MonoBehaviour {
    private struct RecordedScore
    {
        public int score, multiplier;

        public RecordedScore(int s, int m)
        {
            score = s;
            multiplier = m;
        }
    };

    public Text Score;
    public Text AddedScore;

    private int _score;
    private int _multiplier;

    private RecordedScore _currentScoreAdding;
    private ArrayList _scoresToAdd;
    private int _scoreAddStepSize;
    private NumberFormatInfo nfi;


    // Use this for initialization
    void Start()
    {
        AddedScore.text = "";
        _score = 0;
        _multiplier = 1;

        //code to 'peel' numbers off of a queue, add them one at a time to score for visual effect
        _currentScoreAdding = new RecordedScore(0, 0);
        _scoresToAdd = new ArrayList();
        _scoreAddStepSize = 1;
        nfi = new CultureInfo("en-US", false).NumberFormat;
        nfi.NumberDecimalDigits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentScoreAdding.score == 0 && _scoresToAdd.Count > 0)
        {
            _currentScoreAdding = (RecordedScore)_scoresToAdd[0];
            _scoresToAdd.RemoveAt(0);
        }
        else if (_currentScoreAdding.score > 0)
        {
            _scoreAddStepSize = Mathf.Max(_scoresToAdd.Count + 1, _scoreAddStepSize);
            int calculatedScoreAdd = _scoreAddStepSize * _currentScoreAdding.multiplier;
            if (_currentScoreAdding.score >= calculatedScoreAdd)
            {
                _score += calculatedScoreAdd;
                _currentScoreAdding.score -= calculatedScoreAdd;
            }
            else
            {
                _score += _currentScoreAdding.score;
                _currentScoreAdding.score = 0;
            }
        }
        else
        {
            _scoreAddStepSize = 1;
        }
        Score.text = _score.ToString("N", nfi);
    }

    public void AddScore(int scoreToAdd)
    {
        _scoresToAdd.Add(new RecordedScore(scoreToAdd * _multiplier, _multiplier));
        AddedScore.text = "+" + scoreToAdd.ToString();
        if (_multiplier > 1)
        {
            AddedScore.text = AddedScore.text + "x" + _multiplier.ToString() + "!";
        }
    }

    public void BoostMultiplier()
    {
        _multiplier += 1;
    }

    public void ResetMultipler()
    {
        _multiplier = 1;
    }
}
